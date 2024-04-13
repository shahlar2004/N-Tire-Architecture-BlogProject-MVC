using Lesson.Data.Context;
using Lesson.Data.Repostories.Abstraction;
using Lesson.Data.Repostories.Concrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Data.UnitOfWorks
{
    public  class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async ValueTask DisposeAsync()
        {
            await appDbContext.DisposeAsync();
        }

        public int Save()
        {
            return appDbContext.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return appDbContext.SaveChangesAsync();
        }

        IRepository<T> IUnitOfWork.GetRepository<T>()
        {
            return new Repository<T>(appDbContext);
        }
    }
}
