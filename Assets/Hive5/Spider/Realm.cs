using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class Realm
    {
        private static readonly string RealmStringFormat = "io.hive5.spider.realm.{0}";

        
        public long ChannelId { get; private set; }

        public Realm(long channelId)
        {
            this.ChannelId = channelId;
        }

        public override string ToString()
        {
            return string.Format(RealmStringFormat, this.ChannelId);
        }
    }
}
