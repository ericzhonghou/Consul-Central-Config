using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsulTest
{
    class Class1
    {

       public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException();
            }

            string key = args[0];

            ConsulManager client = new ConsulManager("http://localhost:8500");
            ConfigItem c = client.GetConfigItem(key);
        }
    }
}
