using BE.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        Task<int> SaveChangesAsync();

        int SaveChanges();

        IRepository<T> GetRepository<T>()
            where T : class;
    }
}
