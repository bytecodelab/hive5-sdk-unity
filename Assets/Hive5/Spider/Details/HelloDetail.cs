using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class HelloDetail : MessageDetail
    {
        public string app_key { get; set; }

        public long channel_id{ get; set; }

        public string uuid { get; set; }

        public string auth_token { get; set; }

        //public string roles { get; private set; }

        public HelloDetail()
        {
            //this.roles = "{\"publisher\": {},\"subscriber\": {}}";
        }
    }
}
