using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Startup
{
    public static class ExternalAuthStartup
    {
        public static void AddExternalAuth(this AuthenticationBuilder builder, IConfiguration config)
        {
            BindGoogle(builder, config);
            BindFacebook(builder, config);
        }

        private static void BindGoogle(AuthenticationBuilder builder, IConfiguration configuration)
        {
            if (configuration["Identity:External:Google:AppId"] != null)
            {
                builder.AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = configuration["Identity:External:Google:AppId"];
                    options.ClientSecret = configuration["Identity:External:Google:AppSecret"];
                });
            }
        }

        private static void BindFacebook(AuthenticationBuilder builder, IConfiguration configuration)
        {
            if (configuration["Identity:External:Facebook:AppId"] != null)
            {
                builder.AddFacebook("Facebook", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = configuration["Identity:External:Facebook:AppId"];
                    options.ClientSecret = configuration["Identity:External:Facebook:AppSecret"];
                });
            }
        }
    }
}