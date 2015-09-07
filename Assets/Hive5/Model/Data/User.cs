using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Model
{
    /// <summary>
    /// 사용자 모델 클래스
    /// </summary>
    public class User
    {
        /// <summary>
        /// 플랫폼
        /// </summary>
        public string platform { get; set; }
        /// <summary>
        /// 플랫폼 안에서의 고유아이디
        /// </summary>
        public string id { get; set; }
    }
}
