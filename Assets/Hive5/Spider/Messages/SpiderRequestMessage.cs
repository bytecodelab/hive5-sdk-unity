using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Hive5
{
    /// <summary>
    /// 스파이더 요청 메시지의 추상클래스
    /// </summary>
    public abstract class SpiderRequestMessage : SpiderMessage
    {
        /// <summary>
        /// 요청 고유아이디
        /// </summary>
        public long RequestId { get; private set; }
       
        /// <summary>
        /// 기본생성자
        /// </summary>
        public SpiderRequestMessage() : base()
        {
            this.RequestId = GetNextId();
        }

        private static Random _Random = new Random();

        private static long _Counter = 0;

        private static long GetNextId() {
            return Interlocked.Increment(ref _Counter);
        }
    }
}
