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

        public string LeaderBoardKey { get; set; }

        public List<string> ObjectClasses { get; set; }

        static TestValueSet()
        {
            Default = new TestValueSet()
            {
                AppKey = "d8444735-15e3-4198-9179-102ba68776fc",
                Uuid = "46018",
                ValidPlatformUserId = "88197948207226176",
                LeaderBoardKey = "3",
                ObjectClasses = new List<string> { "" }, 
            };
        }
    }
}
