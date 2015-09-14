using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    /// <summary>
    /// 서버점검 계획 모델 클래스
    /// </summary>
    public class Plan
    {
        /// <summary>
        /// 시작하는 시각
        /// </summary>
        public DateTime? StartAt { get; set; }

        /// <summary>
        /// 끝나는 시각
        /// </summary>
        public DateTime? EndAt { get; set; }

        /// <summary>
        /// 메시지
        /// </summary>
        public string Message { get; set; }
    }
}
