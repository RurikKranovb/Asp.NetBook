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


            app.MapControllerRoute(

                name: "default",
                pattern: "{controller}/{action=Index}");

            app.Run();
        }
    }
}
