using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class GoodbyeMessage : SpiderMessage
    {
        public string ReasonUri { get; set; }

        public GoodbyeMessage() : base()
        {
            this.MessageCode = (int)WampMessageCode.GOODBYE;
        }

        public override string ToMessageString()
        {
            // format 오류발생
            //return string.Format("[{0}, {}, {}]", (int)WampMessageCode.GOODBYE);

            int code = (int)WampMessageCode.GOODBYE;
            return "[" + code.ToString() + ", {}, \"wamp.error.goodbye_and_out\"]";
        }

        public static GoodbyeMessage Parse(string s)
        {
            if (string.IsNullOrEmpty(s) == true)
                return null;

            var parts = LitJson.JsonMapper.ToObject<List<object>>(s);

            if (parts.Count != 3)
                return null;


            string reasonUri = (string)parts[2];

            var instance = new GoodbyeMessage()
            {
                ReasonUri = reasonUri,
            };

            return instance;
        }
    }
}
