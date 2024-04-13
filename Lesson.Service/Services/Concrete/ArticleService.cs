using AutoMapper;
using AutoMapper.Execution;
using Lesson.Data.UnitOfWorks;
using Lesson.Entity.DTOs.Articles;
using Lesson.Entity.DTOs.Categories;
using Lesson.Entity.DTOs.Images;
using Lesson.Entity.Entities;
using Lesson.Entity.Enums;
using Lesson.Service.Extensions;
using Lesson.Service.Helpers.Images;
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
    public class ArticleService : IArticleService
    {
        private readonly  IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IImageHelper imageHelper;
        private readonly ClaimsPrincipal _user;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper,IHttpContextAccessor httpContextAccessor, IImageHelper imageHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor =  httpContextAccessor;
            this.imageHelper = imageHelper;
            _user =  httpContextAccessor.HttpContext.User;
        }



        public async Task<ArticleListDTO> GetAllByPagingAsync(Guid? categoryId, int currentPage=1, int pageSize=3, bool isAscending=false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;

         
            var articles = categoryId == null
                ? await unitOfWork.GetRepository<Article>().GetAllAsync(a => !a.isDeleted, a => a.Category, i => i.Image, u=>u.User)
                : await unitOfWork.GetRepository<Article>().GetAllAsync(a => a.CategoryId == categoryId && !a.isDeleted,
                    a => a.Category, i => i.Image, u => u.User);

            var sortedArticles = isAscending
                ? articles.OrderBy(a => a.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : articles.OrderByDescending(a => a.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new ArticleListDTO
            {
                Articles = sortedArticles,
                CategoryId = categoryId == null ? null : categoryId.Value,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = articles.Count,
                IsAscending = isAscending
            };
        }


        public async Task<ArticleListDTO> GetSearch(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;


            var articles = await unitOfWork.GetRepository<Article>().GetAllAsync(a => !a.isDeleted && (a.Content.Contains(keyword) || a.Tutle.Contains(keyword) || a.Category.Name.Contains(keyword)), a => a.Category, i => i.Image, u => u.User);
               

            var sortedArticles = isAscending
                ? articles.OrderBy(a => a.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : articles.OrderByDescending(a => a.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new ArticleListDTO
            {
                Articles = sortedArticles,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = articles.Count,
                IsAscending = isAscending
            };
        }





        public async Task CreateArticle(ArticleAddDto articleAddDTO)
        {
            //var UserId = Guid.Parse("14DF2E78-A93C-4BBF-B5FB-FDF4A42CF4FC");


            var UserId=   _user.GetLoggedInUserId();
            var UserEmail=_user.GetLoggedInEmail();
            Guid? imageId=null;
            if (articleAddDTO.Photo!=null)
            {
             var imageUpload = await imageHelper.Upload(articleAddDTO.Tutle,articleAddDTO.Photo,ImageType.Post);
             Image image = new Image(imageUpload.FullName,articleAddDTO.Photo.ContentType,UserEmail);
             await unitOfWork.GetRepository<Image>().AddAsync(image);
             imageId =image.Id;
            }

            var article = new Article(articleAddDTO.Tutle,articleAddDTO.Content, articleAddDTO.CategoryId, UserId,UserEmail);
            if (imageId is not null)
            {
                article.ImageId = imageId;
            }
            await unitOfWork.GetRepository<Article>().AddAsync(article);
            await unitOfWork.SaveAsync();
        }



        public async Task<List<ArticleDTO>> GetAllArticlesWithCategoryNotDeletedAsync()
        {
            var articles= await unitOfWork.GetRepository<Article>().GetAllAsync(x=>!x.isDeleted, x=>x.Category);
            var map = mapper.Map<List<ArticleDTO>>(articles);
            return map;
        }

        public async Task<ArticleDTO> GetArticlesWithCategoryNotDeletedAsync(Guid articleId)
        {
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.isDeleted && x.Id==articleId, x => x.Category, i=>i.Image);
            var map = mapper.Map<ArticleDTO>(article);
            return  map;
        }

       
        public async Task<string> UpdateArticleAsync(ArticleUpdateDTO articleUpdateDTO)
        {
          

            var UserEmail = _user.GetLoggedInEmail();
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.isDeleted && x.Id==articleUpdateDTO.Id, x => x.Category, i => i.Image );

            if (articleUpdateDTO.Photo != null)
            {
                imageHelper.Delete(article.Image.FileName);
                var imageUpload = await imageHelper.Upload(articleUpdateDTO.Tutle,articleUpdateDTO.Photo, ImageType.Post);
                var image = new Image(imageUpload.FullName,articleUpdateDTO.Photo.ContentType,UserEmail);
                await unitOfWork.GetRepository<Image>().AddAsync(image);
                article.ImageId = image.Id;
            }

            article.Tutle = articleUpdateDTO.Tutle;
            article.Content = articleUpdateDTO.Content;
            article.CategoryId= articleUpdateDTO.CategoryId;    
            article.ModifiedDate=DateTime.Now;
            article.ModifiedBy = UserEmail;
            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await unitOfWork.SaveAsync();
            return articleUpdateDTO.Tutle;
        }

        public async Task SafeDeleteArticleAsync(Guid articleId)
        {
            var UserEmail = _user.GetLoggedInEmail();
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.isDeleted && x.Id == articleId, x => x.Category);
            article.isDeleted = true;
            article.DeletedDate = DateTime.Now;
            article.DeletedBy = UserEmail;
            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<ArticleDTO>> GetAllDeletedArticlesWithCategoryAsync()
        {
            var deletedArticles = await unitOfWork.GetRepository<Article>().GetAllAsync(x=>x.isDeleted, x=>x.Category);
            var map=  mapper.Map <List<ArticleDTO>>(deletedArticles);
            return  map;
        }

        public async Task ArticleUndoDelete(Guid articleId)
        {
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => x.isDeleted && x.Id == articleId, x => x.Category);
            article.isDeleted = false;
            article.DeletedDate = null;
            article.DeletedBy = null;
            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await unitOfWork.SaveAsync();

        }

        public async Task<ArticleDTO> GetArticelDetalByGuid(Guid Id)
        {
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x=>!x.isDeleted && x.Id==Id, i=>i.Image, u=>u.User, c=>c.Category);
            var map=  mapper.Map<ArticleDTO>(article);
            return map;

        }
    }
}
