using System;
using System.Collections.Generic;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Startup
{
    public static class CorsStartup
    {
        
        private static ISet<string> corsOrigins;

        public const string CorsPolicyName = "CorsPolicy";

        public static bool IsAllowedCorsOrigin(string source)
        {
            return !corsOrigins.IsNullOrEmpty() && corsOrigins.Contains(source);
        }

    public static string[] GetCorsOrigins(this IConfiguration config)
        {
            var items = config.GetSection("Cors").GetStringArray("Hosts");
            return items;
        }

        public static void UseIdenityCors(this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicyName);
        }
    }
}