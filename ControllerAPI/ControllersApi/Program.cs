using ControllersApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;

namespace ControllersApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IRepository, MemoryRepository>();


            // Add services to the container.
            builder.Services.AddRazorPages().AddXmlDataContractSerializerFormatters()
                .AddMvcOptions(opt =>
                {
                    opt.FormatterMappings.SetMediaTypeMappingForFormat("xml", 
                        new MediaTypeHeaderValue("application/xml"));
                    opt.RespectBrowserAcceptHeader = true;
                    opt.ReturnHttpNotAcceptable = true;
                });


            builder.Services.AddSession();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseRouting();

            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
                .WithStaticAssets();

            using (var scope = app.Services.CreateScope())
            {

            }


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();

        }
    }
}

