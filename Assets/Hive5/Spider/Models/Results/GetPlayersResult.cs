using Hive5.Spider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider
{
    public class GetPlayersResult : CallResult
    {
        public List<string> PlatformUserIds { get; set; }

        public GetPlayersResult(ResultMessage resultMessage)
        {
            Load(resultMessage);
        }

        protected override void Load(ResultMessage message)
        {
            PlatformUserIds = new List<string>();

            foreach (var json in message.Arguments)
            {
                PlatformUserIds.Add((string)json);
            }
        }
    }
}
