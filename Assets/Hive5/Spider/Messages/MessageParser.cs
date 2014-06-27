using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class MessageParser
    {
        public static SpiderMessage Parse(string s)
        {
            JsonData json = JsonMapper.ToObject(s);
            if (json.Count == 0)
                return null;

            if (json[0].IsInt == false)
                return null;

            int messageCode = (int)json[0];

            switch ((WampMessageCode)messageCode)
            {
                case WampMessageCode.HELLO:
                    break;
                case WampMessageCode.WELCOME:
                    return WelcomeMessage.Parse(s);
                case WampMessageCode.ABORT:
                    break;
                case WampMessageCode.CHALLENGE:
                    break;
                case WampMessageCode.AUTHENTICATE:
                    break;
                case WampMessageCode.GOODBYE:
                    return GoodbyeMessage.Parse(s);
                case WampMessageCode.HEARTBEAT:
                    break;
                case WampMessageCode.ERROR:
                    break;
                case WampMessageCode.PUBLISH:
                    break;
                case WampMessageCode.PUBLISHED:
                    return PublishedMessage.Parse(s);
                case WampMessageCode.SUBSCRIBE:
                    break;
                case WampMessageCode.SUBSCRIBED:
                    break;
                case WampMessageCode.UNSUBSCRIBE:
                    break;
                case WampMessageCode.UNSUBSCRIBED:
                    break;
                case WampMessageCode.EVENT:
                    break;
                case WampMessageCode.CALL:
                    break;
                case WampMessageCode.CANCEL:
                    break;
                case WampMessageCode.RESULT:
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

            return null;
        }
    }
}
