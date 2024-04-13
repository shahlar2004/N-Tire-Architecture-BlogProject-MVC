using Lesson.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lesson.Data.Mapping
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(new Article
            {
                Id = Guid.NewGuid(),
                Tutle = "C# lesson-1",
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer" +
               " took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting," +
               " remaining essentially unchanged.",
                ViewCount = 16,
                CategoryId = Guid.Parse("{7BFB7CB1-748F-4728-816E-354AA2388054}"),
                ImageId = Guid.Parse("{53C70E42-4494-47E0-8391-43AED02DADD3}"),
                CreatedBy = "Jako ismo",
                CreatedDate = DateTime.Now,
                isDeleted = false,
                UserId = Guid.Parse("{E0C8114C-578E-4D0A-84D9-D936E0F34A7C}")
            },
           new Article
           {
               Id = Guid.NewGuid(),
               Tutle = "C# lesson-2",
               Content = "It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including" +
               " versions of Lorem Ipsum.",
               ViewCount = 16,
               CategoryId = Guid.Parse("{7BFB7CB1-748F-4728-816E-354AA2388054}"),
               ImageId = Guid.Parse("{53C70E42-4494-47E0-8391-43AED02DADD3}"),
               CreatedBy = "Jako ismo",
               CreatedDate = DateTime.Now,
               isDeleted = false,
               UserId = Guid.Parse("{E0C8114C-578E-4D0A-84D9-D936E0F34A7C}")
           }
           );
        }
    }
}
