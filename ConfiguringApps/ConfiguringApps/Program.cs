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

                        var env = hostingContext.HostingEnvironment;

                        config.AddJsonFile("appsettings.json",
                                optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                                optional: true, reloadOnChange: true);
                        config.AddEnvironmentVariables();
                        if (args != null)
                        {
                            config.AddCommandLine(args);
                        }
                    })
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(
                            hostingContext.Configuration.GetSection("Logging"));
                        logging.AddConsole();
                        logging.AddDebug();
                    })
                    .UseDefaultServiceProvider((context, options) =>
                    {
                        options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
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
                        app.UseMiddleware<BrowserTypeMiddleware>(); // �� ��� �������������� ��������
                        app.UseMiddleware<ShortCircuitMiddleware>(); // ������������� �� 
                    }


                    //app.UseMiddleware<ErrorMiddleware>(); // �� ��� �������������� �������

                    //app.UseMiddleware<ContentMiddleware>();// ������������� �� ��� ��������� �����������
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

        //public void ConfigureDevelopmentServices(IServiceCollection services)
        //{
        //    services.AddSingleton<UpTimeService>();
        //    services.AddRazorPages();
        //}

        //public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        //{
        //    app.UseExceptionHandler("Home/Error");
        //    app.UseStaticFiles();
        //    app.UseRouting();
        //}

        //public void ConfigureDevelopment(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        //{

        //    app.UseDeveloperExceptionPage();
        //    app.UseStatusCodePages();
        //    app.UseStaticFiles();
        //    app.UseRouting();
        //}
    }
}
