using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace FrontendSpa.Bootstrap
{
    public static class LoggerStartup
    {
        public static void UseMyLogger(this IApplicationBuilder builder, ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    .MinimumLevel.Debug()
            //    .WriteTo.LiterateConsole()
            //    .WriteTo.RollingFile("output-{Date}.log")
            //    .CreateLogger();

            //loggerFactory.AddSerilog();
            //lifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }
    }
}
