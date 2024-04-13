using AutoMapper;
using Lesson.Entity.DTOs.Articles;
using Lesson.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.AutoMapper
{
    public class ArticleProfile:Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleDTO,Article>().ReverseMap();
            CreateMap<ArticleUpdateDTO,Article>().ReverseMap();
            CreateMap<ArticleUpdateDTO, ArticleDTO>().ReverseMap();
            CreateMap<ArticleAddDto,Article> ().ReverseMap();    
        }
    }
} 
