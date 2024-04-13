using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Entity.DTOs.Categories
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }    
        public string Name {get; set;}
        public  string CreatedBy { get; set; }
        public  DateTime CreatedDate { get; set; }
        public bool isDeteled {  get; set; }    
    }
}
