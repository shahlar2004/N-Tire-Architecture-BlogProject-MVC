using Lesson.Core.Entities;
using Lesson.Data.Repostories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Data.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, IEntityBase, new();
        Task<int> SaveAsync();
        int Save();
    }
}
