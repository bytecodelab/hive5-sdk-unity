using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Hive5.Model
{
    public class Rid
    {
        public string RequestId { get; private set; }
        private static long _uid_generate = 0;

        public string QueryString { get; set;  }
        public string PostBody { get; set; }
        public string Url { get; set; }

        public DateTime TimeStamp { get; set; }

        public Rid()
        {
            _uid_generate++;
            this.RequestId = _uid_generate.ToString();
            this.TimeStamp = DateTime.Now;
        }

        public Rid(string url, string queryString, string postBody)
            : this()
        {
            this.Url = url;
            this.QueryString = queryString;
            this.PostBody = postBody;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || 
                obj is Rid == false)
                return false;

            return this.ToDataKey() == (obj as Rid).ToDataKey();
        }

        public string ToDataKey()     
        {
            return string.Format("{0};{1};{2}", this.Url, this.QueryString, this.PostBody);
        }
    }
}
