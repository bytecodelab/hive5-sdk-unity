using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public abstract class MessageDetail
    {
        public virtual string ToJson()
        {
            return LitJson.JsonMapper.ToJson(this);
        }
    }
}
