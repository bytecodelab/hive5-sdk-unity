using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class CallErrorResult : CallResult
    {
        public ErrorMessage Error { get; set; }
    }
}
