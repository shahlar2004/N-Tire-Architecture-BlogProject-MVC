using Lesson.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Entity.Entities
{
    public class Visitor:IEntityBase
    {
        public Visitor() { }

        public Visitor(string IpAddress, string UserAgent)
        {
            this.IpAddress = IpAddress;
            this.UserAgent = UserAgent;
        }

        public int Id { get; set; }
        public string IpAddress {  get; set; }
        public string UserAgent { get; set; }   
        public DateTime CreatedDate { get; set; }= DateTime.Now;

        public ICollection<ArticleVisitor> ArticleVisitors { get; set; }
       
    }
}
