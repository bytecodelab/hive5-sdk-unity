﻿using Hive5;
using Hive5.Models;
using Hive5.Spider;
using Hive5.Spider.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hive5_sdk_unity.test
{
    [TestClass]
    public class Hive5SpiderUnitTest
    {
        public static TestConfig TestValues { get; set; }
        public const string ZoneTopicUri = "io.hive5.spider.topic.zone.7.name1"; // "io.hive5.spider.topic.zone.7.testzone"
        private Hive5Spider _Spider;

        [TestInitialize]
        public void InitTest()
        {
            if (_Spider == null)
            {

            }

            TestValues = TestConfig.Default;
             
            Hive5Config.AppKey = TestValues.AppKey;
            Hive5Config.Host = TestValues.Host;
            Hive5Config.KiterHost = TestValues.KiterHost;

            Hive5Client.Initialize(TestValues.Uuid);                
            Login();
        }


        //[TestMethod, TestCategory("Kiter")]
        //public void TestPickServer()
        //{
        //    var completion = new ManualResetEvent(false);

        //    Hive5Client.Initialize(TestConfig.Default.Uuid);
        //    Hive5Spider spider = new Hive5Spider(TestConfig.Default.KiterHost);
            
        //    spider.PickServers((result) =>
        //        {
        //            completion.Set();
        //        });

        //    completion.WaitOne(); 
        //}
        [TestMethod]
        public void TestConnect()
        {
            var connected = Connect();
            Assert.AreEqual(connected, true);
        }

        private bool Connect()
        {
            var completion = new ManualResetEvent(false);
            
            bool connected = false;
            _Spider.Connect((success) =>
                {
                    connected = success;
                    completion.Set();
                });

            completion.WaitOne();
            return connected;
        }

        [TestMethod]
        public void TestSpiderTopicTryGetUser()
        {
            var topic = new SpiderTopic("io.hive5.spider.topic.user.none.23304330107154");

            var user = topic.TryGetUser();

            Assert.AreEqual(user.platform, "none");
            Assert.AreEqual(user.id, "23304330107154");
        }

        [TestMethod]
        public void TestSpiderTopicIdentifyingTopicKind()
        {
            var appTopic = new SpiderTopic("io.hive5.spider.topic.app.55");
            Assert.AreEqual(appTopic.TopicKind, TopicKind.App);

            var userTopic = new SpiderTopic("io.hive5.spider.topic.user.none.23304330107154");
            Assert.AreEqual(userTopic.TopicKind, TopicKind.User);

            var zoneTopic = new SpiderTopic("io.hive5.spider.topic.zone.7.name1");
            Assert.IsTrue(zoneTopic.TopicKind == TopicKind.Zone);

            var tempTopic = new SpiderTopic("io.hive5.spider.topic.gilbok");
            Assert.IsTrue(tempTopic.TopicKind == TopicKind.Temp);

            var notTopic = new SpiderTopic("io.hive5.spider.rpc");
            Assert.IsTrue(notTopic.TopicKind == TopicKind.NotTopic);
        }

        private void Login()
        {
            string userId = TestValues.TestUser.id;
          
            try
            {
                var completion = new ManualResetEvent(false);

                Hive5Client.Auth.LogIn("android", "1.0", "ko-KR", TestValues.TestUser, TestValues.PlatformParams, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is LoginResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    LoginResponseBody body = response.ResultData as LoginResponseBody;
                    Assert.IsTrue(string.IsNullOrEmpty(body.AccessToken) == false);
                    Assert.IsTrue(body.NewMailCount >= 0);
                    Assert.IsFalse(string.IsNullOrEmpty(body.User.platform));
                    Assert.IsFalse(string.IsNullOrEmpty(body.User.id)); 

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod]
        public void TestEnterZone()
        {
            var connected = Connect();
            Assert.AreEqual(connected, true);

            var sid = EnterZone(ZoneTopicUri);
            Assert.IsTrue(sid > 0);
        }

        private long EnterZone(string zoneTopicUri)
        {
            var completion = new ManualResetEvent(false);

            var connected = Connect();
            Assert.AreEqual(connected, true);

            long zoneTopicSubscriptionId = -1;

            _Spider.EnterZone(zoneTopicUri, (success, sid) =>
                {
                    if (success == true)
                    {
                        zoneTopicSubscriptionId = sid;
                    }
                    
                    completion.Set();
                });

            completion.WaitOne();

            return zoneTopicSubscriptionId;
        }

        [TestMethod]
        public void TestSendToZone()
        {
            var completion = new ManualResetEvent(false);

            var connected = Connect();
            Assert.AreEqual(connected, true);

            var sid = EnterZone(ZoneTopicUri);
            Assert.IsTrue(sid > 0);

            Dictionary<string, string> messagePairs = new Dictionary<string, string>();
            messagePairs.Add("nickname", "UnitTester");
            messagePairs.Add("content", "I'm testing a unit-test.");

            _Spider.SendToZone(messagePairs, ZoneTopicUri, (success, pid) => 
            {
                Assert.IsTrue(success);
                Assert.IsTrue(pid > 0);
                completion.Set();
            });

            completion.WaitOne();
        }

        [TestMethod]
        public void TestSendToUser()
        {
            var completion = new ManualResetEvent(false);

            var connected = Connect();
            Assert.AreEqual(connected, true);

            var sid = EnterZone(ZoneTopicUri);
            Assert.IsTrue(sid > 0);

            Dictionary<string, string> messagePairs = new Dictionary<string, string>();
            messagePairs.Add("nickname", "UnitTester");
            messagePairs.Add("content", "Hi, there!");
            _Spider.SendToUser(messagePairs, new User() { platform = "none", id = "515" }, (success, pid) =>
            {
                Assert.IsTrue(success);
                Assert.IsTrue(pid > 0);
                completion.Set();
            });

            completion.WaitOne();
        }

        //[TestMethod, TestCategory("Spider-Basic")]
        //public void TestConnect()
        //{
        //    var completion = new ManualResetEvent(false);

        //    Hive5Spider spider = new Hive5Spider(this.Hive5Client);
        //    spider.Connect((success) =>
        //        {
        //            Assert.IsTrue(spider.IsConnected == true);
        //            Assert.IsTrue(spider.SessionId > 0);
        //            completion.Set();
        //        });

        //    completion.WaitOne();
        //}

        //[TestMethod, TestCategory("Spider-Basic")]
        //public void TestDisconnect()
        //{
        //    Hive5Spider spider = Connect();

        //    var completion = new ManualResetEvent(false);

        //    spider.Disconnect((success) =>
        //        {
        //            Assert.IsTrue(spider.IsConnected == false);
        //            completion.Set();
        //        });

        //    completion.WaitOne();
        //}


        //[TestMethod, TestCategory("Spider-Publish")]
        //public void TestSendNoticeMessage()
        //{
        //    Hive5Spider spider = Connect();

        //    var completion = new ManualResetEvent(false);

        //    Dictionary<string, string> contents = new Dictionary<string, string>();
        //    contents.Add("content", "notice test by gilbert");

        //    spider.SendNoticeMessage("gogogo", contents, (success, publicationId) =>
        //    {
        //        Assert.IsTrue(publicationId > 0);
        //        Assert.IsTrue(success == true);
        //        completion.Set();
        //    });

        //    completion.WaitOne();
        //}

        //[TestMethod, TestCategory("Spider-Publish")]
        //public void TestSendSystemMessage()
        //{
        //    Hive5Spider spider = Connect();

        //    var completion = new ManualResetEvent(false);

        //    Dictionary<string, string> contents = new Dictionary<string, string>();
        //    contents.Add("content", "test system message by gilbert");

        //    spider.SendSystemMessage(contents, (success, publicationId) =>
        //    {
        //        Assert.IsTrue(publicationId > 0);
        //        Assert.IsTrue(success == true);
        //        completion.Set();
        //    });

        //    completion.WaitOne();
        //}

        //[TestMethod, TestCategory("Spider-Publish")]
        //public void TestSendPrivateMessage()
        //{
        //    Hive5Spider spider = Connect();

        //    var completion = new ManualResetEvent(false);

        //    Dictionary<string, string> contents = new Dictionary<string, string>();
        //    contents.Add("content", "test system message by gilbert");

        //    spider.SendPrivateMessage(TestValues.TestUser.id, contents, (success, publicationId) =>
        //    {
        //        Assert.IsTrue(publicationId > 0);
        //        Assert.IsTrue(success == true);
        //        completion.Set();
        //    });

        //    completion.WaitOne();
        //}

        //[TestMethod, TestCategory("Spider-Publish")]
        //public void TestSendChannelMessage()
        //{
        //    Hive5Spider spider = Connect();

        //    var completion = new ManualResetEvent(false);

        //    Dictionary<string, string> contents = new Dictionary<string, string>();
        //    contents.Add("content", "abcd");

        //    bool onceReceived = false;
        //    spider.MessageReceived += (sender, topicKind, messageContents) =>
        //        {
        //            if (onceReceived == true)
        //                return;

        //            onceReceived = true;

        //        };

        //    spider.SendChannelMessage(contents, (success, publicationId) =>
        //    {
        //        Assert.IsTrue(publicationId > 0);
        //        Assert.IsTrue(success == true);
        //        completion.Set();
        //    });


        //    completion.WaitOne();
        //}


        //[TestMethod, TestCategory("Spider-Subscribe")]
        //public void TestSubscribeTopicChannel()
        //{
        //    Hive5Spider spider = Connect();
        //    Subscribe(spider, TopicKind.Channel);

        //}

        //[TestMethod, TestCategory("Spider-Subscribe")]
        //public void TestSubscribeTopicNotice()
        //{
        //    Hive5Spider spider = Connect();
        //    Subscribe(spider, TopicKind.Notice);

        //}

        //[TestMethod, TestCategory("Spider-Subscribe")]
        //public void TestSubscribeTopicPrivate()
        //{
        //    Hive5Spider spider = Connect();
        //    Subscribe(spider, TopicKind.Private);

        //}

        //[TestMethod, TestCategory("Spider-Subscribe")]
        //public void TestSubscribeTopicSystem()
        //{
        //    Hive5Spider spider = Connect();
        //    Subscribe(spider, TopicKind.System);
        //}

        //public long Subscribe(Hive5Spider spider, TopicKind topicKind)
        //{
        //    // Topic, Channel
        //    var completion = new ManualResetEvent(false);

        //    long returnedSubscriptionId = -1;
        //    spider.Subscribe(topicKind, (success, subscriptionId) =>
        //    {
        //        Assert.IsTrue(success == true);
        //        Assert.IsTrue(subscriptionId > 0);

        //        returnedSubscriptionId = subscriptionId;
        //        completion.Set();
        //    });

        //    completion.WaitOne();

        //    return returnedSubscriptionId;
        //}

        //[TestMethod, TestCategory("Spider-Unsubscribe")]
        //public void TestUnsubscribeTopicChannel()
        //{
        //    Hive5Spider spider = Connect();

        //    long subscriptionId = Subscribe(spider, TopicKind.Channel);

        //    // Topic, Channel
        //    var completion = new ManualResetEvent(false);

        //    spider.Unsubscribe(subscriptionId, (success) =>
        //    {
        //        Assert.IsTrue(success == true);
        //        completion.Set();
        //    });

        //    completion.WaitOne();
        //}

        //[TestMethod, TestCategory("Spider-Event")]
        //public void TestEventChannel()
        //{
        //    Hive5Spider spider = Connect();

        //    long subscriptionId = Subscribe(spider, TopicKind.Channel);

        //    // Topic, Channel
        //    var completion = new ManualResetEvent(false);

        //    Dictionary<string, string> contents = new Dictionary<string, string>();

        //    string contentKey = "content";
        //    string contentValue = "test channel message for event";
        //    contents.Add(contentKey, contentValue);

        //    long returnedPublicationId = -1;

        //    // 다른 호출과 꼬임방지
        //    bool onceReceived = false;
        //    spider.MessageReceived += (sender, topicKind, messageContents) =>
        //        {
        //            if (onceReceived == true)
        //                return;

        //            onceReceived = true;

        //            Assert.IsTrue(topicKind == TopicKind.Channel);
        //            Assert.IsTrue(messageContents[contentKey] == contentValue);
        //            completion.Set();
        //        };

        //    spider.SendChannelMessage(contents, (success, publicationId) =>
        //    {
        //        Assert.IsTrue(publicationId > 0);
        //        Assert.IsTrue(success == true);

        //        returnedPublicationId = publicationId;
        //    });

        //    completion.WaitOne();
        //}

        //[TestMethod, TestCategory("Spider-Event")]
        //public void TestEventNotice()
        //{
        //    Hive5Spider spider = Connect();

        //    long subscriptionId = Subscribe(spider, TopicKind.Notice);

        //    // Topic, Channel
        //    var completion = new ManualResetEvent(false);

        //    Dictionary<string, string> contents = new Dictionary<string, string>();

        //    string contentKey = "content";
        //    string contentValue = "test notice message for event";
        //    contents.Add(contentKey, contentValue);

        //    long returnedPublicationId = -1;

        //    // 다른 호출과 꼬임방지
        //    bool onceReceived = false;
        //    spider.MessageReceived += (sender, topicKind, messageContents) =>
        //        {
        //            if (onceReceived == true)
        //                return;

        //            onceReceived = true;

        //            Assert.IsTrue(topicKind == TopicKind.Notice);
        //            Assert.IsTrue(messageContents[contentKey] == contentValue);
        //            completion.Set();
        //        };

        //    spider.SendNoticeMessage("gogogo", contents, (success, publicationId) =>
        //    {
        //        Assert.IsTrue(publicationId > 0);
        //        Assert.IsTrue(success == true);

        //        returnedPublicationId = publicationId;
        //    });

        //    completion.WaitOne();
        //}

        //[TestMethod, TestCategory("Spider-Event")]
        //public void TestEventSystem()
        //{
        //    Hive5Spider spider = Connect();

        //    long subscriptionId = Subscribe(spider, TopicKind.System);

        //    // Topic, Channel
        //    var completion = new ManualResetEvent(false);

        //    Dictionary<string, string> contents = new Dictionary<string, string>();

        //    string contentKey = "content";
        //    string contentValue = "test system message for event";
        //    contents.Add(contentKey, contentValue);

        //    long returnedPublicationId = -1;
        //    // 다른 호출과 꼬임방지
        //    bool onceReceived = false;
        //    spider.MessageReceived += (sender, topicKind, messageContents) =>
        //        {
        //            if (onceReceived == true)
        //                return;

        //            onceReceived = true;

        //            Assert.IsTrue(topicKind == TopicKind.System);
        //            Assert.IsTrue(messageContents[contentKey] == contentValue);
        //            completion.Set();
        //        };

        //    spider.SendSystemMessage(contents, (success, publicationId) =>
        //    {
        //        Assert.IsTrue(publicationId > 0);
        //        Assert.IsTrue(success == true);

        //        returnedPublicationId = publicationId;
        //    });

        //    completion.WaitOne();
        //}

        //[TestMethod, TestCategory("Spider-Event")]
        //public void TestEventPrivate()
        //{
        //    Hive5Spider spider = Connect();

        //    long subscriptionId = Subscribe(spider, TopicKind.Private);

        //    // Topic, Channel
        //    var completion = new ManualResetEvent(false);

        //    Dictionary<string, string> contents = new Dictionary<string, string>();

        //    string contentKey = "content";
        //    string contentValue = "test private message for event";
        //    contents.Add(contentKey, contentValue);

        //    long returnedPublicationId = -1;

        //    // 다른 호출과 꼬임방지
        //    bool onceReceived = false;
        //    spider.MessageReceived += (sender, topicKind, messageContents) =>
        //        {
        //            if (onceReceived == true)
        //                return;

        //            onceReceived = true;

        //            Assert.IsTrue(topicKind == TopicKind.Private);
        //            Assert.IsTrue(messageContents[contentKey] == contentValue);
        //            completion.Set();
        //        };

        //    spider.SendPrivateMessage(TestValues.TestUser.id, contents, (success, publicationId) =>
        //    {
        //        Assert.IsTrue(publicationId > 0);
        //        Assert.IsTrue(success == true);

        //        returnedPublicationId = publicationId;
        //    });

        //    completion.WaitOne();
        //}

        //[TestMethod, TestCategory("Spider-Call")]
        //public void TestGetChannels()
        //{
        //    Hive5Spider spider = Connect();

        //    var completion = new ManualResetEvent(false);

        //    spider.GetChannels((success, result) =>
        //    {
        //        Assert.IsTrue(success == true);
        //        Assert.IsTrue(result != null);
        //        Assert.IsTrue(result is GetChannelsResult == true);

        //        GetChannelsResult getChannelsResult = result as GetChannelsResult;
        //        Assert.IsTrue(getChannelsResult.Channels.Count > 0);
        //        Assert.IsTrue(getChannelsResult.Channels[0].app_id > 0);
        //        Assert.IsTrue(getChannelsResult.Channels[0].channel_number > 0);
        //        Assert.IsTrue(getChannelsResult.Channels[0].session_count >= 0);

        //        completion.Set();
        //    });

        //    completion.WaitOne();
        //}

        //[TestMethod, TestCategory("Spider-Call")]
        //public void TestGetPlayers()
        //{
        //    Hive5Spider spider = Connect();

        //    var completion = new ManualResetEvent(false);

        //    spider.Error += (sender, error) =>
        //        {
                    
        //        };


        //    spider.Closed += (sender, error) =>
        //        {
        //        };

        //    spider.GetPlayers((success, result) =>
        //    {
        //        Assert.IsTrue(success == true);
        //        Assert.IsTrue(result != null);
        //        Assert.IsTrue(result is GetPlayersResult == true);

        //        GetPlayersResult getChannelsResult = result as GetPlayersResult;
        //        Assert.IsTrue(getChannelsResult.PlatformUserIds.Count > 0);
        //        Assert.IsTrue(string.IsNullOrEmpty(getChannelsResult.PlatformUserIds[0]) == false);

        //        //completion.Set();
        //    });

        //    completion.WaitOne();
        //}

        //Hive5Spider usedSpider { get; set; }

        //private Hive5Spider Connect(bool newInstance = true)
        //{
        //    var completion = new ManualResetEvent(false);

        //    Hive5Spider spider = null;
        //    if (newInstance == true)
        //    {
        //        spider = new Hive5Spider(this.Hive5Client);
        //    }
        //    else
        //    {
        //        if (this.usedSpider == null)
        //        {
        //            this.usedSpider = new Hive5Spider(this.Hive5Client);
        //        }

        //        spider = this.usedSpider;
        //    }
        //    spider.Connect((success) =>
        //        {
        //            completion.Set();
        //        });

        //    completion.WaitOne();

        //    return spider;
        //}

        
    }
}
