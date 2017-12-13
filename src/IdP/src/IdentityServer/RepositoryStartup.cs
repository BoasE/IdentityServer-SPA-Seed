using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using QuickstartIdentityServer.Pipelines;

namespace QuickstartIdentityServer
{

        public static class RepositoryStartup
        {
            public static IMongoDatabase AddMongoDatabase(this IServiceCollection services, IConfigurationRoot configuration)
            {
                string constring = configuration["DataBase:ConnectionString"];
                Console.WriteLine($"mongodb on {constring}");
                var client = new MongoClient(constring);
                IMongoDatabase database = client.GetDatabase(configuration["DataBase:Name"]);
                services.AddSingleton(database);

                return database;
            }

            public static void AddRepositories(this IServiceCollection services, IMongoDatabase database)
            {
                services.AddSingleton(database);
                services.AddSingleton<IProfileService, ProfileService>();
                services.AddSingleton<IResourceStore, CustomInMemoryResourceStore>();
            }
        }
    
}
