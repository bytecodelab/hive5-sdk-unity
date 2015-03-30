using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Hive5.Util
{
    public enum ServerStatus
    {
        Indeterminated = 0,
        Good = 1,
        Plan = 2, // 점검예정
        Shutdown = 3
    }

    public interface IHealthChecker
    {
        ServerStatus Status { get; }

        event EventHandler StatusChanged;
    }
}
