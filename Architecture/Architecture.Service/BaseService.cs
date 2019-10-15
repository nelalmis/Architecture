using Architecture.Base;
using Architecture.Common.Types;
using Architecture.Base.Repository;
using Architecture.Base.UnifOfWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Architecture.Service
{
    public class BaseService<TEntity,TContext> :ObjectHelperBase, IBaseService<TEntity,TContext> 
        where TEntity : class
        where TContext : ContextBase
    {
        private readonly IUnitOfWork<TContext> _baseUow;
        public readonly IGenericRepository<TEntity,TContext> _baseRepository;
        private readonly string className="Architecture.WithLinq.Service";        
        /// <summary>
        /// Construnctor
        /// </summary>
        /// <param name="uow"></param>
        public BaseService(IUnitOfWork<TContext> uow)
        {
            _baseUow = uow;
            _baseRepository = uow.GetRepository<TEntity,TContext>();
        }

        public virtual GenericResponse<IEnumerable<TEntity>> Select()
        {
            GenericResponse<IEnumerable<TEntity>> returnObject;
            returnObject = InitializeGenericResponse<IEnumerable<TEntity>>(className + ".Select");
            returnObject.Value = _baseRepository.GetAll();
            return returnObject;
        }

        /// <summary>
        /// SelectByColumns
        /// List veya FirstOrDefault çekilmesi için ToList() veya FirstOrDefault komutunun kullanılması lazım.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual GenericResponse<IEnumerable<TEntity>> SelectByColumns(Expression<Func<TEntity, bool>> predicate)
        {
            GenericResponse<IEnumerable<TEntity>> returnObject;
            returnObject = InitializeGenericResponse<IEnumerable<TEntity>>(className + ".SelectByColumns");
            returnObject.Value = _baseRepository.SelectByColumns(predicate);
            return returnObject;
        }

        /// <summary>
        /// SelectByKey
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual GenericResponse<TEntity> SelectByKey(int id)
        {
            GenericResponse<TEntity> returnObject;
            returnObject = InitializeGenericResponse<TEntity>(className + ".SelectByKey");
            returnObject.Value = _baseRepository.Find(id);
            return returnObject;
        }

        /// <summary>
        /// Kullanıcı ekle.
        /// </summary>
        /// <param name="user"></param>
        public virtual GenericResponse<TEntity> Insert(TEntity entity)
        {
            GenericResponse<TEntity> returnObject;
            returnObject = InitializeGenericResponse<TEntity>(className + ".Insert");
            returnObject.Value = _baseRepository.Insert(entity);
            return returnObject;
        }

        /// <summary>
        /// Kullanıcı güncelle.
        /// </summary>
        /// <param name="user"></param>
        public virtual GenericResponse<TEntity> Update(TEntity entity)
        {
            GenericResponse<TEntity> returnObject;
            returnObject = InitializeGenericResponse<TEntity>(className + ".Update");
            returnObject.Value = _baseRepository.Update(entity);
            return returnObject;
        }

        /// <summary>
        /// Kullanıcı Güncelle
        /// </summary>
        /// <param name="userId"></param>
        public virtual GenericResponse<TEntity> Update(int id)
        {
            GenericResponse<TEntity> returnObject;
            returnObject = InitializeGenericResponse<TEntity>(className + ".Update");
            var entity = _baseRepository.Find(id);
            if (entity == null)
            {
                returnObject.Results.Add(new KeyNotFoundException(id.ToString()));
            }
            returnObject.Value = _baseRepository.Update(entity);
            return returnObject;
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="user">Kullanıcı</param>
        public virtual GenericResponse<TEntity> Delete(TEntity entity)
        {
            GenericResponse<TEntity> returnObject;
            returnObject = InitializeGenericResponse<TEntity>(className + ".Delete");
            var response = _baseRepository.Find(entity);
            if (response == null)
            {
                returnObject.Results.Add(new KeyNotFoundException(typeof(TEntity).ToString()));
            }
            returnObject.Value = _baseRepository.Delete(entity);
            return returnObject;
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="userId">Kullanıcı Id</param>
        public virtual GenericResponse<TEntity> Delete(int id)
        {
            GenericResponse<TEntity> returnObject;
            returnObject = InitializeGenericResponse<TEntity>(className + ".Delete");
            var response = _baseRepository.Find(id);
            if (response == null)
            {
                returnObject.Results.Add(new KeyNotFoundException(typeof(TEntity).ToString()));
            }
            returnObject.Value = _baseRepository.Delete(response);
            return returnObject;
        }
    }    
}
