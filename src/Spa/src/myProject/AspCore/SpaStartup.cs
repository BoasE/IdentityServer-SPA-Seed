using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace FrontendSpa.Bootstrap
{
    public static class SpaStartup
    {
        public static void UseDefaultSpaDocument(this IApplicationBuilder app)
        {
            app.Use((context, next) =>
            {
                if (!Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = new PathString("/index.html");
                }
                return next();
            });
        }
    }
}