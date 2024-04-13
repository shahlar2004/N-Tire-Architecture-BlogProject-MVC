using Lesson.Service.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Lesson.Web.Areas.Admin.Controllers
{


    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IDashboardService dashboardService;

        public HomeController(IArticleService articleService, IDashboardService dashboardService)
        {
            this.articleService = articleService;
            this.dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await articleService.GetAllArticlesWithCategoryNotDeletedAsync();
            var test = await dashboardService.GetYearlyArticleCounts();
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> YearlyArticleCounts()
        {
            var count = await dashboardService.GetYearlyArticleCounts();
            return Json(JsonConvert.SerializeObject(count));
        }

        [HttpGet]
        public async Task<IActionResult> GetTotalyArticleCounts()
        {
            var count = await dashboardService.GetTotalyArticleCount();
            return Json(count);
        }


        [HttpGet]
        public async Task<IActionResult> GetTotalyCategoryCounts()
        {
            var count = await dashboardService.GetTotalyCategoryCount();
            return Json(count);
        }
    }
}
