using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class CallUris
    {
        private const string BaseUri = "io.hive5.spider.rpc";

        public static string GetChannels = BaseUri + "." + "list_channels";
        public static string GetPlayers = BaseUri + "." + "list_players";
    }

}
