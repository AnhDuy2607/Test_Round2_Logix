using BE.Infrastructure.DataContext;
using BE.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BE.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MovieDataContext _db;
        public Repository(MovieDataContext db)
        {
            _db = db;
        }

        public void Add(T entity)
        {
            _db.Entry(entity).State = EntityState.Added;
        }

        public IQueryable<T> AsQueryable()
        {
            return _db.Set<T>().AsQueryable();
        }

        public void Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}