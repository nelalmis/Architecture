using Architecture.Base.Repository;
using Architecture.Base.UnifOfWork;
using Architecture.Common.Types;
using Architecture.Helper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Architecture.Base
{
    public class ObjectHelper : ObjectHelperBase,IObjectHelper
    {
        public DBLayer DBLayer { get;  }
        public SQLDBHelper SQLDBHelper { get;}
        public ExecutionDataContext ExecutionDataContext { get; set; }
        public ObjectHelper()
        {
            DBLayer = new DBLayer();
            SQLDBHelper = new SQLDBHelper();
        }
        public ObjectHelper(ExecutionDataContext context)
        {
            ExecutionDataContext = context;
            DBLayer = new DBLayer();
            SQLDBHelper = new SQLDBHelper();
        }
        
    }
    
    public class ObjectHelper<TEntity> : ObjectHelper, IObjectHelper
        where TEntity : class
    {
        private ContextBase _context { get; set; }
        private IUnitOfWork _uow { get; set; }
        private IGenericRepository<TEntity> _baseRepository { get; set; }

        private readonly string className = "Architecture.Data.ObjectHelper<>";

        /// <summary>
        /// DbContext olan projeler için kullanılır.
        /// </summary>
        /// <param name="context"></param>
        public ObjectHelper(ExecutionDataContext executionDataContext, ContextBase context):base(executionDataContext)
        {
            _context = context;
            DbContextInitialize();
        }

        /// <summary>
        /// DbContextInitialize
        /// </summary>
        private void DbContextInitialize()
        {
            if (_context != null)
            {
                _uow = new UnitOfWork(_context);
                _baseRepository = _uow.GetRepository<TEntity>();
            }
        }

        /// <summary>
        /// DbContextCleanup
        /// </summary>
        public void DbContextCleanup()
        {
            if (_context != null && _uow != null)
            {
                _context.Dispose();
                _uow.Dispose();
            }
        }

        public int SaveChanges()
        {
            if (_context != null && _uow != null)
            {
                return _uow.SaveChanges();
            }

            return -1;
        }

        /// <summary>
        /// Tüm kullanıcılar.
        /// </summary>
        /// <returns></returns>
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

    public class ObjectHelpers : ObjectHelper, IObjectHelper
    {
        private ContextBase _context { get; set; }
        private IUnitOfWork _uow { get; set; }

        private readonly string className = "Architecture.Data.ObjectHelper<>";

        /// <summary>
        /// DbContext olan projeler için kullanılır.
        /// </summary>
        /// <param name="context"></param>
        public ObjectHelpers(ExecutionDataContext executionDataContext, ContextBase context) : base(executionDataContext)
        {
            _context = context;
            DbContextInitialize();
        }

        /// <summary>
        /// DbContextInitialize
        /// </summary>
        private void DbContextInitialize()
        {
            if (_context != null)
            {
                _uow = new UnitOfWork(_context);
            }
        }

        public IGenericRepository<TEntity> Resolve<TEntity>()
            where TEntity:class
        {
            return _uow.GetRepository<TEntity>();
        }

        /// <summary>
        /// DbContextCleanup
        /// </summary>
        public void DbContextCleanup()
        {
            if (_context != null && _uow != null)
            {
                _context.Dispose();
                _uow.Dispose();
            }
        }

        public int SaveChanges()
        {
            if (_context != null && _uow != null)
            {
                return _uow.SaveChanges();
            }
            return -1;
        }
    }
    public abstract class ObjectHelperBase:IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public  GenericResponse<T> InitializeGenericResponse<T>(string pointName)
        {
            GenericResponse<T> genericResponse = FactoryHelper.InitializeGenericResponse<T>(pointName);
            return genericResponse;
        }
    }
    public interface IObjectHelper
    {
        DBLayer DBLayer { get; }
        SQLDBHelper SQLDBHelper { get; }
        ExecutionDataContext ExecutionDataContext { get; set; }
    }
}
