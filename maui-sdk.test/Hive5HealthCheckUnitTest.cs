using Assets.Hive5;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maui_sdk.test
{
    [TestClass]
    public class Hive5HealthCheckUnitTest
    {
        public static string ProductionHealthUrl = "http://health.hive5.io/public-test/status-normal.json";
        public static string BetaHealthUrl = "http://health.hive5.io/public-test/status-warning.json";
        public static string AlphaHealthUrl = "http://health.hive5.io/public-test/status-shutdown.json";


        [TestMethod, TestCategory("HealthCheck")]
        public void TestHealthCheckNormal()
        {
            //{
            //    "maintenance" : {
            //        "execution" : null,
            //        "plans":[ ]
            //    }
            //}
            string filename = "fixtures/status/normal.json";

            string json = File.ReadAllText(filename);
            var maintenance = Maintenance.Parse(json);
            Assert.IsNotNull(maintenance);
            Assert.IsNull(maintenance.ExecutingPlan);
            Assert.IsTrue(maintenance.PendingPlans == null || maintenance.PendingPlans.Count == 0);
        }

        [TestMethod, TestCategory("HealthCheck")]
        public void TestHealthCheckShutdown()
        {
            //{
            //    "maintenance" : {
            //        "execution" : {
            //            "start":"2015-02-04T14:00:00.000Z",
            //            "end":"2015-02-04T17:00:00.000Z",
            //            "message":{
            //                "default":"system ...",
            //                "en":"system ...",
            //                "kr":"2~3am 까지 시스템 점검입니다"
            //            }
            //        },
            //        "plans":[ ]
            //    }
            //}
            string filename = "fixtures/status/shutdown.json";

            HealthChecker.Instance.CountryCode = "kr";

            string json = File.ReadAllText(filename);
            var maintenance = Maintenance.Parse(json);
            Assert.IsNotNull(maintenance);
            Assert.IsNotNull(maintenance.ExecutingPlan);

            var plan = maintenance.ExecutingPlan;

            Assert.IsNotNull(plan.StartAt);
            Assert.IsTrue(plan.StartAt.Value.Hour == 23);
            Assert.IsNotNull(plan.EndAt);
            Assert.IsTrue(plan.EndAt.Value.Hour == 2);
            Assert.IsTrue(plan.Message == "2~3am 까지 시스템 점검입니다");

            Assert.IsTrue(maintenance.PendingPlans == null || maintenance.PendingPlans.Count == 0);
        }

        [TestMethod, TestCategory("HealthCheck")]
        public void TestHealthCheckWarning()
        {
            //{
            //    "maintenance" : {
            //        "execution" : null,
            //        "plans":[
            //            {
            //                "start":"2015-02-05T14:00:00.000Z",
            //                "end":"2015-02-05T17:00:00.000Z",
            //                "message":{
            //                    "default":"system ...",
            //                    "en":"system ...",
            //                    "kr":"1~4am 까지 시스템 점검입니다"
            //                }
            //            }             
            //        ]
            //    }
            //}
            string filename = "fixtures/status/warning.json";

            HealthChecker.Instance.CountryCode = "kr";

            string json = File.ReadAllText(filename);
            var maintenance = Maintenance.Parse(json);
            Assert.IsNotNull(maintenance);
            Assert.IsNull(maintenance.ExecutingPlan);

            Assert.IsTrue(maintenance.PendingPlans != null && maintenance.PendingPlans.Count == 1);

            var plan = maintenance.PendingPlans[0];

            Assert.IsNotNull(plan.StartAt);
            Assert.IsTrue(plan.StartAt.Value.Hour == 23);
            Assert.IsNotNull(plan.EndAt);
            Assert.IsTrue(plan.EndAt.Value.Hour == 2);
            Assert.IsTrue(plan.Message == "1~4am 까지 시스템 점검입니다");
        }

        [TestMethod, TestCategory("HealthCheck")]
        public void TestGetMaintenance()
        {
            var maintenance = HealthChecker.Instance.GetMaintenance(Hive5HealthCheckUnitTest.ProductionHealthUrl);
            Assert.IsNotNull(maintenance);
            Assert.IsNull(maintenance.ExecutingPlan);
            Assert.IsTrue(maintenance.PendingPlans == null || maintenance.PendingPlans.Count == 0);

            // warning
            var warningMaintenance = HealthChecker.Instance.GetMaintenance(Hive5HealthCheckUnitTest.BetaHealthUrl);
            Assert.IsNotNull(warningMaintenance);
            Assert.IsNull(warningMaintenance.ExecutingPlan);

            Assert.IsTrue(warningMaintenance.PendingPlans != null && warningMaintenance.PendingPlans.Count == 1);

            var plan = warningMaintenance.PendingPlans[0];

            Assert.IsNotNull(plan.StartAt);
            Assert.IsTrue(plan.StartAt.Value.Hour == 23);
            Assert.IsNotNull(plan.EndAt);
            Assert.IsTrue(plan.EndAt.Value.Hour == 2);
            Assert.IsTrue(plan.Message == "system ...");

            // shutdown
            var shutdownMaintenance = HealthChecker.Instance.GetMaintenance(Hive5HealthCheckUnitTest.AlphaHealthUrl);
            Assert.IsNotNull(shutdownMaintenance);
            Assert.IsNotNull(shutdownMaintenance.ExecutingPlan);

            var plan2 = shutdownMaintenance.ExecutingPlan;

            Assert.IsNotNull(plan2.StartAt);
            Assert.IsTrue(plan2.StartAt.Value.Hour == 23);
            Assert.IsNotNull(plan2.EndAt);
            Assert.IsTrue(plan2.EndAt.Value.Hour == 2);
            Assert.IsTrue(plan2.Message == "system ...");

            Assert.IsTrue(shutdownMaintenance.PendingPlans == null || shutdownMaintenance.PendingPlans.Count == 0);
        }
    }
}
