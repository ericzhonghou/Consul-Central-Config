// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace WordCount.Service
{
    using System;
    using System.Threading;
    using Microsoft.ServiceFabric.Services.Runtime;
    using System.Diagnostics;
    using Consul.Config;
    using Consul;/// <summary>
                 /// The service host is the executable that hosts the Service instances.
                 /// </summary>
                 ///


    public class Program
    {
        // Key associated with this specific application
        private static string key = "WordCount.Service";
        private static string address = "http://localhost:8500";

        public static void Main(string[] args)
        {
            // Create a Service Fabric Runtime and register the service type.
            try
            {
                // This is the name of the ServiceType that is registered with FabricRuntime. 
                // This name must match the name defined in the ServiceManifest. If you change
                // this name, please change the name of the ServiceType in the ServiceManifest.
                ServiceRuntime.RegisterServiceAsync(
                    "WordCountServiceType",
                    context =>
                        new WordCountService(context)).GetAwaiter().GetResult();

                StartConsul();
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e);
            }
        }
        private static void StartConsul()
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
                TimeSpan interval = new TimeSpan(0, 1, 0);
                Thread.Sleep(interval);

                // Constantly checks if the value has changed
                ConfigItem check = client.GetConfigItem(key);

                if (curr.Value != check.Value)
                {
                    // Resync two values
                    curr.Value = check.Value;
                    Debug.WriteLine("!@#$!@#$@#!$" + curr.Value);
                    
                }

            }
        }
    }
}