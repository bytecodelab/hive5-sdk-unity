using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public abstract class SpiderMessage
    {
        public int MessageCode { get; protected set; }

        public virtual string ToMessageString()
        {
            throw new NotImplementedException("해당 클래스에서 ToJson의 overrider가 필요합니다.");
        }
    }
}
