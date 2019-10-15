using Architecture;
using MVCProjectBase.Data.Repository;
using MVCProjectBase.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MVCProjectBase.Service.Base
{
    public abstract class BaseService<TEntity> :BusinessBaseClass where TEntity : class
    {
        private readonly IUnitOfWork _baseUow;
        private readonly IGenericRepository<TEntity> _baseRepository;
        private readonly string className="class";
        /// <summary>
        /// Construnctor
        /// </summary>
        /// <param name="uow"></param>
        public BaseService(IUnitOfWork uow)
        {
            _baseUow = uow;
            _baseRepository = uow.GetRepository<TEntity>();
        }

        /// <summary>
        /// Tüm kullanıcılar.
        /// </summary>
        /// <returns></returns>
        public GenericResponse<IEnumerable<TEntity>> Select()
        {
            GenericResponse<IEnumerable<TEntity>> returnObject;
            returnObject = InitializeGenericResponse<IEnumerable<TEntity>>(className + ".Select");
            returnObject= _baseRepository.Select();
            return returnObject;
        }

        /// <summary>
        /// SelectByColumns
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public GenericResponse<IEnumerable<TEntity>> SelectByColumns(Expression<Func<TEntity, bool>> predicate)
        {
            return _baseRepository.SelectByColumns(predicate);
        }

        /// <summary>
        /// SelectByKey
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public GenericResponse<TEntity> SelectByKey(int userId)
        {
            return _baseRepository.SelectByKey(userId);
        }

        /// <summary>
        /// Kullanıcı ekle.
        /// </summary>
        /// <param name="user"></param>
        public GenericResponse<TEntity> Insert(TEntity entity)
        {
            return _baseRepository.Insert(entity);
        }

        /// <summary>
        /// Kullanıcı güncelle.
        /// </summary>
        /// <param name="user"></param>
        public GenericResponse<TEntity> Update(TEntity entity)
        {
            return _baseRepository.Update(entity);
        }

        /// <summary>
        /// Kullanıcı Güncelle
        /// </summary>
        /// <param name="userId"></param>
        public GenericResponse<TEntity> Update(int userId)
        {
            var entity = _baseRepository.SelectByKey(userId);
            if (!entity.Success || entity.Value==null)
                return entity;
            return _baseRepository.Update(entity.Value);
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="user">Kullanıcı</param>
        public GenericResponse<TEntity> Delete(TEntity entity)
        {
            return _baseRepository.Delete(entity);
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="userId">Kullanıcı Id</param>
        public GenericResponse<TEntity> Delete(int id)
        {
            var entity = _baseRepository.SelectByKey(id);
            if (!entity.Success || entity.Value == null)
                return entity;
            return _baseRepository.Delete(entity.Value);
        }
    }
}
