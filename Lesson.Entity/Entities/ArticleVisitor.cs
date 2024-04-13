using Lesson.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Entity.Entities
{
    public class ArticleVisitor:IEntityBase
    {
        public ArticleVisitor() { }

        public ArticleVisitor(Guid ArticleId, int VisitorId)
        {
            this.ArticleId = ArticleId;
            this.VisitorId = VisitorId;
        }

        public Guid ArticleId { get; set; } 
        public Article Article { get; set; }
        public int VisitorId { get; set; }  
        public Visitor Visitor { get; set; }

    
    }
}
