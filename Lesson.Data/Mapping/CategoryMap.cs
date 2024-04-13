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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category
            {
                Id = Guid.Parse("{7BFB7CB1-748F-4728-816E-354AA2388054}"),
                Name = "Microsoft programming language",
                CreatedBy = "Shahlar Ismayilov",
                CreatedDate = DateTime.Now,
            },

            new Category
            {
                Id = Guid.Parse("{9C2E31F7-FBF2-44D1-9C3D-321165616B47}"),
                Name = "Item2",
                CreatedBy = "Shahlar Ismayilov",
                CreatedDate = DateTime.Now,
            }

            );
        }
    }
}
