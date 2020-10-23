using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Entities;

namespace backend.Interfaces
{
    public interface IDbRepository
    {
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IEntityBase;

        ValueTask<TEntity> FindAsync<TEntity>(long id) where TEntity : class, IEntityBase;

        Task<int> SaveAsync();

        void Add<TEntity>(TEntity entity) where TEntity : class, IEntityBase;

        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntityBase;

        void Update<TEntity>(TEntity entity) where TEntity : class, IEntityBase;
    }
}
