using Architecture.Common.Types;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Architecture.Base.Repository
{
    public sealed class GenericRepository<TEntity,TContext> :ObjectHelperBase, IGenericRepository<TEntity,TContext>
        where  TEntity : ContractBase
        where TContext : ContextBase
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _dbSet;

        private readonly string className = "MvcProjectBase.Data.Repository.GenericRepository<T>";
        public GenericRepository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {         
            return _dbSet;
        } 
        /// <summary>
        /// Id değerine göre Kayıt bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  TEntity Find(int id)
        {
            return _dbSet.Find(id);           
        }
        /// <summary>
        /// Id değerine göre Kayıt bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Find(TEntity entity)
        {
            return _dbSet.Find(entity);
        }

        /// <summary>
        /// SelectByColumns
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> SelectByColumns(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        /// <summary>
        /// Kayıt ekle.
        /// </summary>
        /// <param name="entity"></param>
        public TEntity Insert(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        /// <summary>
        /// Insert entities 
        /// </summary>
        /// <param name="entities"></param>
        public IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            return _dbSet.AddRange(entities);
        }

        /// <summary>
        /// Kayıt güncelle.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public  TEntity Update(TEntity entityToUpdate)
        {
            var response = _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            return response;
        }

        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="entityToDelete">Kayıt</param>
        public TEntity Delete(TEntity entityToDelete)
        {
            DbEntityEntry dbEntityEntry = _context.Entry(entityToDelete);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _dbSet.Attach(entityToDelete); 
            }
            
            return _dbSet.Remove(entityToDelete);
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            var objects = _dbSet.Where<TEntity>(where).AsEnumerable();

            foreach (TEntity obj in objects)
                _dbSet.Remove(obj);

        }
        
        public IEnumerable<TEntity> GetAll(params string[] navigations)
        {

            var set = _dbSet.AsQueryable();

            foreach (string nav in navigations)

                set = set.Include(nav);

            return set.AsEnumerable();

        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, params string[] navigations)
        {
            var set = _dbSet.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);

            return set.Where(where).AsEnumerable();

        }
        public TEntity Get(Expression<Func<TEntity, bool>> where, params string[] navigations)
        {
            var set = _dbSet.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);

            return set.Where(where).FirstOrDefault<TEntity>();

        }

    }

    public class GenericRepository<TEntity> : ObjectHelperBase, IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly ContextBase _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly string className = "MvcProjectBase.Data.Repository.GenericRepository<T>";
        public GenericRepository(ContextBase context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }
        /// <summary>
        /// Id değerine göre Kayıt bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Find(int id)
        {
            return _dbSet.Find(id);
        }
        /// <summary>
        /// Id değerine göre Kayıt bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Find(TEntity entity)
        {
            return _dbSet.Find(entity);
        }

        /// <summary>
        /// SelectByColumns
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> SelectByColumns(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        /// <summary>
        /// Kayıt ekle.
        /// </summary>
        /// <param name="entity"></param>
        public TEntity Insert(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        /// <summary>
        /// Insert entities 
        /// </summary>
        /// <param name="entities"></param>
        public IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            return _dbSet.AddRange(entities);
        }

        /// <summary>
        /// Kayıt güncelle.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public TEntity Update(TEntity entityToUpdate)
        {
            var response = _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            return response;
        }

        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="entityToDelete">Kayıt</param>
        public TEntity Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            return _dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities"></param>
        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            return _dbSet.RemoveRange(entities);
        }
    }
}
