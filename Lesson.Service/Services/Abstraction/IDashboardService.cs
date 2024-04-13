using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.Services.Abstraction
{
    public interface IDashboardService
    {
        Task<List<int>> GetYearlyArticleCounts();

        Task<int> GetTotalyArticleCount();

        Task<int> GetTotalyCategoryCount();
    }
}
