using MVCProjectBase.Core.Domain.Entity;
using MVCProjectBase.Data.Repository;
using MVCProjectBase.Data.UnitOfWork;
using MVCProjectBase.Service.Base;
using System.Collections.Generic;
using System.Linq;

namespace MVCProjectBase.Service.Roles
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<User> _userRepository;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uow"></param>
        public RoleService(IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
            _userRepository = uow.GetRepository<User>();
        }
        
        /// <summary>
        /// Kullanıcıya göre roller.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IEnumerable<Role> GetRolesByUser(string userName)
        {
            return _userRepository.Select().Value.FirstOrDefault(x => x.UserName == userName).Roles.AsQueryable();
        }

        /// <summary>
        /// Kullanıcı role sahip mi.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool IsUserInRole(string userName, string roleName)
        {
            if (_userRepository.Select() != null)
            {
                var result = _userRepository.Select().Value.FirstOrDefault(x => x.UserName == userName).Roles;
                if (result != null)
                    return result.Any(x => x.RoleName == roleName);
            }
            return false;
        }

    }
}