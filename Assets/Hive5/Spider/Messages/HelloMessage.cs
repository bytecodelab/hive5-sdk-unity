using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class HelloMessage : SpiderMessage
    {
        public Realm Realm { get; set; }

        public HelloDetail Detail { get; set; }

        public HelloMessage()
        {
            this.MessageCode = (int)WampMessageCode.HELLO;
        }

        public override string ToString()
        {
            string message = string.Format("[{0}, {1}, {2}]",
                this.MessageCode,
                this.Realm.ToString(),
                this.Detail.ToJson());

            return message;
        }
    }
}
