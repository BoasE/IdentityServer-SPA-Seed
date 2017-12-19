using System;
using IdentityServer.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickstartIdentityServer;

namespace IdentityServer.Administration
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            var HOST_IP = Environment.GetEnvironmentVariable("HOST_IP");
            
            var database = services.AddMongoDatabase(Configuration);
            services.AddDataProtection()
                .SetApplicationName("identityserver")
                .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"/var/dpkeys/"));
            var mongoUserStore = new MongoUserStore(database);
            services.AddSingleton<MongoUserStore>(mongoUserStore);
            services.AddRepositories(database);
            services.AddMvc();
            
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = $"http://{HOST_IP}";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "users";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
