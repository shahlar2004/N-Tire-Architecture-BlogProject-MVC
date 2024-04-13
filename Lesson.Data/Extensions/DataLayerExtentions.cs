using Lesson.Data.Context;
using Lesson.Data.Repostories.Abstraction;
using Lesson.Data.Repostories.Concrets;
using Lesson.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Data.Extensions
{
    public static  class DataLayerExtentions
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services,IConfiguration config)
        {
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddDbContext<AppDbContext>(op => op.UseSqlServer(config.GetConnectionString("Key")));
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            return services;
        }

       
    }
}
