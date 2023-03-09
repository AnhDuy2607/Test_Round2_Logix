using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);

        IQueryable<T> AsQueryable();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
