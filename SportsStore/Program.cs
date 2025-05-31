using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(connectionString));

            builder.Services.AddTransient<IProductRepository, FakeProductRepository>();

            builder.Services.AddTransient<IProductRepository, EFProductRepository>();

            builder.Services.AddTransient<SeedData>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<SeedData>();

                dbInitializer.EnsurePopulated(app);
            }


            //app.MapControllerRoute(

            //    name: "default",
            //    pattern: "{controller=Product}/{action=List}/{id?}");

            //SeedData.EnsurePopulated(app);

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{category}/Page{productPage:int}",
                    defaults: new {controller = "Product", action = "List"});

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{category}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

            app.Run();



        }
    }
}
