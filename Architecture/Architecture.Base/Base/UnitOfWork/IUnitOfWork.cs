using Architecture.Base.Repository;
using System;

namespace Architecture.Base.UnifOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable
        where TContext:ContextBase
    {
        IGenericRepository<TEntity, TContext1> GetRepository<TEntity, TContext1>()
        where TEntity : class
        where TContext1 : ContextBase; 
        int SaveChanges();
    }
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class;
        int SaveChanges();
    }
}
