using Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCProjectBase.Service.Base
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        GenericResponse<IEnumerable<TEntity>> Select();

        /// <summary>
        /// SelectByColumns
        /// </summary>
        /// <returns></returns>
        GenericResponse<IEnumerable<TEntity>> SelectByColumns(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// SelectByKey
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        GenericResponse<TEntity> SelectByKey(int Id);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="user"></param>
        GenericResponse<TEntity> Insert(TEntity entity);

        /// <summary>
        /// Update With Key
        /// </summary>
        /// <param name="user"></param>
        GenericResponse<TEntity> Update(int Id);

        /// <summary>
        /// Update With Entity
        /// </summary>
        /// <param name="user"></param>
        GenericResponse<TEntity> Update(TEntity entity);

        /// <summary>
        /// Delete With Id
        /// </summary>
        /// <param name="user">Kullanıcı</param>
        GenericResponse<TEntity> Delete(int Id);

        /// <summary>
        /// Delete With Entity
        /// </summary>
        /// <param name="user">Kullanıcı</param>
        GenericResponse<TEntity> Delete(TEntity entity);
    }
}
