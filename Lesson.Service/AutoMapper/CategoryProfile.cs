using AutoMapper;
using Lesson.Entity.DTOs.Categories;
using Lesson.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.AutoMapper
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {

            CreateMap<CategoryDTO,Category>().ReverseMap();
            CreateMap<CategoryAddDTO, Category>().ReverseMap();
            CreateMap<CategoryAddDTO, CategoryDTO>().ReverseMap();
            CreateMap<CategoryUpdateDTO, Category>().ReverseMap();
            CreateMap<CategoryUpdateDTO, CategoryDTO>().ReverseMap();


        }
    }
}
