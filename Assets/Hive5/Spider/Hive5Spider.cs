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

namespace Hive5
{
    public delegate void SpiderCallback(bool success);
    public delegate void SubscribeCallback(bool success, long subscriptionId);
    public delegate void SendMessageCallback(bool success, long publicationId); // PublishCallback
    public delegate void CallResultCallback(bool success, CallResult result);

    public delegate void ErrorMessageEventHandler(object sender, ErrorMessage error);
    public delegate void MessageReceivedEventHandler(object sender, TopicKind topicKind, Dictionary<string, string> mesageContents);

    /// <summary>
    /// Hive5의 Spider API에 대응하는 클래스
    /// </summary>
    public class Hive5Spider
    {
        #region 프로퍼티들

        public long SessionId { get; set; }

        public bool IsConnected { get; set; }

        #endregion 프로퍼티들


        #region 멤버들

        private const string RouterEndPoint = "ws://beta.spider.hive5.io:9201/channels/ws";

        private SpiderCallback connectedCallback;
        private SpiderCallback disconnectedCallback;

        private Hive5Client hive5Client { get; set; }
        private WebSocket mySocket { get; set; }

        #endregion 멤버들

        #region 생성자들

        public Hive5Spider(Hive5Client client)
        {
            if (client == null)
                throw new NullReferenceException();

            this.mySocket = new WebSocket(Hive5Spider.RouterEndPoint);
            this.mySocket.OnOpen += mySocket_OnOpen;
            this.mySocket.OnError += mySocket_OnError;
            this.mySocket.OnMessage += mySocket_OnMessage;
            this.mySocket.OnClose += mySocket_OnClose;

            this.hive5Client = client;
        }

        #endregion 생성자들


        #region 메서드들




        #region Hello


        public void Hello(long channelId)
        {
            HelloMessage message = new HelloMessage()
            {
                Realm = new Realm(channelId),
                Detail = new HelloDetail()
                {
                    app_key = this.hive5Client.AppKey,
                    auth_token = this.hive5Client.AccessToken,
                    channel_id = channelId,
                    uuid = this.hive5Client.Uuid,
                },
            };

            string jsonMessage = message.ToJson();
            Logger.Log("Spider Hello: " + jsonMessage);
            this.SendAsync(jsonMessage, helloCompleted);
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
        }

        #endregion Hello


        public void Connect(SpiderCallback callback)
        {
            connectedCallback = callback;
            mySocket.ConnectAsync();
        }

        public void Subscribe(TopicKind topicKind, SubscribeCallback callback)
        {
            SubscribeMessage message = new SubscribeMessage()
                {
                    TopicKind = topicKind,
                };

            if (callback != null)
            {
                CallbackManager.SubscribeRequestIdToCallback.Add(message.RequestId, callback);
            }
            string jsonMessage = message.ToJson();
            this.SendAsync(jsonMessage, (success) =>
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

        public void SendAsync(string message, Action<bool> completed)
        {
            if (mySocket == null)
                return;

            Logger.Log("▶ Message sent.\tRaw message = " + message);
            mySocket.SendAsync(message, completed);
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

            string jsonMessage = message.ToJson();
            this.SendAsync(jsonMessage, (success) =>
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

        /// <summary>
        /// 공지메세지 전송
        /// </summary>
        /// <param name="appSecret"></param>
        /// <param name="contents"></param>
        public void SendNoticeMessage(string appSecret, Dictionary<string, string> contents, SendMessageCallback callback)
        {
            Publish(TopicKind.Notice, new NoticePublishOptions() { secret = appSecret }, contents, callback);
        }

        /// <summary>
        /// 시스템메세지 전송
        /// </summary>
        /// <param name="contents"></param>
        public void SendSystemMessage(Dictionary<string, string> contents, SendMessageCallback callback)
        {
            Publish(TopicKind.System, new SystemPublishOptions(), contents, callback);
        }

        /// <summary>
        /// 채널 안의 모두가 볼 수 있도록 메세지 전송
        /// </summary>
        /// <param name="contents"></param>
        public void SendChannelMessage(Dictionary<string, string> contents, SendMessageCallback callback)
        {
            Publish(TopicKind.Channel, new ChannelPublishOptions(), contents, callback);
        }

        /// <summary>
        /// 채널 안의 특정 사람이 볼 수 있도록 전송
        /// </summary>
        /// <param name="contents"></param>
        public void SendPrivateMessage(Dictionary<string, string> contents, SendMessageCallback callback)
        {
            Publish(TopicKind.Private, new PrivatePublishOptions(), contents, callback);
        }


        private void Publish(TopicKind kind, PublishOptions options, Dictionary<string, string> content, SendMessageCallback callback)
        {
            PublishMessage message = new PublishMessage()
            {
                TopicUri = TopicUris.GetTopicUri(kind),
                Options = options,
                Contents = content,
            };

            CallbackManager.PublishRequestIdToCallback.Add(message.RequestId, callback);

            string jsonMessage = message.ToJson();
            Logger.Log("Spider Publish: " + jsonMessage);
            this.SendAsync(jsonMessage, (success) =>
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

            string jsonMessage = callMessage.ToJson();

            var callbackNode = new CallResultCallbackNode(resultKind, callback);
            CallbackManager.CallRequestIdToCallbackNode.Add(callMessage.RequestId, callbackNode);
            this.SendAsync(jsonMessage, (success) =>
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

        #region Disconnect(GoodBye)

        public void Disconnect(SpiderCallback callback)
        {
            GoodbyeMessage message = new GoodbyeMessage();
            string jsonMessage = message.ToJson();

            disconnectedCallback = callback;
            this.SendAsync(jsonMessage, goodbyeCompleted);
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
        }

        void mySocket_OnMessage(object sender, MessageEventArgs e)
        {
            Logger.Log("◀ Message received.\tRaw message = " + e.Data);
            SpiderMessage message = MessageParser.Parse(e.Data);
            if (message == null)
                return;

            switch ((WampMessageCode)message.MessageCode)
            {
                case WampMessageCode.HELLO:
                    break;
                case WampMessageCode.WELCOME:
                    {
                        WelcomeMessage welcomeMessage = message as WelcomeMessage;
                        this.SessionId = welcomeMessage.SessionId;
                        this.IsConnected = true;

                        // Subscribe들
                        this.SubscribeAll();

                        if (connectedCallback != null)
                        {
                            connectedCallback(true);
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
                                                 Error =  castedMessage,
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
                        SendMessageCallback registeredCallbackTemp = null;

                        if (CallbackManager.SubscribeRequestIdToCallback.TryGetValue(castedMessage.RequestId, out registeredCallback) == true)
                        {
                            CallbackManager.SubscribeRequestIdToCallback.Remove(castedMessage.RequestId);
                            registeredCallback(true, castedMessage.SubscriptionId);
                        }
                        // 임시코드 Publish를 호출해도 Subscribed가 와서 구현한 임시 루틴
                        else if (CallbackManager.PublishRequestIdToCallback.TryGetValue(castedMessage.RequestId, out registeredCallbackTemp) == true)
                        {
                            CallbackManager.PublishRequestIdToCallback.Remove(castedMessage.RequestId);
                            registeredCallbackTemp(true, castedMessage.SubscriptionId);
                        }
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
                        OnMessageReceived(castedMessage.GetTopicKind(), castedMessage.ArgumentsKw);
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

        }

        private void SubscribeAll()
        {
            this.Subscribe(TopicKind.Channel, (success, subscriptionId) => { });
            this.Subscribe(TopicKind.Notice, (success, subscriptionId) => { });
            this.Subscribe(TopicKind.Private, (success, subscriptionId) => { });
            this.Subscribe(TopicKind.System, (success, subscriptionId) => { });
        }

        void mySocket_OnError(object sender, ErrorEventArgs e)
        {

        }

        void mySocket_OnOpen(object sender, EventArgs e)
        {
            this.Hello(1);
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

        private void OnMessageReceived(TopicKind topicKind, Dictionary<string, string> messageContents)
        {
            if (MessageReceived == null)
                return;

            MessageReceived(this, topicKind, messageContents);
        }

        #endregion MessageReceived



        #endregion 이벤트들
    }
}

