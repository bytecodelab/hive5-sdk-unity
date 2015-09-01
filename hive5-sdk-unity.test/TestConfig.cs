using Hive5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hive5_sdk_unity.test
{
    public class TestConfig
    {
        public static TestConfig Default { get; set; }
        public static TestConfig HealthCheck { get; set; }

        public string AppKey { get; set; }

        public string Uuid { get; set; }

        public User TestUser { get; set; }

        public string GoogleSdkVersion { get; set; }

        public string LeaderBoardKey { get; set; }

        public List<string> ObjectClasses { get; set; }

        public string XPlatformKey { get; set; }

        public string CustomAccountPlatformName { get; set; }

        public string Host { get; set; }

        public string KiterHost { get; set; }

        public string HealthCheckUrl { get; set; }

        public User Friend { get; set;  }

        static TestConfig()
        {
            Default = new TestConfig()
            {
                Host = "https://alpha.hornet.hive5.io",
                AppKey = "d8444735-15e3-4198-9179-102ba68776fc",
                Uuid = "46018",
                TestUser = new User() {
                    id = "8",
                    platform = "anonymous",
                },
                GoogleSdkVersion = "3",
                LeaderBoardKey = "leader1",
                ObjectClasses = new List<string> { "" }, 
                XPlatformKey = "4b9ea368-2809-4e57-91a1-d9ce7ac39534",
                CustomAccountPlatformName = "test",
                HealthCheckUrl = "http://health.hive5.io/public-test/status-normal.json",
                Friend = new User() { 
                    id = "13",
                    platform = "anonymous",
                },
                KiterHost = "http://alpha.rtapi.hive5.io/",
            };

            HealthCheck = new TestConfig()
            {
                Host = "https://alpha.hornet.hive5.io",
                AppKey = "4f81f92c-7cbf-47c5-a2a4-fdec156076a8",
                Uuid = "46018",
                TestUser = new User() {
                    id = "8",
                    platform="anonymous",
                },
                GoogleSdkVersion = "3",
                LeaderBoardKey = "leader1",
                ObjectClasses = new List<string> { "" },
                XPlatformKey = "4b9ea368-2809-4e57-91a1-d9ce7ac39534",
                CustomAccountPlatformName = "test",
                HealthCheckUrl = "http://health.hive5.io/public-test/status-shutdown.json",
                Friend = new User() { 
                    id = "13",
                    platform = "anonymous",
                },
            };
        }
    }
}
