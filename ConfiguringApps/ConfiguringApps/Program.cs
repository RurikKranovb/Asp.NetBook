using System.Reflection;
using ConfiguringApps.Infrastructure;

namespace ConfiguringApps
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            builder.Services.AddSingleton<UpTimeService>();

            builder.WebHost.ConfigureAppConfiguration(p =>
            {
                new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration();

                #region MyRegion

                //        .ConfigureAppConfiguration((hostingContext, config) =>
                //        {
                //            var env = hostingContext.HostingEnvironment;
                //            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true,
                //                    reloadOnChange: true);

                //            if (env.IsDevelopment())
                //            {
                //                var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                //                if (appAssembly != null)
                //                {
                //                    config.AddUserSecrets(appAssembly, optional: true);
                //                }
                //            }

                //            config.AddEnvironmentVariables();
                //            if (args != null)
                //            {
                //                config.AddCommandLine(args);
                //            }
                //        })
                //        .ConfigureLogging((hostingContext, logging) =>
                //        {
                //            logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                //            logging.AddConsole();
                //            logging.AddDebug();
                //        })
                //        .UseIISIntegration()
                //        .UseDefaultServiceProvider((context, options) =>
                //        {
                //            options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                //        });

                //});

                #endregion



                var app = builder.Build();

                app.MapRazorPages()
                    .WithStaticAssets();

                app.UseMiddleware<BrowserTypeMiddleware>();
                app.UseMiddleware<ShortCircuitMiddleware>();
                app.UseMiddleware<ContentMiddleware>();


                app.MapControllerRoute(

                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            });
        }
    }
}
