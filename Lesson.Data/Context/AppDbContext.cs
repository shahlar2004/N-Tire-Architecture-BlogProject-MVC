using Lesson.Data.Mapping;
using Lesson.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Lesson.Data.Context
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,Guid,AppUserClaim,AppUserRole,AppUserLogin,AppRoleClaim,AppUserToken>
    {

        public AppDbContext()
        {
                            
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
           
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        // Replace "YourConnectionStringHere" with your actual SQL Server connection string
        //        string connectionString = "Server=WIN-OAMUUJJ1LER\\SQLEXPRESS;Database=LessonMVCDb;Trusted_Connection=True;";

        //        optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Lesson.Web"));
        //    }
        //}


        public DbContextOptions Options { get; }
        DbSet<Article> Articles { get; set; }
        DbSet<Category> Categories { get; set; }    
        DbSet<Image> images { get; set; }

        DbSet<Visitor> Visitors { get; set; } 

        DbSet<ArticleVisitor> ArticleVisitors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           //modelBuilder.ApplyConfiguration(new ArticleMap()); // bu sekilde tek tek daxil etmekvaxt itkisi oldugundan asagidaki kimi elave edilir.


            base.OnModelCreating(modelBuilder); //sonradan elave etdik: identity

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

           // modelBuilder.Entity<Article>().Property(x=>x.Tutle).HasMaxLength(50); birbasa burda da ede bilerik amma bu temiz kod yazmaq ucun yaxsi deyil.
           //elave class-da yazmagimiz daha yaxsidir.
        }
    }
}


