using Lesson.Data.Context;
using Lesson.Data.Extensions;
using Lesson.Service.Extensions;
using Lesson.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Lesson.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using FluentValidation.AspNetCore;
using NToastNotify;
using Lesson.Web.ArticleVisitors;

namespace Lesson.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.
            builder.Services.AddControllersWithViews(opt =>
            {
                opt.Filters.Add<ArticleVisitorFilter>(); 
            }
                ).AddNToastNotifyToastr(new ToastrOptions()
            {
                PositionClass=ToastPositions.TopRight,
                TimeOut=3000
            })
               .AddRazorRuntimeCompilation();

            //Get Executing project name version
            //var assembly=Assembly.GetExecutingAssembly();

           // builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.LoadDataLayerExtension(builder.Configuration);
            builder.Services.LoadServiceLayerExtension();
            builder.Services.AddSession();// sonradan elave edildi: identity.



            builder.Services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
            })
            .AddRoleManager<RoleManager<AppRole>>()
            .AddEntityFrameworkStores<AppDbContext>() 
            .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = new PathString("/Admin/Auth/Login");
                config.LogoutPath = new PathString("/Admin/Auth/Logout");
                config.Cookie = new CookieBuilder()
                {
                    Name="YoutubeLesson",
                    HttpOnly=true,
                    SameSite=SameSiteMode.Strict,
                    SecurePolicy=CookieSecurePolicy.SameAsRequest
                };

                config.SlidingExpiration = true;
                config.ExpireTimeSpan=TimeSpan.FromDays(1);
                config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
                
            });
            

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseNToastNotify();  //notift-nin elave edilmesi

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseSession();// sonradan elave edildi: identity.
            app.UseRouting();
            app.UseAuthentication();  //sonradan elave etdik: Identity
            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.MapDefaultControllerRoute();



            app.UseEndpoints(endpoints => 
            {
                endpoints.MapAreaControllerRoute
                (
                    name:"Admin",
                    areaName:"Admin",
                    pattern:"Admin/{controller=Auth}/{action=Login}/{id?}"
                );

                endpoints.MapDefaultControllerRoute();
            });


            app.Run();
        }
    }
}