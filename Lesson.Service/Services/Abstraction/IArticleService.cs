using Lesson.Entity.DTOs.Articles;
using Lesson.Entity.DTOs.Categories;
using Lesson.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.Services.Abstraction
{
    public interface IArticleService
    {
        Task<List<ArticleDTO>> GetAllArticlesWithCategoryNotDeletedAsync();

        Task<List<ArticleDTO>> GetAllDeletedArticlesWithCategoryAsync();
        Task CreateArticle(ArticleAddDto articleAddDTO);
        Task<ArticleDTO> GetArticlesWithCategoryNotDeletedAsync(Guid articleId);
        Task<string> UpdateArticleAsync(ArticleUpdateDTO articleUpdateDTO);
        Task SafeDeleteArticleAsync(Guid articleId);

        Task ArticleUndoDelete(Guid articleId);
        Task<ArticleListDTO> GetSearch(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false);
        Task<ArticleListDTO> GetAllByPagingAsync(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false);

        Task<ArticleDTO> GetArticelDetalByGuid(Guid Id);


    }
}
