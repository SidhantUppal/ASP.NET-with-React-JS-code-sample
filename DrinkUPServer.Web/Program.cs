using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DrinkUPServer.Web
{
    public class Program
    {
        public static void Main ( string[] args )
        {
            MachineServer.Program.Run();

            IHostBuilder hostBuilder = Host.CreateDefaultBuilder( args );
            hostBuilder.ConfigureWebHostDefaults( RunWebHostDefaults );

            IHost host = hostBuilder.Build();

            host.Run();
        }

        private static void RunWebHostDefaults ( IWebHostBuilder webHostBuilder )
        {
            webHostBuilder.UseStartup<Startup>();
        }
    }
}
