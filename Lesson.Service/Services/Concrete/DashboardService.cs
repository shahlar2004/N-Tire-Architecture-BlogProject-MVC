using Lesson.Data.UnitOfWorks;
using Lesson.Entity.Entities;
using Lesson.Service.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.Services.Concrete
{
    public class DashboardService:IDashboardService
    {
        private readonly IUnitOfWork unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<int>> GetYearlyArticleCounts()
        {
            var datas= new List<int>();
            var articles= await unitOfWork.GetRepository<Article>().GetAllAsync(x=>!x.isDeleted);

            var startDate = DateTime.Now.Date;
            startDate = new DateTime(startDate.Year, 1, 1);
            for (int i = 1; i <=12; i++)
            {
                var startedDate = new DateTime(startDate.Year,i,1);
                var endDate= startedDate.AddMonths(1);
                var data= articles.Where(x=>x.CreatedDate>=startedDate && x.CreatedDate<endDate).Count();


                datas.Add(data); 
             }


            return  datas;
        }


        public async Task<int> GetTotalyArticleCount()
        {
            var count = await unitOfWork.GetRepository<Article>().CountAsync();    
            return count;
        }

        public async Task<int> GetTotalyCategoryCount()
        {
            var count = await unitOfWork.GetRepository<Category>().CountAsync();
            return count;
        }
    }
}
