using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class MockMonoSingleton<T> where T : class, new()
    {
        public static T Instance { get; private set; }

        static MockMonoSingleton()
        {
            Instance = new T();
        }

        protected MockMonoSingleton()
        {

        }
    }
}
