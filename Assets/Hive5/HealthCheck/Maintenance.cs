using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using System.Collections;

namespace Hive5.HealthChecker
{
    /// <summary>
    /// 서버 유지보수 활동 모델 클래스 
    /// </summary>
    public class Maintenance
    {
        /// <summary>
        /// 실행중인 계획
        /// </summary>
        public Plan ExecutingPlan { get; set; }
        /// <summary>
        /// 대기중인 계획
        /// </summary>
        public List<Plan> PendingPlans { get; set; }

        /// <summary>
        /// 기본생성자
        /// </summary>
        public Maintenance()
        {
        }

        /// <summary>
        /// json문자열을 파싱하여 Maintenance 인스턴스를 생성한다.
        /// </summary>
        /// <param name="json">Json 데이터</param>
        /// <returns>Maintenance 인스턴스</returns>
        public static Maintenance Parse(string json)
        {
            if (string.IsNullOrEmpty(json) == true)
                return null;

            Maintenance maintenance = new Maintenance();
            
            var jsonData = JsonMapper.ToObject<LitJson.JsonData>(json);

            if (jsonData.ContainsKey("maintenance") == false)
                return null;

            var rootJson = jsonData["maintenance"];

            // execution
            if (rootJson.ContainsKey("execution") == true)
            {
                var executionJson = rootJson["execution"];
                maintenance.ExecutingPlan = ConvertToPlan(executionJson);
            }

            if (rootJson.ContainsKey("plans") == true)
            {
                var plansJson = rootJson["plans"];

                if (plansJson.IsArray == true &&
                    plansJson.Count > 0)
                {
                    var plans = new List<Plan>();
                    for (int i = 0; i < plansJson.Count; i++)
                    {
                        var plan = ConvertToPlan(plansJson[i]);
                        if (plan == null)
                            continue;

                        plans.Add(plan);
                    }

                    maintenance.PendingPlans = plans;
                }
            }


            return maintenance;
        }

        /// <summary>
        /// JsonData를 Plan으로 변환한다.
        /// </summary>
        /// <param name="jsonData">데이터 원본</param>
        /// <returns>변환된 Plan 인스턴스</returns>
        private static Plan ConvertToPlan(JsonData jsonData)
        {
            if (jsonData == null)
                return null;

            //"start":"2015-02-04T14:00:00.000Z",
            //"end":"2015-02-04T17:00:00.000Z",
            //"message":{
            //    "default":"system ...",
            //    "en":"system ...",
            //    "kr":"2~3am 까지 시스템 점검입니다"
            //}
            Plan plan = new Plan();

            if (jsonData.ContainsKey("start") == true) {
                plan.StartAt =  DateTime.Parse((string)jsonData["start"]);
            }

            if (jsonData.ContainsKey("end") == true) {
                plan.EndAt = DateTime.Parse((string)jsonData["end"]);
            }

            var messageJson = jsonData["message"];

            IDictionary dic = messageJson as IDictionary;
            if(dic != null)
            { 
                string countryCode = string.IsNullOrEmpty(HealthChecker.Instance.CountryCode) == false ? HealthChecker.Instance.CountryCode : HealthChecker.DefaultCountryCode;

                if(dic.Contains(countryCode) == true)
                {
                    plan.Message = (string)messageJson[countryCode];
                }
                else if (dic.Contains(HealthChecker.DefaultCountryCode) == true)
                {
                    plan.Message = (string)messageJson[HealthChecker.DefaultCountryCode];
                }
            }
              

            return plan;
        }
    }
}
