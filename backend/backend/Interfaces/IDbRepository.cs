using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Entities;

namespace backend.Interfaces
{
    public interface IDbRepository 
    {
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : EntityBase;

        ValueTask<TEntity> FindAsync<TEntity>(long id) where TEntity : EntityBase;

        Task<int> SaveAsync();

        void Add<TEntity>(TEntity entity) where TEntity : EntityBase;

        void Delete<TEntity>(TEntity entity) where TEntity : EntityBase;

        void Update<TEntity>(TEntity entity) where TEntity : EntityBase;
    }
}
