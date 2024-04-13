using Lesson.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Entity.Entities
{
    public class Category:EntityBase
    {

        public Category()
        {
                
        }

        public Category(string Name)
        {
           this.Name = Name;
        }

        [MinLength(3)]
        public string Name { get; set; }    
        public ICollection<Article> Articles { get; set;}
    }
}
