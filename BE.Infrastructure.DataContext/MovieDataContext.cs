using BE.Model.Entities;
using BE.Model.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infrastructure.DataContext
{
    public class MovieDataContext : DbContext
    {
        public MovieDataContext(DbContextOptions<MovieDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<LikeHistory> LikeHistory { get; set; }

        private void SetTimeToEntity()
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>())
            {
                entry.Entity.ModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.ModifiedDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedDate = DateTime.Now;
                }
            }
        }

        public override int SaveChanges()
        {
            SetTimeToEntity();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTimeToEntity();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
