using Lesson.Entity.DTOs.Categories;
using Lesson.Entity.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Entity.DTOs.Articles
{
    public class ArticleAddDto
    {
        public string Tutle { get; set; }   
        public string Content { get; set; } 
        public Guid CategoryId { get; set; }  
        public IFormFile  Photo { get; set; }    
        public List<CategoryDTO> Categories { get; set; }
    }
}
 