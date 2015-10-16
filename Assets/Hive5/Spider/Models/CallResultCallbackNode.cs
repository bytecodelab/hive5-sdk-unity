using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class CallResultCallbackNode
    {
        public CallResultKind Kind { get; set; }
        public CallResultCallback Callback { get; set; }

        public CallResultCallbackNode(CallResultKind kind, CallResultCallback callback)
        {
            this.Kind = kind;
            this.Callback = callback;
        }
    }
}
