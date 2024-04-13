using Microsoft.EntityFrameworkCore;
using Lesson.Entity.Entities;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lesson.Data.Mapping
{
    //a4f6167a-e890-48ec-bc5f-53de9234f6e4
    public class ImageMap : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData(new Image
            {
                Id = Guid.Parse("{53C70E42-4494-47E0-8391-43AED02DADD3}"),

                FileName = "Images/BlogPhoto",
                FileType = "Jpg",
                CreatedBy = "Shahlar Ismayilov",
                CreatedDate = DateTime.Now,
            });
        }
    }
}
