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
                    name: "default",
                    pattern: "{controller=Home}/Shop/{action=Index}");


                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Public/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "X{controller=Home}/{action=Index}/{id?}");


            });

            app.Run();
        }
    }
}
