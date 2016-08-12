using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsulTest
{
    class Class1
    {

        public static void Update(string key)
        {
            ConsulManager client = new ConsulManager("http://localhost:8500");
            ConfigItem c = client.GetConfigItem(key);
        }
    }
}
