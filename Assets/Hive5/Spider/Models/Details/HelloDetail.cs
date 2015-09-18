using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class HelloDetail : MessageDetail
    {
        public string app_key { get; set; }

        public long channel_id{ get; set; }

        public string uuid { get; set; }

        public string auth_token { get; set; }

        public Dictionary<string, object> roles { get; private set; }

        public HelloDetail()
        {
            var roles = new Dictionary<string, object>();
            roles.Add("publisher", new Dictionary<string, object>());
            roles.Add("subscriber", new Dictionary<string, object>());
            this.roles = roles;
        }
    }
}
