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
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddJsonFile("appsettings.json",
                            optional: true, reloadOnChange: true);
                        config.AddEnvironmentVariables();
                        if (args != null)
                        {
                            config.AddCommandLine(args);
                        }
                    })
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

                if (builder.Environment.IsDevelopment())
                {

                    app.UseDeveloperExceptionPage();
                    app.UseStatusCodePages();
                    if (builder.Configuration.GetSection("ShortCircuitMiddleware")
                        .GetValue<bool>("EnableBrowserShortCircuit"))
                    {
                        app.UseMiddleware<BrowserTypeMiddleware>();// ПО для редактирования запросов
                        app.UseMiddleware<ShortCircuitMiddleware>();// промежуточное ПО 
                    }


                    //app.UseMiddleware<ErrorMiddleware>(); // ПО для редактирования ответов
                
                    //app.UseMiddleware<ContentMiddleware>();// промежуточное ПО для генерации содержимого
                }
                else
                {
                    app.UseExceptionHandler("Home/Error");
                }

                app.UseStaticFiles();


                app.MapControllerRoute(

                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            });
        }
    }
}
