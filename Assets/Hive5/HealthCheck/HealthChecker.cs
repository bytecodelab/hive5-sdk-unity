using Hive5;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Hive5.HealthChecker
{
    /// <summary>
    /// 서버 상태 열거형
    /// 아래 링크 내용을 참고하였음
    /// http://www.novell.com/documentation//edir88/edir88new/data/bqn2l9c.html#bqa5ntz
    /// </summary>
    public enum ServerStatus 
    { 
        Indeterminated = 0, 
        Normal = 1, 
        Warning = 2, // 점검예정 
        Critical = 3 
    } 


    /// <summary>
    /// 서버 건상 상태를 일시적/주기적으로 확인할 수 있는 클래스
    /// </summary>
#if DOTNET
    public class HealthChecker : MockMonoSingleton<Hive5Http> {
#else
	public class HealthChecker : MonoSingleton<Hive5Http> {
#endif

        /// <summary>
        /// 국가코드 기본값
        /// </summary>
        public const string DefaultCountryCode = "default";
        /// <summary>
        /// 국가코드
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// 싱글턴 패턴의 인스턴스 접근자
        /// </summary>
        public static HealthChecker Instance { get; private set; }

        private ServerStatus _Status = ServerStatus.Indeterminated;

        /// <summary>
        /// 서버 상태
        /// </summary>
        public ServerStatus Status 
        {
            get { return _Status; }
            private set 
            {
                bool changed = _Status != value;
                _Status = value;

                if (changed == true)
                {
                    OnStatusChanged();
                    if (_Listener != null)
                    {
                        _Listener.OnServerStatusChanged(value, this.CurrentMaintenance);
                    }
                }
            }
        }

        /// <summary>
        /// 현재 유지보수 내용
        /// </summary>
        public Maintenance CurrentMaintenance { get; set; }

        private Hive5HealthCheckerListener _Listener = new Hive5HealthCheckerListener();

        /// <summary>
        /// 상태 변경 이벤트
        /// </summary>
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

        /// <summary>
        /// 유지보수 내용 가져오기
        /// </summary>
        /// <param name="url">호출 주소</param>
        /// <returns>유지보수 내용</returns>
        public Maintenance GetMaintenance(string url)
        {
            string json = DownloadString(url);
            var maintenance = Maintenance.Parse(json);
            return maintenance;
        }

#if DOTNET
        /// <summary>
        /// Url로 웹 문자열 컨텐츠 내려받기
        /// </summary>
        /// <param name="url">호출 주소</param>
        /// <returns>문자열</returns>
        public string DownloadString(string url)
        {
            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };
            string content = wc.DownloadString(url);
            return content;
        }
#else
        /// <summary>
        /// Url로 웹 문자열 컨텐츠 내려받기
        /// </summary>
        /// <param name="url">호출 주소</param>
        /// <returns>문자열</returns>
        public string DownloadString(string url)
        {
            var www = GetLoadedWWW(url);
            string content = www.text;
            return content;
        }

        private WWW GetLoadedWWW(string url)
        {
            WWW www = new WWW (url);
            StartCoroutine (WaitForRequest (www));
            return www; 
        }

        private IEnumerator WaitForRequest(WWW www)
        {
            yield return www;

            // check for errors
            if (www.error == null)
            {
                Debug.Log("WWW Ok!: " + www.text);
            } else {
                Debug.Log("WWW Error: "+ www.error);
            }    
        }

#endif

        
        private bool _MonitorStarted = false;
        private BackgroundWorker _HealthMonitorWorker = null;
        private bool _ForceStopMonitor = false;

        /// <summary>
        /// 감시를 시작합니다.
        /// </summary>
        public void StartMonitor()
        {
            if (_MonitorStarted == true)
                throw new InvalidOperationException("Monitor is alread started");

            if (_HealthMonitorWorker == null)
            {
                _HealthMonitorWorker = new BackgroundWorker() {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                _HealthMonitorWorker.ProgressChanged += _HealthMonitorWorker_ProgressChanged;
                _HealthMonitorWorker.RunWorkerCompleted += _HealthMonitorWorker_RunWorkerCompleted;
                _HealthMonitorWorker.DoWork += _HealthMonitorWorker_DoWork;
            }

            _HealthMonitorWorker.RunWorkerAsync();
        }

        void _HealthMonitorWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _MonitorStarted = false;
        }

        /// <summary>
        /// 감시를 중단합니다.
        /// </summary>
        public void StopMonitor()
        {
            _ForceStopMonitor = true;
            if (_HealthMonitorWorker != null)
            {
                _HealthMonitorWorker.CancelAsync();
            }
        }


        void _HealthMonitorWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            while (_ForceStopMonitor == false)
            {
                var maintenance = GetMaintenance(Hive5Client.HealthCheckUrl);
                bw.ReportProgress(0, maintenance);
                Thread.Sleep(Hive5Config.AutoHealthCheckInterval);
            }

            _ForceStopMonitor = false;
        }

        void _HealthMonitorWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var maintenance = e.UserState as Maintenance;
            this.CurrentMaintenance = maintenance;
            this.Status = GetStatusBy(maintenance);
        }

        private ServerStatus GetStatusBy(Maintenance maintenance)
        {
            if (maintenance == null)
                return ServerStatus.Indeterminated;

            if (maintenance.ExecutingPlan != null)
            {
                return ServerStatus.Critical;
            }
            else
            {
                if (maintenance.PendingPlans == null || maintenance.PendingPlans.Count == 0)
                {
                    return ServerStatus.Normal;
                }
                else
                {
                    return ServerStatus.Warning;
                }
            }
        }
    }
}
