using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class SpiderRealm
    {
        public string RealmUri { get; private set; }
    
        public SpiderRealm(string realmUrl)
        {
            RealmUri = realmUrl;
        }

        public override string ToString()
        {
            return RealmUri;
        }
    }
}
