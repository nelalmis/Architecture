using MVCProjectBase.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Architecture;

namespace MVCProjectBase.Data.Repository
{
    public class GenericRepository<TEntity> : BusinessBaseClass, IGenericRepository<TEntity> where TEntity : class
    {
        private readonly MvcProjectBaseContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly string className = "MvcProjectBase.Data.Repository.GenericRepository<T>";
        public GenericRepository(MvcProjectBaseContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetDbSet()
        {
            return _dbSet;
        }

        /// <summary>
        /// Tüm kayıtlar.
        /// </summary>
        /// <returns></returns>
        public virtual GenericResponse<IEnumerable<TEntity>> Select()
        {
            GenericResponse<IEnumerable<TEntity>> returnObject;
            returnObject = this.InitializeGenericResponse<IEnumerable<TEntity>>(className + ".Select");
            try
            {
                returnObject.Value = _dbSet.ToList();
            }
            catch (FormatException e)
            {
                returnObject.Results.Add(e.Message);
            }
            return returnObject;
        }

        /// <summary>
        /// Id değerine göre Kayıt bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GenericResponse<TEntity> SelectByKey(int id)
        {
            GenericResponse<TEntity> returnObject;
            returnObject = this.InitializeGenericResponse<TEntity>(className + ".SelectByKey");
            try
            {
                returnObject.Value = _dbSet.Find(id);
            }
            catch (FormatException e)
            {
                returnObject.Results.Add(e.Message);
            }
            return returnObject;
        }

        /// <summary>
        /// SelectByColumns
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual GenericResponse<IEnumerable<TEntity>> SelectByColumns(Expression<Func<TEntity, bool>> predicate)
        {
            GenericResponse<IEnumerable<TEntity>> returnObject;
            returnObject = this.InitializeGenericResponse<IEnumerable<TEntity>>(className + ".Select");
            try
            {
                returnObject.Value=_dbSet.Where(predicate);
            }
            catch (FormatException e)
            {
                returnObject.Results.Add(e.Message);
            }
            return returnObject;

        }

        /// <summary>
        /// Kayıt ekle.
        /// </summary>
        /// <param name="entity"></param>
        public virtual GenericResponse<TEntity> Insert(TEntity entity)
        {
            GenericResponse<TEntity> returnObject;
            returnObject = this.InitializeGenericResponse<TEntity>(className + ".SelectByKey");
            try
            {
                returnObject.Value=_dbSet.Add(entity);
            }
            catch (FormatException e)
            {
                returnObject.Results.Add(e.Message);
            }
            return returnObject;
        }

        /// <summary>
        /// Insert entities 
        /// </summary>
        /// <param name="entities"></param>
        public virtual GenericResponse<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> entities)
        {
            GenericResponse<IEnumerable<TEntity>> returnObject;
            returnObject = this.InitializeGenericResponse<IEnumerable<TEntity>>(className + ".SelectByKey");
            try
            {
                returnObject.Value = _dbSet.AddRange(entities);
            }
            catch (FormatException e)
            {
                returnObject.Results.Add(e.Message);
            }
            return returnObject;
        }

        /// <summary>
        /// Kayıt güncelle.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual GenericResponse<TEntity> Update(TEntity entityToUpdate)
        {
            GenericResponse<TEntity> returnObject;
            returnObject = this.InitializeGenericResponse<TEntity>(className + ".SelectByKey");
            try
            {
                 returnObject.Value=_dbSet.Attach(entityToUpdate);
                _context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            catch (FormatException e)
            {
                returnObject.Results.Add(e.Message);
            }
            return returnObject;

        }

        /* TODO: BURASI AÇILABİLİR.
        /// <summary>
        /// Kayıt güncelle.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(int userId)
        {
            var entity=_dbSet.Find(userId);
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        */

        /* TODO: BURASI AÇILABİLİR.
        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="id">Kayıt id</param>
        public virtual void Delete(int id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }
        */

        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="entityToDelete">Kayıt</param>
        public virtual GenericResponse<TEntity> Delete(TEntity entityToDelete)
        {
            GenericResponse<TEntity> returnObject;
            returnObject = this.InitializeGenericResponse<TEntity>(className + ".SelectByKey");
            try
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
               returnObject.Value= _dbSet.Remove(entityToDelete);
            }
            catch (FormatException e)
            {
                returnObject.Results.Add(e.Message);
            }
            return returnObject;
        }
        
        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities"></param>
        public virtual GenericResponse<IEnumerable<TEntity>> Delete(IEnumerable<TEntity> entities)
        {
            GenericResponse<IEnumerable<TEntity>> returnObject;
            returnObject = this.InitializeGenericResponse<IEnumerable<TEntity>>(className + ".SelectByKey");
            try
            {
                returnObject.Value=_dbSet.RemoveRange(entities);
            }
            catch (FormatException e)
            {
                returnObject.Results.Add(e.Message);
            }
            return returnObject;
        }
        
    }
}
