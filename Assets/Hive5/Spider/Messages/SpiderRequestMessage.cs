using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public abstract class SpiderRequestMessage : SpiderMessage
    {
        public long RequestId { get; private set; }

        //private static long requestIdAutoIncrementor = 1;

        public SpiderRequestMessage()
        {
            this.RequestId = getRequestId();
        }

        private static Random _random = new Random();

        private long getRequestId()
        {
            //return requestIdAutoIncrementor++;

            return this.LongRandom(100000000000000000, long.MaxValue, _random);
        }

        
        private long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
    }
}
