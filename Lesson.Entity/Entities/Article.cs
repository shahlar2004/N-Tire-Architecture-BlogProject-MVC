using Lesson.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Entity.Entities
{
    public class Article:EntityBase
    {


        public Article()
        {
                
        }

        public Article(string Tutle, string Content, Guid CategoryId, Guid UserId, string CreatedBy)
        {
            this.Tutle = Tutle;
            this.Content = Content;
            this.CategoryId = CategoryId;
            this.UserId = UserId;
            this.CreatedBy = CreatedBy;
            ViewCount = 0;
            ImageId = Guid.Parse("{53C70E42-4494-47E0-8391-43AED02DADD3}");
        }

        public Article(string Tutle, string Content, Guid CategoryId, Guid ImageId, Guid UserId,string CreatedBy)
        {
            this.Tutle = Tutle;
            this.Content = Content;
            this.CategoryId = CategoryId;
            this.ImageId = ImageId;
            this.UserId = UserId;
            this.CreatedBy = CreatedBy;
            ViewCount = 0;
        }


        public string Tutle { get; set; }   
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public Guid CategoryId { get; set; }    
        public Category Category { get; set; } 
        public Guid? ImageId { get; set; }   
        public Image Image { get; set; }   

        public Guid UserId { get; set; }    
        public AppUser User { get; set; }
        public ICollection<ArticleVisitor> ArticleVisitors { get; set; }


    }
}
 