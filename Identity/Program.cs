using Identity.Infrastructure;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            var connectionString = builder.Configuration.GetConnectionString("SportStoreIdentity");


            builder.Services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordValidation>();
            builder.Services.AddTransient<IUserValidator<AppUser>, CustomUserValidator>();

            builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
                opt.UseSqlServer(connectionString));

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
                {
                    //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
                    opt.User.RequireUniqueEmail = true;
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;

                })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapStaticAssets();
            app.MapRazorPages()
                .WithStaticAssets();

            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
