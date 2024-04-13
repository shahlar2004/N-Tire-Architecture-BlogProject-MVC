using Lesson.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();





            var admin = new AppUser()
            {
                Id = Guid.Parse("E0C8114C-578E-4D0A-84D9-D936E0F34A7C"),
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                PhoneNumber = "+905439999988",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                FirstName = "Jako",
                LastName = "Ismo",
                SecurityStamp = Guid.NewGuid().ToString(),
                ImageId = Guid.Parse("{53C70E42-4494-47E0-8391-43AED02DADD3}")
            };


            var superadmin = new AppUser()
            {
                Id = Guid.Parse("{B5C0033F-E7F1-4610-A19C-FA970C039602}"),
                UserName = "superadmin@gmail.com",
                NormalizedUserName = "SUPERADMIN@GMAIL.COM",
                Email = "superadmin@gmail.com",
                NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                PhoneNumber = "+905439999988",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                FirstName = "Jako",
                LastName = "Ismo",
                SecurityStamp = Guid.NewGuid().ToString(),
                ImageId = Guid.Parse("{53C70E42-4494-47E0-8391-43AED02DADD3}")
            };



            admin.PasswordHash = CreatePasswordHash(admin, "123456");
            superadmin.PasswordHash = CreatePasswordHash(superadmin,"123456");

            builder.HasData(admin,superadmin);

        }

        private string CreatePasswordHash(AppUser user, string password)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
