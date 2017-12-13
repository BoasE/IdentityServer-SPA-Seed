using FrontendSpa.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FrontendSpa
{
    public sealed class Startup
    {
        private static IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddResponseCompression();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IApplicationLifetime lifetime)
        {
            app.UseMyLogger(loggerFactory, lifetime);
            app.UseResponseCompression();
            app.UseMvcWithDefaultRoute();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseDefaultSpaDocument();
        }
    }
}