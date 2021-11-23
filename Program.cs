using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HelloMVCWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseWebRoot("MyNewFolderName");  // Use a different Folder for  Static  files 

                    webBuilder.UseStartup<Startup>();
                });
                //.ConfigureLogging(logBuilder =>  https://www.tutorialsteacher.com/core/aspnet-core-logging
                //{
                //    logBuilder.ClearProviders(); // removes all providers from LoggerFactory
                //    logBuilder.AddConsole(); // Add Console  provider
                //    logBuilder.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider
                //});
    }
}
