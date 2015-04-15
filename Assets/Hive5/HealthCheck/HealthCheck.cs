using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Assets.Hive5
{
    public enum ServerStatus 
    { 
        Indeterminated = 0, 
        Good = 1, 
        Plan = 2, // 점검예정 
        Shutdown = 3 
    } 



    public class HealthChecker
    {
        public const string DefaultCountryCode = "default";
        public string CountryCode { get; set; }


        public static HealthChecker Instance { get; private set; }

        private ServerStatus _Status = ServerStatus.Indeterminated;
        public ServerStatus Status 
        {
            get { return _Status; }
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
        
        static HealthChecker()
        {
            HealthChecker.Instance = new HealthChecker();
        }

        private HealthChecker()
        {
            this.CountryCode = HealthChecker.DefaultCountryCode;
        }

        public Maintenance GetMaintenance(string url)
        {
            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };
            var json = wc.DownloadString(url);

            var maintenance = Maintenance.Parse(json);
            return maintenance;
        }
    }
}
