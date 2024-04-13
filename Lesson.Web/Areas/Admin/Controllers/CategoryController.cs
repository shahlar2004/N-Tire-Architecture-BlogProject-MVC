using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Lesson.Data.UnitOfWorks;
using Lesson.Entity.DTOs.Categories;
using Lesson.Entity.Entities;
using Lesson.Service.Extensions;
using Lesson.Service.Services.Abstraction;
using Lesson.Web.ResultMessages;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Lesson.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IValidator<Category> validator;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;

        public CategoryController(ICategoryService categoryService, IValidator<Category> validator, IMapper mapper, IToastNotification toastNotification)
        {
            this.categoryService = categoryService;
            this.validator = validator;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllCategoriesNonDeletedAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDTO categoryAddDTO)
        {
            var map = mapper.Map<Category>(categoryAddDTO);
            var validationResult = await validator.ValidateAsync(map);

            if (validationResult.IsValid)
            {
                await categoryService.CreateCategoryAsync(categoryAddDTO);
                toastNotification.AddSuccessToastMessage(Messages.Category.Add(categoryAddDTO.Name), new ToastrOptions() { Title = "Uğurlu" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }


            else
            {
                validationResult.AddToModelState(ModelState);
                return View();
            }

        }


        [HttpPost]

        public async Task<IActionResult> AddWithAjax([FromBody] CategoryAddDTO categoryAddDTO)
        {
            var map = mapper.Map<Category>(categoryAddDTO);
            var validationResult = await validator.ValidateAsync(map);


            if (validationResult.IsValid)
            {
                await categoryService.CreateCategoryAsync(categoryAddDTO);
                toastNotification.AddSuccessToastMessage(Messages.Category.Add(categoryAddDTO.Name), new ToastrOptions() { Title = "Uğurlu" });
                return Json(Messages.Category.Add(categoryAddDTO.Name));
            }

            else
            {
                toastNotification.AddErrorToastMessage(Messages.Category.Message(validationResult.Errors.First().ErrorMessage), new ToastrOptions() { Title = "Uğursuz" });
                return Json(validationResult.Errors.First().ErrorMessage);
            }

        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid categoryId)
        {
            var category = await categoryService.GetCategoryAsync(categoryId);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDTO category)
        {

            var map = mapper.Map<Category>(category);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await categoryService.UpdateCategoryAsync(category);
                toastNotification.AddSuccessToastMessage(Messages.Category.Update(category.Name), new ToastrOptions() { Title = "Uğurlu" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }

            else
            {
                result.AddToModelState(ModelState);
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            await categoryService.SafeCategoryDeleteAsync(categoryId);
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> DeletedCategory()
        {
            var categories = await categoryService.GetAllDeletedCategoriesAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid categoryId)
        {
            await categoryService.UndoDeleteCategoryAsync(categoryId);
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }
    }
}
