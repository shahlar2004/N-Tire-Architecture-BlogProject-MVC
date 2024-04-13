using Lesson.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Data.Mapping
{
    public class RoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("AspNetRoles");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<AppRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();


            builder.HasData(
             new AppRole
             {
                 Id = Guid.Parse("81D91823-EB61-4D17-A1FC-8A286F88F6D4"),
                 Name = "Superadmin",
                 NormalizedName = "SUPERADMIN",
                 ConcurrencyStamp = Guid.NewGuid().ToString()
             },

            new AppRole
            {
                Id = Guid.Parse("4380FCF7-DF75-485F-888A-D7715BE71026"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },

             new AppRole
             {
                 Id = Guid.Parse("26C34F97-7D52-452B-8E70-48135D3756CD"),
                 Name = "User",
                 NormalizedName = "USER",
                 ConcurrencyStamp = Guid.NewGuid().ToString()
             }
            );
        }
    }
}
