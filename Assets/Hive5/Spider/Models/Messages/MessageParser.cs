using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class MessageParser
    {
        public static SpiderMessage Parse(string json)
        {
			JsonData jsonData = JsonMapper.ToObject<LitJson.JsonData>(json);
            if (jsonData.Count == 0)
                return null;

            if (jsonData[0].IsInt == false)
                return null;

            int messageCode = (int)jsonData[0];

            switch ((WampMessageCode)messageCode)
            {
                case WampMessageCode.HELLO:
                    break;
                case WampMessageCode.WELCOME:
                    return WelcomeMessage.Parse(json);
                case WampMessageCode.ABORT:
                    break;
                case WampMessageCode.CHALLENGE:
                    break;
                case WampMessageCode.AUTHENTICATE:
                    break;
                case WampMessageCode.GOODBYE:
                    return GoodbyeMessage.Parse(json);
                case WampMessageCode.HEARTBEAT:
                    break;
                case WampMessageCode.ERROR:
                    return ErrorMessage.Parse(json);
                case WampMessageCode.PUBLISH:
                    break;
                case WampMessageCode.PUBLISHED:
                    return PublishedMessage.Parse(json);
                case WampMessageCode.SUBSCRIBE:
                    break;
                case WampMessageCode.SUBSCRIBED:
                    return SubscribedMessage.Parse(json);
                case WampMessageCode.UNSUBSCRIBE:
                    break;
                case WampMessageCode.UNSUBSCRIBED:
                    return UnsubscribedMessage.Parse(json);
                case WampMessageCode.EVENT:
                    return EventMessage.Parse(json);
                case WampMessageCode.CALL:
                    break;
                case WampMessageCode.CANCEL:
                    break;
                case WampMessageCode.RESULT:
                    return ResultMessage.Parse(json);
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

            return null;
        }
    }
}
