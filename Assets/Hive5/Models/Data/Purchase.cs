using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Models
{
    /// <summary>
    /// 결제 모델 클래스
    /// </summary>
    public class Purchase
    {
        /// <summary>
        /// 고유아이디
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 상품코드
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 상태
        /// </summary>
        public string Status { get; set; }
    }
}
