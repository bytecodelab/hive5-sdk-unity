using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public enum CallResultKind
    {
        Unknown,
        GetChannelsResult,
        GetPlayersResult,
    }

    public abstract class CallResult
    {
        protected virtual void Load(ResultMessage message)
        {

        }
    }
}
