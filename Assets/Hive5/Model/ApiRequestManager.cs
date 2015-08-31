using Hive5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Model
{
    public class ApiRequestManager
    {
        /// <summary>
        /// 유효기간 (단위 MilliSeconds)
        /// </summary>
        public int Ttl = 2500;
        private Dictionary<string, Rid> _dataKeyToRid = new Dictionary<string, Rid>();
        private Dictionary<string, Rid> _requestIdToRid = new Dictionary<string, Rid>();
        public static ApiRequestManager Instance { get; private set; }

        static ApiRequestManager()
        {
            Instance = new ApiRequestManager();
        }

        private ApiRequestManager() { }
        public bool CheckRequestAllowed(Rid rid)
        {
            // Cleanup
            var olds = _dataKeyToRid.Where(c=>c.Value.TimeStamp < DateTime.Now.AddMilliseconds(this.Ttl)).Select(c=>c.Value).ToList();
#if UNITY_EDITOR
            if (olds.Count > 0) { 
                Logger.Log("RequestManager request count is cleaned up to " + (_requestIdToRid.Count - olds.Count));
            }
#endif
            
            foreach (var item in olds)
	        {
                _dataKeyToRid.Remove(item.ToDataKey());
                _requestIdToRid.Remove(item.RequestId);
	        }

            var dataKey = rid.ToDataKey();

            if (_dataKeyToRid.ContainsKey(dataKey) == true)
                return false;

            return true;
        }

        public void Add(Rid rid)
        {
            if (rid == null)
                return;

            _requestIdToRid.Add(rid.RequestId, rid);
            _dataKeyToRid.Add(rid.ToDataKey(), rid);

#if UNITY_EDITOR
             Logger.Log("RequestManager request count is increased to " + _requestIdToRid.Count);
#endif
        }

        public void RemoveByRequestId(string requestId)
        {
            Rid rid = null;
            if (_requestIdToRid.TryGetValue(requestId, out rid) == false)
                return;

            _requestIdToRid.Remove(requestId);
            _dataKeyToRid.Remove(rid.ToDataKey());

#if UNITY_EDITOR
            Logger.Log("RequestManager request count is decreased to " + _requestIdToRid.Count);
#endif
        }
    }
}
