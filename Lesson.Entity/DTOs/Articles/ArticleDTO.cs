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
    public class ArticleDTO
    {
        public Guid Id { get; set; }
        public string Tutle { get; set; }
        public string Content { get; set; }
        public CategoryDTO Category { get; set; }
        public DateTime CreatedDate { get; set; }   
        public Image Image { get; set; }
        public AppUser User { get; set; }   
        public string CreatedBy { get; set; }   
        public bool isDeleted { get; set; } 

        public int ViewCount { get; set; }  
    }
}
