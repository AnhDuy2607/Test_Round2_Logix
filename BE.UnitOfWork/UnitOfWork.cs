using BE.Infrastructure.DataContext;
using BE.Repository.Interface;
using BE.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BE.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MovieDataContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        private IDbContextTransaction _transaction;
        private ILogger<UnitOfWork> _logger;
        public UnitOfWork(MovieDataContext _dbContext, IServiceProvider _serviceProvider, ILogger<UnitOfWork> logger)
        {
            this._dbContext = _dbContext;
            this._serviceProvider = _serviceProvider;
            _logger = logger;
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
            }
        }
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return _serviceProvider.GetService<IRepository<T>>();
        }

        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}