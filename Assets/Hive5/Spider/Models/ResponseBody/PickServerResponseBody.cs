using Hive5.Model;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class PickServerResponseBody : CommonResponseBody
    {
        public SpiderServer Server { get; set; }
        public string Realm { get; set; }

        public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			return new PickServerResponseBody() {
                Server =  SpiderServer.Load(json["server"]),
                Realm = (string)json["realm"],
			};
		}
    }
}
