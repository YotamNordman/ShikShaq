using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShikShaq.Data;
using Microsoft.Extensions.DependencyInjection;
using ShikShaq.Migrations;
using ShikShaq.Logic;

namespace ShikShaq
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            
            var context = services.GetRequiredService<ShikShaqContext>();
            ShikShaqContextInitializer initializer = new ShikShaqContextInitializer();
            initializer.Initialize(context);

            host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
