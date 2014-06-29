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
            var completion = new ManualResetEvent(false);

            Hive5Spider spider = new Hive5Spider(this.apiClient);
            spider.Connect((success) =>
                {
                    Assert.IsTrue(spider.IsConnected == true);
                    Assert.IsTrue(spider.SessionId > 0);
                    completion.Set();
                });

            completion.WaitOne();
        }

        [TestMethod, TestCategory("Spider-Basic")]
        public void TestDisconnect()
        {
            Hive5Spider spider = Connect();

            var completion = new ManualResetEvent(false);

            spider.Disconnect((success) =>
                {
                    Assert.IsTrue(spider.IsConnected == false);
                    completion.Set();
                });

            completion.WaitOne();
        }


        [TestMethod, TestCategory("Spider-Publish")]
        public void TestSendNoticeMessage()
        {
            Assert.Inconclusive("서버가 Publish에 대해서도 Subscribed를 반환한다.");
            return;

            Hive5Spider spider = Connect();

            var completionPre = new ManualResetEvent(false);
            spider.Subscribe(TopicKind.Notice, (success, subsciptionId) =>
            {
                Assert.IsTrue(success == true);
                Assert.IsTrue(subsciptionId > 0);
                completionPre.Set();
            });

            completionPre.WaitOne();


            var completion = new ManualResetEvent(false);

            Dictionary<string, string> contents = new Dictionary<string, string>();
            contents.Add("content", "notice test by gilbert");

            spider.SendNoticeMessage("gogogo", contents, (success, publicationId) =>
            {
                Assert.IsTrue(publicationId > 0);
                Assert.IsTrue(success == true);
                completion.Set();
            });

            completion.WaitOne();
        }


        [TestMethod, TestCategory("Spider-Subscribe")]
        public void TestSubscribeTopicChannel()
        {
            Hive5Spider spider = Connect();
            Subscribe(spider, TopicKind.Channel);

        }

        [TestMethod, TestCategory("Spider-Subscribe")]
        public void TestSubscribeTopicNotice()
        {
            Hive5Spider spider = Connect();
            Subscribe(spider, TopicKind.Notice);

        }

        [TestMethod, TestCategory("Spider-Subscribe")]
        public void TestSubscribeTopicPrivate()
        {
            Hive5Spider spider = Connect();
            Subscribe(spider, TopicKind.Private);

        }

        [TestMethod, TestCategory("Spider-Subscribe")]
        public void TestSubscribeTopicSystem()
        {
            Hive5Spider spider = Connect();
            Subscribe(spider, TopicKind.System);
        }

        public long Subscribe(Hive5Spider spider, TopicKind topicKind)
        {
            // Topic, Channel
            var completion = new ManualResetEvent(false);

            long returnedSubscriptionId = -1;
            spider.Subscribe(topicKind, (success, subscriptionId) =>
            {
                Assert.IsTrue(success == true);
                Assert.IsTrue(subscriptionId > 0);

                returnedSubscriptionId = subscriptionId;
                completion.Set();
            });

            completion.WaitOne();

            return returnedSubscriptionId;
        }

        [TestMethod, TestCategory("Spider-Unsubscribe")]
        public void TestUnsubscribeTopicChannel()
        {
            Hive5Spider spider = Connect();

            long subscriptionId = Subscribe(spider, TopicKind.Channel);

            // Topic, Channel
            var completion = new ManualResetEvent(false);

            spider.Unsubscribe(subscriptionId, (success) =>
            {
                Assert.IsTrue(success == true);
                completion.Set();
            });

            completion.WaitOne();
        }

        [TestMethod, TestCategory("Spider-Event")]
        public void TestEventChannel()
        {
            Hive5Spider spider = Connect();

            long subscriptionId = Subscribe(spider, TopicKind.Channel);

            // Topic, Channel
            var completion = new ManualResetEvent(false);

            Dictionary<string, string> contents = new Dictionary<string, string>();

            string contentKey = "content";
            string contentValue = "test channel message for event";
            contents.Add(contentKey, contentValue);

            long returnedPublicationId = -1;

            spider.MessageReceived += (sender, topicKind, messageContents) =>
                {
                    Assert.IsTrue(messageContents[contentKey] == contentValue);
                    completion.Set();
                };

            spider.SendChannelMessage(contents, (success, publicationId) =>
            {
                Assert.IsTrue(publicationId > 0);
                Assert.IsTrue(success == true);

                returnedPublicationId = publicationId;
            });

            completion.WaitOne();
        }

        [TestMethod, TestCategory("Spider-Event")]
        public void TestEventNotice()
        {
            Hive5Spider spider = Connect();

            long subscriptionId = Subscribe(spider, TopicKind.Notice);

            // Topic, Channel
            var completion = new ManualResetEvent(false);

            Dictionary<string, string> contents = new Dictionary<string, string>();

            string contentKey = "content";
            string contentValue = "test channel message for event";
            contents.Add(contentKey, contentValue);

            long returnedPublicationId = -1;

            spider.MessageReceived += (sender, topicKind, messageContents) =>
                {
                    Assert.IsTrue(messageContents[contentKey] == contentValue);
                    completion.Set();
                };

            spider.SendChannelMessage(contents, (success, publicationId) =>
            {
                Assert.IsTrue(publicationId > 0);
                Assert.IsTrue(success == true);

                returnedPublicationId = publicationId;
            });

            completion.WaitOne();
        }

        [TestMethod, TestCategory("Spider-Call")]
        public void TestGetChannels()
        {
            Hive5Spider spider = Connect();

            var completion = new ManualResetEvent(false);

            spider.GetChannels((success, result) =>
            {
                Assert.IsTrue(success == true);
                Assert.IsTrue(result != null);
                Assert.IsTrue(result is GetChannelsResult == true);

                GetChannelsResult getChannelsResult = result as GetChannelsResult;
                Assert.IsTrue(getChannelsResult.Channels.Count > 0);
                Assert.IsTrue(getChannelsResult.Channels[0].id > 0);
                Assert.IsTrue(getChannelsResult.Channels[0].session_count >= 0);

                completion.Set();
            });

            completion.WaitOne();
        }

        [TestMethod, TestCategory("Spider-Call")]
        public void TestGetPlayers()
        {
            Hive5Spider spider = Connect();

            var completion = new ManualResetEvent(false);

            spider.GetPlayers((success, result) =>
            {
                Assert.IsTrue(success == true);
                Assert.IsTrue(result != null);
                Assert.IsTrue(result is GetPlayersResult == true);

                GetPlayersResult getChannelsResult = result as GetPlayersResult;
                Assert.IsTrue(getChannelsResult.PlatformUserIds.Count > 0);
                Assert.IsTrue(string.IsNullOrEmpty(getChannelsResult.PlatformUserIds[0]) == false);

                completion.Set();
            });

            completion.WaitOne();
        }

        Hive5Spider usedSpider { get; set; }

        private Hive5Spider Connect(bool newInstance = true)
        {
            var completion = new ManualResetEvent(false);

            Hive5Spider spider = null;
            if (newInstance == true)
            {
                spider = new Hive5Spider(this.apiClient);
            }
            else
            {
                if (this.usedSpider == null)
                {
                    this.usedSpider = new Hive5Spider(this.apiClient);
                }

                spider = this.usedSpider;
            }
            spider.Connect((success) =>
                {
                    completion.Set();
                });

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
