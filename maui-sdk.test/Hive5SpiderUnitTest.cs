using Hive5;
using Hive5.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace maui_sdk.test
{
    [TestClass]
    public class Hive5SpiderUnitTest
    {
        private Hive5Client apiClient { get; set; }

        [TestInitialize]
        public void InitTest()
        {
            if (this.apiClient == null)
            {
                var client = Hive5Client.Instance;
                string appKey = Hive5UnitTest.ValidAppKey;
                string uuid = Hive5UnitTest.Uuid;

                client.Init(appKey, uuid, Hive5UnitTest.TestZone);
                this.apiClient = client;

                Login();
            }
        }





        [TestMethod, TestCategory("Spider-Basic")]
        public void TestConnect()
        {
            var spider = Connect();
            Assert.IsTrue(spider.IsConnected == true);
            Assert.IsTrue(spider.SessionId > 0);
        }

        [TestMethod, TestCategory("Spider-Basic")]
        public void TestDisconnect()
        {
            Hive5Spider spider = Connect();

            var completion = new ManualResetEvent(false);

            spider.Disconnected += (s, e) =>
                {
                    Assert.IsTrue(spider.IsConnected == false);
                    completion.Set();
                };
            spider.Disconnect();

            completion.WaitOne();
        }


        [TestMethod, TestCategory("Spider-Publish")]
        public void TestSendNoticeMessage()
        {
            Hive5Spider spider = Connect();

            var completion = new ManualResetEvent(false);

            Dictionary<string, object> contents = new Dictionary<string, object>();
            contents.Add("content", "notice test by gilbert");

            spider.SendNoticeMessage("gogogo", contents, (success) =>
            {
                Assert.IsTrue(success == true);
                completion.Set();
            });

            completion.WaitOne();
        }

        [TestMethod, TestCategory("Spider-Call")]
        public void TestGetChannels()
        {
            Hive5Spider spider = Connect();

            var completion = new ManualResetEvent(false);

            Dictionary<string, object> contents = new Dictionary<string, object>();
            contents.Add("content", "notice test by gilbert");

            spider.GetChannels((success, result) =>
            {
                Assert.IsTrue(result is GetChannelsResult == true);

                GetChannelsResult getChannelsResult = result as GetChannelsResult;
                completion.Set();
            });

            completion.WaitOne();
        }

        

        private Hive5Spider Connect()
        {
            var completion = new ManualResetEvent(false);

            Hive5Spider spider = new Hive5Spider(this.apiClient);
            spider.Connected += (s, e) =>
                {
                    completion.Set();
                };
            spider.ConnectAsync();

            completion.WaitOne();

            return spider;
        }


        private void Login()
        {
            string userId = Hive5UnitTest.ValidUserId;
            string sdkVersion = Hive5UnitTest.GoogleSdkVersion;
            string[] objectKeys = new string[] { "" };		// 로그인 후 가져와야할 사용자 object의 key 목록
            string[] configKeys = new string[] { "time_event1" };	// 로그인 후 가져와야할 사용자 configuration의 key

            try
            {
                var completion = new ManualResetEvent(false);

                this.apiClient.Login(OSType.Android, objectKeys, configKeys, PlatformType.Google, userId, sdkVersion, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is LoginResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    LoginResponseBody body = response.ResultData as LoginResponseBody;
                    Assert.IsTrue(string.IsNullOrEmpty(body.AccessToken) == false); // 잘못된 아이디로 로그인했으니
                    Assert.IsTrue(body.Agreements != null);
                    Assert.IsTrue(body.CompletedMissions != null);
                    Assert.IsTrue(body.Configs != null);
                    Assert.IsTrue(body.MailboxNewItemCount >= 0);
                    Assert.IsTrue(body.Promotions != null);
                    Assert.IsTrue(body.UserId > 0); // 잘못된 아이디로 로그인했으니

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }
    }
}
