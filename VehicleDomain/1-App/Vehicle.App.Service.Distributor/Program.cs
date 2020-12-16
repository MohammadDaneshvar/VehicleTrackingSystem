using System;
using System.IO;
using System.Threading.Tasks;
using Framework.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Vehicle.Infra.Persistance.EF;

namespace App.Service.AspDotNetDistributor
{
    public class Program
    {
        private static AppSettings _appSettings;
        public static async Task Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddCommandLine(args)
            .Build();

            _appSettings = config.Get<AppSettings>();
            GlobalDiagnosticsContext.Set("connectionString", _appSettings.ConnectionStrings.ConnectionString);
            Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                var host = CreateWebHostBuilder(args).Build();
                await InitializeDatabaseAsync(host);
                CreateConfigLog(host);
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex,  "ApplicationStopped");
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseKestrel()
                    .UseIIS()
                    .UseUrls(_appSettings.ApplicationLaunchUrl)
                    .UseStartup<Startup>();
                    
                }).ConfigureLogging(logging =>
                {
                    // logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                }).UseNLog();

        private static void CreateConfigLog(IHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                IWebHostEnvironment hostingEnvironment = services.GetRequiredService<IWebHostEnvironment>();
                string webRoot = hostingEnvironment.WebRootPath;
                string path = "/ConfigLog.json";

                if (!File.Exists(webRoot + path))
                {
                    //GetLog logScanner = services.GetRequiredService<GetLog>();
                    //string json = logs.ToJson();
                    //File.WriteAllText(webRoot + path, json);
                }
            }
        }

        public async static Task InitializeDatabaseAsync(IHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                //ICurrentUserService user = services.GetRequiredService<ICurrentUserService>();
                var dbContext = services.GetService<IDbContext>();
             
                ((VehicleDbContext)dbContext).Database.Migrate();
                
                
             //   await seeder.SeedAsync();
                //GlobalDiagnosticsContext.Set("organizationId", user.OrganizationId);
                //GlobalDiagnosticsContext.Set("userId", user.OrganizationId);
                //GlobalDiagnosticsContext.Set("userName", user.UserName);
            }
        }
    }
}
