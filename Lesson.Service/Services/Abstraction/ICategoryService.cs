using Lesson.Entity.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.Services.Abstraction
{
    public interface ICategoryService
    {
        Task <List<CategoryDTO>> GetAllCategoriesNonDeletedAsync();

        Task<List<CategoryDTO>> GetAllDeletedCategoriesAsync();

        Task CreateCategoryAsync(CategoryAddDTO categoryAddDTO);

        Task<CategoryUpdateDTO> GetCategoryAsync(Guid categoryId);

        Task UpdateCategoryAsync(CategoryUpdateDTO categoryUpdateDTO);

        Task SafeCategoryDeleteAsync(Guid categoryId);

        Task UndoDeleteCategoryAsync(Guid categoryId);

        Task<List<CategoryDTO>> GetAllCategoriesNonDeletedTake24();
    }
}
