using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maui_sdk.test
{
    public class TestValueSet
    {
        public static TestValueSet AinaRod { get; set; }

        public static TestValueSet Default { get; set; }

        public string AppKey { get; set; }

        public string Uuid { get; set; }

        public string ValidPlatformUserId { get; set; }

        public string GoogleSdkVersion { get; set; }

        public string LeaderBoardKey { get; set; }

        public List<string> ObjectClasses { get; set; }

        static TestValueSet()
        {
            AinaRod = new TestValueSet()
            {
                AppKey = "c355a728-2bbe-4093-ac3e-2653eb0bcb8b",
                Uuid = "47425",
                ValidPlatformUserId = "41024",
                GoogleSdkVersion = "3",
                LeaderBoardKey = "24",
                ObjectClasses = new List<string> { "" },
            };

            Default = new TestValueSet()
            {
                AppKey = "a40e4122-99d9-44a6-b916-68ed756f79d6",
                Uuid = "46018",
                ValidPlatformUserId = "88197948207226176",
                GoogleSdkVersion = "3",
                LeaderBoardKey = "3",
                ObjectClasses = new List<string> { "" }, 
            };
        }
    }
}
