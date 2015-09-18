using Hive5.Models;
using Hive5.Spider.Helpers;
using Hive5.Spider.Models;
// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace Hive5.Spider
{
    public delegate void SpiderCallback(bool success);
    public delegate void SubscribeCallback(bool success, long subscriptionId);
    public delegate void SendMessageCallback(bool success, long publicationId); // PublishCallback
    public delegate void CallResultCallback(bool success, CallResult result);

    public delegate void ErrorMessageEventHandler(object sender, ErrorMessage error);
    public delegate void MessageReceivedEventHandler(object sender, SpiderTopic topic, Dictionary<string, string> mesageContents);

    /// <summary>
    /// Hive5의 실시간 서버인 Spider API에 대응하는 클래스
    /// </summary>
    public class Hive5Spider
    {
        #region 프로퍼티들
        /// <summary>
        /// Kiter/Spider Version
        /// </summary>
        public const string Version = "v1";

        public string KiterHost { get; private set; }

        public SpiderServer SpiderServer { get; private set; }

        public CallbackManager CallbackManager { get; private set; }

        public string Realm { get; private set; }

        public long SessionId { get; set; }

        public bool IsConnected { get; set; }

        private SubscribeReserver _ConnectingSubscribeReserver;

        #endregion 프로퍼티들


        #region 멤버들

        private SpiderCallback _ConnectedCallback;
        private SpiderCallback connectedToWebSocketCallback;
        private SpiderCallback disconnectedCallback;

        private WebSocket mySocket { get; set; }

        public bool Initialized { get; private set; }

        #endregion 멤버들

        #region 생성자들


        public Hive5Spider()
        {
            CallbackManager = new CallbackManager();
        }

        public void Initialize(string kiterHost)
        {
            this.KiterHost = kiterHost;
            this.Initialized = true;
        }

        #endregion 생성자들


        #region 메서드들

        #region Hello

        private void Hello()
        {
            HelloMessage message = new HelloMessage()
            {
                Realm = new SpiderRealm(this.Realm),
                Detail = new HelloDetail()
                {
                    app_key = Hive5Client.AppKey,
                    auth_token = Hive5Client.Auth.AccessToken,
                    uuid = Hive5Client.Uuid,
                },
            };

            Logger.Log("Spider Hello: " + message.ToMessageString());
            this.SendAsync(message, helloCompleted);
        }

        private void helloCompleted(bool success)
        {
            if (success == false)
            {
                Logger.Log("Spider Hello 전송 실패 in HelloCompleted");
            }
            else
            {
                Logger.Log("Spider Hello 전송 성공 in HelloCompleted");
            }

            if (connectedToWebSocketCallback != null)
            {
                connectedToWebSocketCallback(success);
            }
        }

        #endregion Hello

        private void PickServers(SpiderCallback callback)
        {
            string requestUrl = System.IO.Path.Combine(this.KiterHost, string.Format("{0}/servers/pick", Hive5Spider.Version));

            Hive5Http.Instance.GetHttpAsync(requestUrl, null, PickServerResponseBody.Load, (response) =>
                {
                    var body = response.ResultData as PickServerResponseBody;

                    if (body == null || body.Server == null)
                    {
                        callback(false);
                    }
                    else
                    {
                        this.SpiderServer = body.Server;
                        this.Realm = body.Realm;
                        callback(true);
                    }
                });
        }

        private void ConnectToWebsocket(SpiderCallback callback)
        {
            connectedToWebSocketCallback = callback;
            mySocket.ConnectAsync();
        }

        public void Connect(SpiderCallback callback)
        {
            if (this.mySocket != null)
                this.mySocket.CloseAsync();

            _ConnectedCallback = callback;
            PickServers((result) =>
            {
                if (result == false)
                {
                    callback(false);
                    return;
                }

                this.mySocket = new WebSocket(this.SpiderServer.ToEndPoint());
                LinkEvents(this.mySocket);

                ConnectToWebsocket((result2) =>
                    {
                        if (result2 == false)
                            return;
                    });
            });
        }

        private void UnlinkEvents(WebSocket ws)
        {
            if (ws == null) throw new NullReferenceException("ws should not be null!");

            ws.OnOpen -= mySocket_OnOpen;
            ws.OnError -= mySocket_OnError;
            ws.OnMessage -= mySocket_OnMessage;
            ws.OnClose -= mySocket_OnClose;
        }

        private void LinkEvents(WebSocket ws)
        {
            if (ws == null) throw new NullReferenceException("ws should not be null!");

            UnlinkEvents(ws);
            ws.OnOpen += mySocket_OnOpen;
            ws.OnError += mySocket_OnError;
            ws.OnMessage += mySocket_OnMessage;
            ws.OnClose += mySocket_OnClose;
        }

        public void Subscribe(SpiderTopic topic, SubscribeCallback callback)
        {
            SubscribeMessage message = new SubscribeMessage()
                {
                    Topic = topic,
                };

            if (callback != null)
            {
                CallbackManager.SubscribeRequestIdToCallback.Add(message.RequestId, callback);
            }

            // RequestId를 TopicKind와 등록
            SubscriptionManager.ReportSubscribe(message.RequestId, topic);

            this.SendAsync(message, (success) =>
            {
                if (success == false)
                {
                    // 사전에서 제거
                    CallbackManager.SubscribeRequestIdToCallback.Remove(message.RequestId);
                    callback(false, -1);
                    Logger.Log("Spider Subscribe 전송 실패 in subscribeCompleted");
                }
                else
                {
                    Logger.Log("Spider Subscribe 전송 성공 in subscribeCompleted");
                }
            });
        }

        public void SendAsync(SpiderMessage message, Action<bool> completed)
        {
            if (mySocket == null)
                return;

            string rawMessage = message.ToMessageString();
            Logger.Log("▶ Message sent.\tRaw message = " + rawMessage);
            mySocket.SendAsync(rawMessage, completed);
        }


        public void Unsubscribe(long subscriptionId, SpiderCallback callback)
        {
            UnsubscribeMessage message = new UnsubscribeMessage()
            {
                SubscriptionId = subscriptionId,
            };

            if (callback != null)
            {
                CallbackManager.UnsubscribeRequestIdToCallback.Add(message.RequestId, callback);
            }

            this.SendAsync(message, (success) =>
            {
                if (success == false)
                {
                    // 사전에서 제거
                    CallbackManager.UnsubscribeRequestIdToCallback.Remove(message.RequestId);
                    callback(false);
                    Logger.Log("Spider Unsubscribe 전송 실패 in unsubscribeCompleted");
                }
                else
                {
                    Logger.Log("Spider Unsubscribe 전송 성공 in unsubscribeCompleted");
                }
            });
        }

        #region Publish

        ///// <summary>
        ///// 공지메세지 전송
        ///// </summary>
        ///// <param name="appSecret"></param>
        ///// <param name="contents"></param>
        //public void SendNoticeMessage(string appSecret, Dictionary<string, string> contents, SendMessageCallback callback)
        //{
        //    Publish(TopicKind.Notice, new NoticePublishOptions() { secret = appSecret }, contents, callback);
        //}

        ///// <summary>
        ///// 시스템메세지 전송
        ///// </summary>
        ///// <param name="contents"></param>
        //public void SendSystemMessage(Dictionary<string, string> contents, SendMessageCallback callback)
        //{
        //    Publish(TopicKind.System, new SystemPublishOptions(), contents, callback);
        //}

        ///// <summary>
        ///// 채널 안의 모두가 볼 수 있도록 메세지 전송
        ///// </summary>
        ///// <param name="contents"></param>
        //public void SendChannelMessage(Dictionary<string, string> contents, SendMessageCallback callback)
        //{
        //    Publish(TopicKind.Channel, new ChannelPublishOptions(), contents, callback);
        //}

        ///// <summary>
        ///// 채널 안의 특정 사람이 볼 수 있도록 전송
        ///// </summary>
        ///// <param name="contents"></param>
        //public void SendPrivateMessage(string platformUserId, Dictionary<string, string> contents, SendMessageCallback callback)
        //{
        //    Publish(TopicKind.Private, new PrivatePublishOptions() {  platform_user_id = platformUserId }, contents, callback);
        //}

        private void Publish(SpiderTopic topic, PublishOptions options, Dictionary<string, string> content, SendMessageCallback callback)
        {
            PublishMessage message = new PublishMessage()
            {
                TopicUri = topic.TopicUri,
                Options = options,
                Contents = content,
            };

            CallbackManager.PublishRequestIdToCallback.Add(message.RequestId, callback);

            Logger.Log("Spider Publish: " + message.ToMessageString());
            this.SendAsync(message, (success) =>
                {
                    if (success == false)
                    {
                        CallbackManager.PublishRequestIdToCallback.Remove(message.RequestId);
                        callback(false, -1);
                        Logger.Log("Spider Publish 전송 실패 in publishCompleted");
                    }
                    else
                    {
                        Logger.Log("Spider Publish 전송 성공 in publishCompleted");
                    }
                });
        }

        #endregion Publish

        public void GetChannels(CallResultCallback callback)
        {
            this.call(CallUris.GetChannels, null, CallResultKind.GetChannelsResult, callback);
        }


        public void GetPlayers(CallResultCallback callback)
        {
            this.call(CallUris.GetPlayers, null, CallResultKind.GetPlayersResult, callback);
        }

        private void call(string callUri, CallOptions options, CallResultKind resultKind, CallResultCallback callback)
        {
            if (options == null)
                options = new CallOptions();

            CallMessage callMessage = new CallMessage()
            {
                ProcedureUri = callUri,
                Options = options,
            };

            var callbackNode = new CallResultCallbackNode(resultKind, callback);
            CallbackManager.CallRequestIdToCallbackNode.Add(callMessage.RequestId, callbackNode);
            this.SendAsync(callMessage, (success) =>
            {
                if (success == false)
                {
                    CallbackManager.CallRequestIdToCallbackNode.Remove(callMessage.RequestId);
                    callbackNode.Callback(false, null);
                    Logger.Log("Spider call 전송 실패 in callCompleted");
                }
                else
                {
                    Logger.Log("Spider call 전송 성공 in callCompleted");
                }
            });
        }

        public void EnterZone(string zoneTopicUri, SubscribeCallback callback)
        {
            SpiderTopic zoneTopic = new SpiderTopic(zoneTopicUri);
            this.Subscribe(zoneTopic, callback);
        }

        public void SendToZone(string nickname, string message, string zoneTopicUri, SendMessageCallback callback)
        {
            var zoneTopic = new SpiderTopic(zoneTopicUri);
            var content = new Dictionary<string, string>();
            content.Add("nick", nickname);
            content.Add("content", message);
            this.Publish(zoneTopic, new NoticePublishOptions(), content, callback);
        }

        public void SendToUser(string nickname, string message, User user, SendMessageCallback callback)
        {
            var userTopic = SpiderTopic.CreateUserTopic(user);
            var content = new Dictionary<string, string>();
            content.Add("nick", nickname);
            content.Add("content", message);
            this.Publish(userTopic, new PrivatePublishOptions(), content, callback);
        }

        #region Disconnect(GoodBye)

        public void AddDisconnectCallback(SpiderCallback callback)
        {
            disconnectedCallback = callback;
        }

        public void Disconnect()
        {
            GoodbyeMessage message = new GoodbyeMessage();
            this.SendAsync(message, goodbyeCompleted);
        }

        private void goodbyeCompleted(bool success)
        {
            if (success == false)
            {
                Logger.Log("Spider Goodbye 전송 실패 in goodbyeCompleted");
            }
            else
            {
                Logger.Log("Spider Goodbye 전송 성공 in goodbyeCompleted");
            }
        }

        #endregion Disconnect(GoodBye)

        #endregion 메서드들


        #region 이벤트핸들러들


        void mySocket_OnClose(object sender, CloseEventArgs e)
        {
            Logger.Log("[OnClose]");
            OnClosed();
        }


        void mySocket_OnMessage(object sender, MessageEventArgs e)
        {
#if !UNITTEST
            Loom.RunAsync(new Action(() =>
            {
#endif
                Logger.Log("◀ Message received.\tRaw message = " + e.Data);
                SpiderMessage message = MessageParser.Parse(e.Data);
                if (message == null)
                {
                    Logger.Log("Message couldn't be parsed. " + e.Data);
                    return;
                }

                switch ((WampMessageCode)message.MessageCode)
                {
                    case WampMessageCode.HELLO:
                        break;
                    case WampMessageCode.WELCOME:
                        {
                            WelcomeMessage welcomeMessage = message as WelcomeMessage;
                            this.SessionId = welcomeMessage.SessionId;

                            _ConnectingSubscribeReserver = new SubscribeReserver(this, welcomeMessage.Topics, _ConnectedCallback);
                            foreach (var topic in welcomeMessage.Topics)
                            {
                                this.Subscribe(topic, (success, sid) =>
                                {
                                    if (success == true)
                                    {
                                        var subscribedTopic = SubscriptionManager.GetTopicBySubscriptionId(sid);
                                        _ConnectingSubscribeReserver.Confirm(subscribedTopic);
                                    }
                                });
                            }
                        }
                        break;
                    case WampMessageCode.ABORT:
                        this.IsConnected = false;
                        break;
                    case WampMessageCode.CHALLENGE:
                        break;
                    case WampMessageCode.AUTHENTICATE:
                        break;
                    case WampMessageCode.GOODBYE:
                        {
                            GoodbyeMessage goodbyeMessage = message as GoodbyeMessage;

                            this.IsConnected = false;

                            if (disconnectedCallback != null)
                            {
                                disconnectedCallback(true);
                                mySocket.CloseAsync(); // Close
                            }
                        }
                        break;
                    case WampMessageCode.HEARTBEAT:
                        break;
                    case WampMessageCode.ERROR:
                        {
                            ErrorMessage castedMessage = message as ErrorMessage;

                            switch ((WampMessageCode)castedMessage.MessageCodeOfError)
                            {
                                case WampMessageCode.SUBSCRIBE:
                                    {
                                        SubscribeCallback foundCallback = null;
                                        if (CallbackManager.SubscribeRequestIdToCallback.TryGetValue(castedMessage.RequestId, out foundCallback) == true)
                                        {
                                            foundCallback(false, -1);
                                        }
                                    }
                                    break;
                                case WampMessageCode.UNSUBSCRIBE:
                                    {
                                        SpiderCallback foundCallback = null;
                                        if (CallbackManager.UnsubscribeRequestIdToCallback.TryGetValue(castedMessage.RequestId, out foundCallback) == true)
                                        {
                                            foundCallback(false);
                                        }
                                    }
                                    break;
                                case WampMessageCode.PUBLISH:
                                    {
                                        SendMessageCallback foundCallback = null;
                                        if (CallbackManager.PublishRequestIdToCallback.TryGetValue(castedMessage.RequestId, out foundCallback) == true)
                                        {
                                            foundCallback(false, -1);
                                        }
                                    }
                                    break;
                                case WampMessageCode.CALL:
                                    {
                                        CallResultCallbackNode node = null;
                                        if (CallbackManager.CallRequestIdToCallbackNode.TryGetValue(castedMessage.RequestId, out node) == true)
                                        {
                                            node.Callback(false, new CallErrorResult()
                                                {
                                                    Error = castedMessage,
                                                });
                                        }
                                    }
                                    break;
                            }


                            OnError(castedMessage);
                        }
                        break;
                    case WampMessageCode.PUBLISH:
                        break;
                    case WampMessageCode.PUBLISHED:
                        {
#if LOADTEST
                            return;
#endif
                            PublishedMessage publishedMessage = message as PublishedMessage;

                            SendMessageCallback registeredCallback = null;
                            if (CallbackManager.PublishRequestIdToCallback.TryGetValue(publishedMessage.RequestId, out registeredCallback) == true)
                            {
                                CallbackManager.PublishRequestIdToCallback.Remove(publishedMessage.RequestId);
                                registeredCallback(true, publishedMessage.PublicationId);
                            }
                        }
                        break;
                    case WampMessageCode.SUBSCRIBE:
                        break;
                    case WampMessageCode.SUBSCRIBED:
                        {
                            SubscribedMessage castedMessage = message as SubscribedMessage;

                            SubscribeCallback registeredCallback = null;
                            //SendMessageCallback registeredCallbackTemp = null;

                            if (CallbackManager.SubscribeRequestIdToCallback.TryGetValue(castedMessage.RequestId, out registeredCallback) == true)
                            {
                                // RequestId를 통해 SubscriptionId와 TopicKind 연결
                                SubscriptionManager.ReportSubscribed(castedMessage.RequestId, castedMessage.SubscriptionId);

                                CallbackManager.SubscribeRequestIdToCallback.Remove(castedMessage.RequestId);
                                registeredCallback(true, castedMessage.SubscriptionId);
                            }
                            //// 임시코드 Publish를 호출해도 Subscribed가 와서 구현한 임시 루틴
                            //else if (CallbackManager.PublishRequestIdToCallback.TryGetValue(castedMessage.RequestId, out registeredCallbackTemp) == true)
                            //{
                            //    CallbackManager.PublishRequestIdToCallback.Remove(castedMessage.RequestId);
                            //    registeredCallbackTemp(true, castedMessage.SubscriptionId);
                            //}
                        }
                        break;
                    case WampMessageCode.UNSUBSCRIBE:
                        break;
                    case WampMessageCode.UNSUBSCRIBED:
                        {
                            UnsubscribedMessage castedMessage = message as UnsubscribedMessage;
                            SpiderCallback registeredCallback = null;
                            if (CallbackManager.UnsubscribeRequestIdToCallback.TryGetValue(castedMessage.RequestId, out registeredCallback) == true)
                            {
                                CallbackManager.UnsubscribeRequestIdToCallback.Remove(castedMessage.RequestId);
                                registeredCallback(true);
                            }
                        }
                        break;
                    case WampMessageCode.EVENT:
                        {
                            EventMessage castedMessage = message as EventMessage;
                            // subscriptionId를 통해 TopicKind 찾아오기
                            var topic = SubscriptionManager.GetTopicBySubscriptionId(castedMessage.SubscriptionId);
                            OnMessageReceived(topic, castedMessage.ArgumentsKw);
                        }
                        break;
                    case WampMessageCode.CALL:
                        break;
                    case WampMessageCode.CANCEL:
                        break;
                    case WampMessageCode.RESULT:
                        {
                            ResultMessage resultMessage = message as ResultMessage;

                            CallResultCallbackNode registeredCallbackNode = null;
                            if (CallbackManager.CallRequestIdToCallbackNode.TryGetValue(resultMessage.RequestId, out registeredCallbackNode) == true)
                            {
                                CallbackManager.CallRequestIdToCallbackNode.Remove(resultMessage.RequestId);

                                switch (registeredCallbackNode.Kind)
                                {
                                    default:
                                    case CallResultKind.Unknown:
                                        registeredCallbackNode.Callback(true, null);
                                        break;
                                    case CallResultKind.GetChannelsResult:
                                        {
                                            GetChannelsResult result = new GetChannelsResult(resultMessage);
                                            registeredCallbackNode.Callback(true, result);
                                        }
                                        break;
                                    case CallResultKind.GetPlayersResult:
                                        {
                                            GetPlayersResult result = new GetPlayersResult(resultMessage);
                                            registeredCallbackNode.Callback(true, result);
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case WampMessageCode.REGISTER:
                        break;
                    case WampMessageCode.REGISTERED:
                        break;
                    case WampMessageCode.UNREGISTER:
                        break;
                    case WampMessageCode.UNREGISTERED:
                        break;
                    case WampMessageCode.INVOCATION:
                        break;
                    case WampMessageCode.INTERRUPT:
                        break;
                    case WampMessageCode.YIELD:
                        break;
                    default:
                        break;
                }
#if !UNITTEST
            }));
#endif
        }

        //private void SubscribeAll()
        //{
        //    this.Subscribe(TopicKind.Channel, (success, subscriptionId) => { });
        //    this.Subscribe(TopicKind.Notice, (success, subscriptionId) => { });
        //    this.Subscribe(TopicKind.Private, (success, subscriptionId) => { });
        //    this.Subscribe(TopicKind.System, (success, subscriptionId) => { });
        //}

        void mySocket_OnError(object sender, ErrorEventArgs e)
        {
            Logger.Log("[OnError] " + e.Message);
        }

        void mySocket_OnOpen(object sender, EventArgs e)
        {
            this.Hello();
        }

        #endregion 이벤트핸들러들


        #region 이벤트들

        #region Error

        public event ErrorMessageEventHandler Error;

        private void OnError(ErrorMessage error)
        {
            if (Error == null)
                return;

            Error(this, error);
        }

        #endregion Error


        #region MessageReceived

        public event MessageReceivedEventHandler MessageReceived;

        private void OnMessageReceived(SpiderTopic topic, Dictionary<string, string> messageContents)
        {
            if (MessageReceived == null)
                return;

            MessageReceived(this, topic, messageContents);
        }

        #endregion MessageReceived


        #region Closed

        public event EventHandler Closed;

        private void OnClosed()
        {
            if (Closed == null)
                return;

            Closed(this, null);
        }

        #endregion Closed


        #endregion 이벤트들


    }
}

