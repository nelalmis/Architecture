using Architecture.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Architecture.Base.Repository
{
    public interface IGenericRepository<TEntity, TContext> 
        where TEntity : class
        where TContext : ContextBase
    {

        /// <summary>
        /// Tüm kayıtlar.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Id değeri ile Kayıt bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(int id);

        /// <summary>
        /// Entity değeri ile bul.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        TEntity Find(TEntity entity);

        /// <summary>
        /// Filtreleme
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> SelectByColumns(Expression<Func<TEntity,bool>> predicate);
        
        /// <summary>
        /// Kayıt ekle.
        /// </summary>
        /// <param name="entity"></param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Kayıtlar ekle.
        /// </summary>
        /// <param name="entity"></param>
        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Kayıt güncelle.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        TEntity Update(TEntity entityToUpdate);
        
        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="entityToDelete">Kayıt</param>
        TEntity Delete(TEntity entityToDelete);
        
    }

    public interface IGenericRepository<TEntity>
       where TEntity : class
    {

        /// <summary>
        /// Tüm kayıtlar.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Id değeri ile Kayıt bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(int id);

        /// <summary>
        /// Entity değeri ile bul.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        TEntity Find(TEntity entity);

        /// <summary>
        /// Filtreleme
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> SelectByColumns(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Kayıt ekle.
        /// </summary>
        /// <param name="entity"></param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Kayıtlar ekle.
        /// </summary>
        /// <param name="entity"></param>
        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Kayıt güncelle.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        TEntity Update(TEntity entityToUpdate);
        
        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="entityToDelete">Kayıt</param>
        TEntity Delete(TEntity entityToDelete);

        /// <summary>
        /// Kayıtları Sil
        /// </summary>
        /// <param name="entities">Kayıtlar</param>
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);
    }
}
