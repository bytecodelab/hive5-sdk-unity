using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class GetChannelsResult : CallResult
    {
        public List<SpiderChannel> Channels { get; set; }

    }
}
