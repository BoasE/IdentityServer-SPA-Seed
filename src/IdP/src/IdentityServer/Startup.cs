using System;
using System.Collections.Generic;
using System.Security.Claims;
using Identity.Shared;
using IdentityModel;
using IdentityServer.MongoDb;
using IdentityServer.MongoDb.dto;
using IdentityServer.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickstartIdentityServer.Pipelines;

namespace QuickstartIdentityServer
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }


        public Startup(IHostingEnvironment env)
        {
            var environmentVar = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLowerInvariant() ??
                                 env.EnvironmentName?.ToLowerInvariant();

            Console.WriteLine($"Environment {environmentVar}");
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentVar}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var database = services.AddMongoDatabase(Configuration);
            services.AddDataProtection()
                .SetApplicationName("identityserver")
                .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"/var/dpkeys/"));
            var mongoUserStore = new MongoUserStore(database);
            var hasher = new PasswordHasher<MongoExternalUser>();
//            var mongoExternalUser = mongoUserStore.AutoProvisionUser("IdSrv", "alex", new List<Claim>()
//            {
//                new Claim(JwtClaimTypes.Name,"alex"),
//                new Claim(JwtClaimTypes.Email, "alexander.zeitler@pdmlab.com")
//            }).Result;
//            var hash = hasher.HashPassword(mongoExternalUser as MongoExternalUser, "test");
//            var updateResult = mongoUserStore.SetPasswordHashForUser(mongoExternalUser, hash).Result;
            services.AddSingleton<MongoUserStore>(mongoUserStore);
            services.AddRepositories(database);
            services.AddMvc();
            
            

            services.AddIdentityServer()
                .AddSigningCredential(CertificateStore.LoadFromConfiguration(Configuration))
                .AddMongoRepository()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddIdentityApiResources()
                .AddPersistedGrants()
                .AddInMemoryClients(Config.GetClients(Configuration))
                .AddProfileService<ProfileService>();

            services
                .AddAuthentication()
                .AddExternalAuth(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseIdenityCors();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsForwardingOptions();
            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}