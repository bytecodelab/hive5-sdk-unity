using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class SpiderServer
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public string State { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static SpiderServer Load(JsonData json)
        {
            try
            {
                SpiderServer server = new SpiderServer()
                {
                    IP = (string)json["ip"],
                    Port = json["port"].ToInt(),
                    State = (string)json["state"],
                    UpdatedAt = DateTime.Parse((string)json["updated_at"])
                };

                return server;
            }
            catch
            {
                return null;
            }
        }

        public string ToEndPoint()
        {
            return string.Format("ws://{0}:{1}/ws", this.IP, this.Port);
        }
    }
}
