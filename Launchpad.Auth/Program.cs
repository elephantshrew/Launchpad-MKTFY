using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Launchpad.Auth
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
                    webBuilder.UseStartup<Startup>();
                })
             .ConfigureAppConfiguration((builder) =>
            {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development")
                {
                    builder.AddSystemsManager(String.Format("/Launchpad/{0}/", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")), new AWSOptions
                    {
                        Region = RegionEndpoint.CACentral1,
                        Profile = "default"
                    });
                }
                if (env != "Development")
                {
                    builder.AddSystemsManager(String.Format("/Launchpad/{0}/", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")), new AWSOptions
                    {
                        Region = RegionEndpoint.CACentral1
                    }); }
            });
    }
}
