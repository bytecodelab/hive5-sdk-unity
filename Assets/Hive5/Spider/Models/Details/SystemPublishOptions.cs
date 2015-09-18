using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class SystemPublishOptions : PublishOptions
    {
        public string secret { get; private set; }

        public SystemPublishOptions()
        {
            this.secret = "nothing_special_2014";
        }
    }
}
