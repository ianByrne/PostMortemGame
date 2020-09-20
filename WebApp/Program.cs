using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace IanByrne.ResearchProject.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SetEbConfig();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        /// <summary>
        /// AWS Elastic Beanstalk does not yet support Environment.GetEnvironmentVariable() for .NET Core
        /// This workaround/hack is from https://stackoverflow.com/a/47648283/1907173
        /// </summary>
        private static void SetEbConfig()
        {
            IConfigurationBuilder tempConfigBuilder = new ConfigurationBuilder();

            tempConfigBuilder.AddJsonFile(
                @"C:\Program Files\Amazon\ElasticBeanstalk\config\containerconfiguration",
                optional: true,
                reloadOnChange: true
            );

            IConfigurationRoot configuration = tempConfigBuilder.Build();

            Dictionary<string, string> ebEnv =
                configuration.GetSection("iis:env")
                    .GetChildren()
                    .Select(pair => pair.Value.Split(new[] { '=' }, 2))
                    .ToDictionary(keypair => keypair[0], keypair => keypair[1]);

            foreach (KeyValuePair<string, string> keyVal in ebEnv)
            {
                Environment.SetEnvironmentVariable(keyVal.Key, keyVal.Value);
            }
        }
    }
}
