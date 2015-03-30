using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Hive5.Util
{
    public class HealthChecker : IHealthChecker
    {
        private ServerStatus _Status = ServerStatus.Indeterminated;
        public ServerStatus Status 
        {
            get;
            private set 
            {
                bool changed = _Status != value;
                _Status = value;

                OnStatusChanged();
            }
        }

        public event EventHandler StatusChanged;
        private void OnStatusChanged()
        {
            if (StatusChanged == null)
                return;

            StatusChanged(this, new EventArgs());
        }

    }
}
