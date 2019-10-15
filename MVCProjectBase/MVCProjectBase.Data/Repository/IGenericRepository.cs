using Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCProjectBase.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// DbSet
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetDbSet();

        /// <summary>
        /// Tüm kayıtlar.
        /// </summary>
        /// <returns></returns>
        GenericResponse<IEnumerable<TEntity>> Select();

        /// <summary>
        /// Id değeri ile Kayıt bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GenericResponse<TEntity> SelectByKey(int id);

        /// <summary>
        /// Filtreleme
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        GenericResponse<IEnumerable<TEntity>> SelectByColumns(Expression<Func<TEntity,bool>> predicate);
        
        /// <summary>
        /// Kayıt ekle.
        /// </summary>
        /// <param name="entity"></param>
        GenericResponse<TEntity> Insert(TEntity entity);

        /// <summary>
        /// Kayıtlar ekle.
        /// </summary>
        /// <param name="entity"></param>
        GenericResponse<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Kayıt güncelle.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        GenericResponse<TEntity> Update(TEntity entityToUpdate);

        /// <summary>
        /// Kayıt güncelle.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        //void Update(int Id);

        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="id">Kayıt id</param>
        //void Delete(int id);

        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="entityToDelete">Kayıt</param>
        GenericResponse<TEntity> Delete(TEntity entityToDelete);

        /// <summary>
        /// Kayıtları Sil
        /// </summary>
        /// <param name="entities">Kayıtlar</param>
        GenericResponse<IEnumerable<TEntity>> Delete(IEnumerable<TEntity> entities);

    }
}
