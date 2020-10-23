using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Database;
using backend.Entities;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    
    public class DbRepository<TDbContext> : IDbRepository where TDbContext : DbContext
    {
        protected TDbContext DbContext;

        public DbRepository(TDbContext context)
        {
            this.DbContext = context;
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : EntityBase
        {
            return DbContext.Set<TEntity>().AsQueryable();
        }

        public async ValueTask<TEntity> FindAsync<TEntity>(long id) where TEntity : EntityBase
        {
            return await DbContext.FindAsync<TEntity>(id);
        }

        public async Task<int> SaveAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
