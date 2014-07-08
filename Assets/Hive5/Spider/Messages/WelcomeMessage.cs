using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class WelcomeMessage : SpiderMessage
    {
        public long SessionId { get; set; }


        public WelcomeMessage()
        {
            this.MessageCode = (int)WampMessageCode.WELCOME;
        }

        public static WelcomeMessage Parse(string s)
        {
            if (string.IsNullOrEmpty(s) == true)
                return null;

            var parts = LitJson.JsonMapper.ToObject<List<object>>(s);

            if (parts.Count != 3)
                return null;

            if (parts[1] is long == false)
                return null;

            long sessionId = (long)parts[1];
           
            var instance = new WelcomeMessage()
            {
                 SessionId = sessionId,
            };

            return instance;
        }
    }
}
