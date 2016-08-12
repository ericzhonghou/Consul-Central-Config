using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UpdateConsul
{
    class Program
    {
        private static string server = string.Empty;
        private static string key = string.Empty;
        static void Main(string[] args)
        {
            try
            {
                server = args[0];
                key = args[1];

                ConsulManager client = new ConsulManager(server);
                ConfigItem c = client.GetConfigItem(key);

                Console.WriteLine(c.Value);


                TimeSpan interval = new TimeSpan(0, 0, 10);
                Thread.Sleep(interval);

            }
            catch (IndexOutOfRangeException e)
            {
             
            }

        }
    }
}
