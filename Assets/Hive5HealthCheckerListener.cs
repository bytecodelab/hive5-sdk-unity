using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public partial class Hive5HealthCheckerListener 
    {
        public void OnServerStatusChanged(ServerStatus status, Maintenance maintenance)
        {
            switch (status)
            {
                case ServerStatus.Indeterminated:
                    break;
                case ServerStatus.Normal:
                    break;
                case ServerStatus.Warning:
                    // use maintenance.PendingPlans
                    break;
                case ServerStatus.Critical:
                    // use maintenance.ExecutingPlan
                    break;
                default:
                    break;
            }
        }
    }
}
