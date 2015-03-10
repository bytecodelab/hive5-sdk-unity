using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maui_sdk.test
{
    public class TestValueSet
    {
        public static TestValueSet Default { get; set; }

        public string AppKey { get; set; }

        public string Uuid { get; set; }

        public string ValidPlatformUserId { get; set; }

        public string GoogleSdkVersion { get; set; }

        public string LeaderBoardKey { get; set; }

        public List<string> ObjectClasses { get; set; }

        static TestValueSet()
        {
            Default = new TestValueSet()
            {
                AppKey = "4f81f92c-7cbf-47c5-a2a4-fdec156076a8",
                Uuid = "46018",
                ValidPlatformUserId = "88197948207226176",
                GoogleSdkVersion = "3",
                LeaderBoardKey = "3",
                ObjectClasses = new List<string> { "" }, 
            };
        }
    }
}
