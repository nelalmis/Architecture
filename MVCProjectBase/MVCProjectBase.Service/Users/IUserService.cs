using Architecture;
using MVCProjectBase.Core.Domain.Entity;
using MVCProjectBase.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCProjectBase.Service.Users
{
    public interface IUserService:IBaseService<User>
    {

        /// <summary>
        /// SelectByRole
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        GenericResponse<IEnumerable<User>> SelectByRole(string roleName);

        /// <summary>
        /// Kullanıcı sistemde kayıtlı mı.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        GenericResponse<User> ValidateUser(string userName, string password);

        /// <summary>
        /// IsValidateUser
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        GenericResponse<bool> IsValidateUser(string userName, string password);

        /// <summary>
        /// Email sistemde kayıtlı mı.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        GenericResponse<bool> ValidateEmail(string email);

        /// <summary>
        /// Yeni üye olan kullanıcıya onay mesajı gönder.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        void SendConfirmationMail(int userId, string email, string ConfirmationUrl);

        /// <summary>
        /// SelectByGuid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        GenericResponse<User> SelectByGuid(Guid guid);

    }
}

