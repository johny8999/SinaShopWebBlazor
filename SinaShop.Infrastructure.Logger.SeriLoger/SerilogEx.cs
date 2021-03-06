using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.Logger.SeriLoger
{
    public static class SerilogEx
    {
        public static void UseSerilog_SqlServer(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseSerilog((builder, Logger) =>
            {
                Logger = new SeriLogConfig().ConfigSqlServer(LogEventLevel.Error);
                Logger.CreateLogger();
            });
        }

        public static void UseSerilog_Console(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseSerilog((builder, Logger) =>
            {
                Logger.WriteTo.Console().MinimumLevel.Is(LogEventLevel.Error);
            });
        }

        public static void UseSerilog_File(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseSerilog((builder, Logger) =>
            {
                Logger.WriteTo.File(Directory.GetCurrentDirectory() + "/Loggers/logs.txt"
                    /*, rollingInterval: RollingInterval.Month*/).MinimumLevel.Is(LogEventLevel.Error);
            });
        }
    }
}
