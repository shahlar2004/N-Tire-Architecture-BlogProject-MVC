using Lesson.Data.UnitOfWorks;
using Lesson.Entity.Entities;
using Lesson.Service.Services.Abstraction;
using Lesson.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lesson.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService articleService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IArticleService articleService1;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService, IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            this.articleService = articleService;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {

            var articles = await articleService.GetAllByPagingAsync(categoryId, currentPage, pageSize, isAscending);
            var a = 5;
            return  View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {
            var articles = await articleService.GetSearch(keyword, currentPage, pageSize, isAscending);
            return View(articles);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(Guid Id)
        {
            var ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            var articeVisitors = await unitOfWork.GetRepository<ArticleVisitor>().GetAllAsync(null, x => x.Visitor, y => y.Article);
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => x.Id == Id);
            var result = await articleService.GetArticlesWithCategoryNotDeletedAsync(Id);

            var visitor = await unitOfWork.GetRepository<Visitor>().GetAsync(x => x.IpAddress == ipAddress);

            var addArticleVisitors = new ArticleVisitor(article.Id, visitor.Id);

            if (articeVisitors.Any(x => x.VisitorId == addArticleVisitors.VisitorId && x.ArticleId == addArticleVisitors.ArticleId))
                return View(result);
            else
            {
                await unitOfWork.GetRepository<ArticleVisitor>().AddAsync(addArticleVisitors);
                article.ViewCount += 1;
                await unitOfWork.GetRepository<Article>().UpdateAsync(article);
                await unitOfWork.SaveAsync();
            }

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
