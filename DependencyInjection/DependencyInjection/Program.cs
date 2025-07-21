using DependencyInjection.Infrastructure;
using DependencyInjection.Models;

namespace DependencyInjection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            TypeBroker.SetRepositoryType<AlternateRepository>();

            //builder.Services.AddTransient<IRepository>(provider =>
            //{
            //    if (builder.Environment.IsDevelopment())
            //    {
            //        var x = provider.GetService<MemoryRepository>();
            //        return x;
            //    }
            //    else
            //    {
            //        return new AlternateRepository();
            //    }
             
            //});

            builder.Services.AddSingleton<IRepository,MemoryRepository>();
            builder.Services.AddTransient<IModelStorage,DictionaryStorage>();
            builder.Services.AddTransient<ProductTotalizer>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
