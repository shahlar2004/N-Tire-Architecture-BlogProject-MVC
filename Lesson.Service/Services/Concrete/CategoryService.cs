using AutoMapper;
using Lesson.Data.UnitOfWorks;
using Lesson.Entity.DTOs.Categories;
using Lesson.Entity.Entities;
using Lesson.Service.Extensions;
using Lesson.Service.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;

        public CategoryService( IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }

      

        public async Task<List<CategoryDTO>> GetAllCategoriesNonDeletedAsync()
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(x =>!x.isDeleted); ;
            var map= mapper.Map<List<CategoryDTO>>(categories);
            return map;
        }

        public async Task CreateCategoryAsync(CategoryAddDTO categoryAddDTO)
        {
            var category = new Category()
            {
                Name = categoryAddDTO.Name,
                CreatedBy=_user.GetLoggedInEmail(),
                CreatedDate = DateTime.Now,
            };
            await unitOfWork.GetRepository<Category>().AddAsync(category);
            await unitOfWork.SaveAsync();
        }


        public async Task<CategoryUpdateDTO> GetCategoryAsync(Guid categoryId)
        {
            var category = await unitOfWork.GetRepository<Category>().GetAsync(x=>!x.isDeleted && x.Id == categoryId);
            var map = mapper.Map<CategoryUpdateDTO>(category);
            return map;
        }

      
       public async Task UpdateCategoryAsync(CategoryUpdateDTO categoryUpdateDTO)
        {
            var category = await unitOfWork.GetRepository<Category>().GetAsync(x=>x.Id==categoryUpdateDTO.Id);
            category.Name = categoryUpdateDTO.Name; 
            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();
        }

        public async Task SafeCategoryDeleteAsync(Guid categoryId)
        {
            var category = await unitOfWork.GetRepository<Category>().GetAsync(x=>!x.isDeleted && x.Id==categoryId);
            category.isDeleted = true;
            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();
        }


        public async Task UndoDeleteCategoryAsync(Guid categoryId)
        {
            var category = await unitOfWork.GetRepository<Category>().GetAsync(x => x.isDeleted && x.Id == categoryId);
            category.isDeleted = false;
            category.DeletedBy = null;
            category.DeletedDate = null;
            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<CategoryDTO>> GetAllDeletedCategoriesAsync()
        {
           var articles = await unitOfWork.GetRepository<Category>().GetAllAsync(x=>x.isDeleted);
           var map= mapper.Map<List<CategoryDTO>>(articles);
           return map;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesNonDeletedTake24()
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.isDeleted);
            var map= mapper.Map<List<CategoryDTO>>(categories);
            return map.Take(24).ToList();

        }
    }
}
