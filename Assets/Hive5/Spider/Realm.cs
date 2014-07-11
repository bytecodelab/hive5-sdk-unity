using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class Realm
    {
        private static readonly string RealmStringFormat = "io.hive5.spider.realm.{0}";

        
        public long ChannelNumber { get; private set; }

        public Realm(long channelNumber)
        {
            this.ChannelNumber = channelNumber;
        }

        public override string ToString()
        {
            return string.Format(RealmStringFormat, this.ChannelNumber);
        }
    }
}
