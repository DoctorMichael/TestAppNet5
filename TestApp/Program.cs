using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.BusinessLogic.Services.Implementation;
using TestApp.Domain.Models;
using TestApp.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using TestApp.DataAccess.InitializationDb;

namespace TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //new InitializationDb();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
