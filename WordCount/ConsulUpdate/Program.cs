using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Consul;


namespace ConsulUpdate 
{
    class Program
    {
        private static TimeSpan DisplayTime = new TimeSpan(0, 0, 10);
        public static void run(string address, string key)
        {
            ConsulManager cm = new ConsulManager(address);
            Consul.Config.ConfigItem ci = cm.GetConfigItem(key);

            Console.WriteLine(ci.Value);

            Thread.Sleep(DisplayTime);
        }

        public static void Main(string[] args)
        {
            try
            {
                string server = args[0];
                string key = args[1];

                run(server, key);

                TimeSpan interval = new TimeSpan(0, 0, 10);
                Thread.Sleep(interval);

            }
            catch (IndexOutOfRangeException e)
            {

            }
        }
    }

}
