using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hive5;
using UnityEngine;
using System.Threading;

namespace maui_sdk.test
{
    [TestClass]
    public class UnitTest1
    {
        public Hive5APIZone TestZone { get; set; }
        public Hive5Client ApiClient { get; set; }

        [TestInitialize]
        public void InitializeTests()
        {
            this.TestZone = Hive5APIZone.Beta;

            var client = Hive5Client.Instance;
            client.SetDebug();
            string appKey = "a40e4122-99d9-44a6-b916-68ed756f79d6";
            string uuid = "747474747";

            try
            {
                client.Init(appKey, uuid, this.TestZone);
                ApiClient = client;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestLoginSuccess()
        {
            string userId 		= "88197948207226176";	
            string sdkVersion 	= "3";		
       		string[] objectKeys 	= new string[] {""};		// 로그인 후 가져와야할 사용자 object의 key 목록
    		string[] configKeys 	= new string[] {"time_event1"};	// 로그인 후 가져와야할 사용자 configuration의 key

            try
            {
                var completion = new ManualResetEvent(false);

                this.ApiClient.Login(OSType.Android, objectKeys, configKeys, PlatformType.Google, userId, sdkVersion, (response) => 
                {
                    Assert.AreEqual(Hive5ResultCode.Success, response.ResultCode);
                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestWWW()
        {
            // ECall Exception
            //WWW www = new WWW("http://google.com", (byte[])null);
        }
    }



    public class MonoBehaviour
    {

    }
}
