using Architecture.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Architecture.Service
{
    public interface IBaseService<TEntity, TContext>
    {
        GenericResponse<IEnumerable<TEntity>> Select();
        GenericResponse<IEnumerable<TEntity>> SelectByColumns(Expression<Func<TEntity, bool>> predicate);
        GenericResponse<TEntity> SelectByKey(int id);
        GenericResponse<TEntity> Insert(TEntity entity);
        GenericResponse<TEntity> Update(TEntity entity);
        GenericResponse<TEntity> Update(int id);
        GenericResponse<TEntity> Delete(TEntity entity);
        GenericResponse<TEntity> Delete(int id);
        
    }
}
