using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public abstract class SpiderRequestMessage : SpiderMessage
    {
        public long RequestId { get; private set; }

        public SpiderRequestMessage()
        {
            //this.RequestId = getRequestId();
            this.RequestId = GetNextId();
        }
       
        private static long lastTick = 0;
        private static object idGenLock = new Object();
        private static long GetNextId() {
          lock (idGenLock) {
            long tick = DateTime.UtcNow.Ticks;
            if (lastTick == tick) {
              tick = lastTick+1;
            }
            lastTick = tick;
            return tick;
          }
        }

        //private long getRequestId()
        //{
        //    return this.LongRandom(100000000000000000, long.MaxValue, _random);
        //}

        //private static Random _random = new Random();

        //private long LongRandom(long min, long max, Random rand)
        //{
        //    byte[] buf = new byte[8];
        //    rand.NextBytes(buf);
        //    long longRand = BitConverter.ToInt64(buf, 0);

        //    return (Math.Abs(longRand % (max - min)) + min);
        //}
    }
}
