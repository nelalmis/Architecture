using MVCProjectBase.Core.Domain.Entity;
using MVCProjectBase.Service.Base;
using System.Collections.Generic;
using System.Linq;

namespace MVCProjectBase.Service.Roles
{
    public interface IRoleService:IBaseService<Role>
    {
        /// <summary>
        /// Kullanıcıya göre roller.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IEnumerable<Role> GetRolesByUser(string userName);

        /// <summary>
        /// Kullanıcı role sahip mi.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        bool IsUserInRole(string userName, string roleName);

    }
}

