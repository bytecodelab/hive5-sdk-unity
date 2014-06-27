using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public abstract class SpiderRequestMessage : SpiderMessage
    {
        public long RequestId { get; private set; }

        private static long requestIdAutoIncrementor = 1;

        public SpiderRequestMessage()
        {
            this.RequestId = getRequestId();
        }

        private long getRequestId()
        {
            return requestIdAutoIncrementor++;
        }

    }
}
