using FluentValidation.AspNetCore;
using Lesson.Data.Context;
using Lesson.Data.Repostories.Abstraction;
using Lesson.Data.Repostories.Concrets;
using Lesson.Data.UnitOfWorks;
using Lesson.Service.Helpers.Images;
using Lesson.Service.Services.Abstraction;
using Lesson.Service.Services.Concrete;
using Lesson.Service.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;   
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection  LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly=Assembly.GetExecutingAssembly();
            services.AddScoped<IArticleService,ArticleService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IUsersService,UserService>(); 
            services.AddScoped<IImageHelper,ImageHelper>();
            services.AddScoped<IDashboardService,DashboardService>();
            services.AddAutoMapper(assembly);
            services.AddControllersWithViews().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<ArticleValidation>();
                opt.DisableDataAnnotationsValidation = true;
                opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("az");
            });
            return services;
        }
    }
}
