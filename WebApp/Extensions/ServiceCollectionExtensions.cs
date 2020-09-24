using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using IanByrne.ResearchProject.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IanByrne.ResearchProject.WebApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostMortemContext(this IServiceCollection services)
        {
            string connectionString = "";

            if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("AWS_REGION")))
            {
                using (var client = new AmazonSecretsManagerClient())
                {
                    var request = new GetSecretValueRequest()
                    {
                        SecretId = "PostMortem/Db"
                    };

                    var response = client.GetSecretValueAsync(request).Result;
                    var responseDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.SecretString);
                    connectionString = responseDict["connectionstring"];
                }
            }
            else
            {
                connectionString = "server=localhost;database=postmortem;user=root;password=mysql;SslMode=None";
            }

            services.AddDbContext<PostMortemContext>(o => o.UseMySql(connectionString));

            return services;
        }
    }
}
