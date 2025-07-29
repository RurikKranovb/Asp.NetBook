using Filters.Infrastructure;

namespace Filters
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //builder.Services.AddScoped<IFilterDiagnostics, DefaultFilterDiagnostics>();

            builder.Services.AddSingleton<IFilterDiagnostics, DefaultFilterDiagnostics>();
            builder.Services.AddSingleton<TimerFilter>();

            // Add services to the container.
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddRazorPages();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"
                );
            });

            app.Run();
        }
    }
}
