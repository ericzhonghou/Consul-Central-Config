using System; 
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ConsulTest
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        /// 

        // Key associated with this specific application
        private static string key = "key1";
        private static string address = "http://localhost:8500";

        private static void Main()
        {
            try
            {
                // The ServiceManifest.XML file defines one or more service type names.
                // Registering a service maps a service type name to a .NET type.
                // When Service Fabric creates an instance of this service type,
                // an instance of the class is created in this host process.

                ServiceRuntime.RegisterServiceAsync("ConsulTestType",
                    context => new ConsulTest(context)).GetAwaiter().GetResult();

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(ConsulTest).Name);

                Startup();
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
            }
        }

        private static void Startup()
        {
            // Creates a client connected to Consul Server at address
 
            ConsulManager client = new ConsulManager(address);

            client.SetConfigItem(key, "this is a value");
            ConfigItem configItem = client.GetConfigItem(key);
            Debug.WriteLine("***" + configItem.Value);

            ConfigItem curr = configItem;

            // Prevents this host process from terminating so services keep running.

            while (true)
            {
                TimeSpan interval = new TimeSpan(0, 5, 0);
                Thread.Sleep(interval);

                // Constantly checks if the value has changed
                ConfigItem check = client.GetConfigItem(key);

                if (curr.Value != check.Value)
                {
                    // Resync two values
                    curr.Value = check.Value;
                    Debug.WriteLine(curr.Value);

                    // TODO Put new config in database.
                }

            }
        }
    }
}
