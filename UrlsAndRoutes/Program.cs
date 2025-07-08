using Microsoft.AspNetCore.Routing.Constraints;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            var app = builder.Build();
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.MapRazorPages();

            app.UseRouting();



            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "MyRoute",
                    pattern: "{controller=Home}/{action=Index}/{id?}",
                    constraints: new { id = new WeekDayConstraint() });

                //endpoints.MapControllerRoute(
                //    name: "MyRoute",
                //    pattern: "{controller:regex(^H.*)=Home}/{action:regex(^Index$|^About$)=Index}/{id:int?}");

                //endpoints.MapControllerRoute(
                //    name: "MyRoute",
                //    pattern: "{controller=Home}/{action=Index}",
                //    constraints: new {id = new IntRouteConstraint()});

                //endpoints.MapControllerRoute(
                //    name: "MyRoute",
                //    pattern: "{controller=Home}/{action=Index}/{id:int?}");

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "Shop/OldAction",
                //    defaults: new { controller = "Home", action = "Index" });


                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "Shop/{action=Index}",
                //    defaults: new {controller = "Home"});


                //endpoints.MapControllerRoute(
                //    name: "",
                //    pattern: "Public/{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute(
                //    name: "",
                //    pattern: "X{controller=Home}/{action=Index}/{id?}");


            });

            app.Run();
        }
    }
}
