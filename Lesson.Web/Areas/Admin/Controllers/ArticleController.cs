using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Lesson.Data.UnitOfWorks;
using Lesson.Entity.DTOs.Articles;
using Lesson.Entity.Entities;
using Lesson.Service.Services.Abstraction;
using Lesson.Web.Areas.Consts;
using Lesson.Web.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Lesson.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {

        private readonly IArticleService articleService;
        private readonly IMapper mapper;
        private readonly IValidator<Article> validator;
        private readonly IToastNotification toastNotification;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ICategoryService CategoryService { get; }

        public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper, IValidator<Article> validator, IToastNotification toastNotification, IHttpContextAccessor httpContextAccessor)
        {
            this.articleService = articleService;
            CategoryService = categoryService;
            this.mapper = mapper;
            this.validator = validator;
            this.toastNotification = toastNotification;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin},{RoleConsts.User}")]
        public async Task<IActionResult> Index()
        {
            var articles = await articleService.GetAllArticlesWithCategoryNotDeletedAsync();
            return View(articles);
        }



        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Add()
        {
            var categories = await CategoryService.GetAllCategoriesNonDeletedAsync();
            return View(new ArticleAddDto { Categories = categories });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
        {
            var map = mapper.Map<Article>(articleAddDto);
            var validationResult = await validator.ValidateAsync(map);



            if (validationResult.IsValid)
            {
                await articleService.CreateArticle(articleAddDto);
                toastNotification.AddSuccessToastMessage(Messages.Article.Add(articleAddDto.Tutle), new ToastrOptions() { Title = "Uğurlu!" });
                return RedirectToAction("Index", "Article", new { Area = "Admin" });
            }


            else
            {
                validationResult.AddToModelState(ModelState);
            }

            var categories = await CategoryService.GetAllCategoriesNonDeletedAsync();
            return View(new ArticleAddDto { Categories = categories });
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Update(Guid articleId)
        {
            var article = await articleService.GetArticlesWithCategoryNotDeletedAsync(articleId);
            var categories = await CategoryService.GetAllCategoriesNonDeletedAsync();

            var articleUpdateDto = mapper.Map<ArticleUpdateDTO>(article);

            articleUpdateDto.Categories = categories;

            return View(articleUpdateDto);
        }





        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateDTO articleUpdateDTO)
        {
            var map = mapper.Map<Article>(articleUpdateDTO);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                var title = await articleService.UpdateArticleAsync(articleUpdateDTO);
                toastNotification.AddSuccessToastMessage(Messages.Article.Update(title), new ToastrOptions() { Title = "Uğurlu!" });
                return RedirectToAction("Index", "Article", new { Area = "Admin" });

            }

            else
            {
                result.AddToModelState(ModelState);
            }

            var categories = await CategoryService.GetAllCategoriesNonDeletedAsync();
            articleUpdateDTO.Categories = categories;
            return View(articleUpdateDTO);
        }



        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Delete(Guid articleId)
        {
            await articleService.SafeDeleteArticleAsync(articleId);
            return RedirectToAction("Index", "Article", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> DeletedArticle()
        {
            var articles = await articleService.GetAllDeletedArticlesWithCategoryAsync();
            return View(articles);
        }


        [HttpGet]
        public async Task<IActionResult> ArticleUndoDelete(Guid articleId)
        {

            await articleService.ArticleUndoDelete(articleId);
            return RedirectToAction("Index", "Article", new { Area = "Admin" });
        }



    }
}
