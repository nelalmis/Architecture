using Architecture;
using MVCProjectBase.Core.Domain;
using MVCProjectBase.Core.Domain.Entity;
using MVCProjectBase.Data.Repository;
using MVCProjectBase.Data.UnitOfWork;
using MVCProjectBase.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MVCProjectBase.Service.Users
{
    public class UserService:BaseService<User>,IUserService
    {
        private readonly string className = "MvcProjectBase.Data.Repository.GenericRepository<T>";

        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<User> _userRepository;
        public UserService(IUnitOfWork uow)
        :base(uow)
        {
            _uow = uow;
            _roleRepository = uow.GetRepository<Role>();
            _userRepository = uow.GetRepository<User>();
        }
        
        /// <summary>
        /// Role göre kullanıcılar.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public GenericResponse<IEnumerable<User>> SelectByRole(string roleName)
        {
            GenericResponse<IEnumerable<User>> returnObject;
            returnObject = this.InitializeGenericResponse<IEnumerable<User>>(className + ".Select");

            var response = _roleRepository.SelectByColumns(u => u.RoleName == roleName);
            if (!response.Success)
            {
                returnObject.Results.AddRange(response.Results);
            }
            returnObject.Value = response.Value.FirstOrDefault().Users;
            return returnObject;
        }

        /// <summary>
        /// Kullanıcı sistemde kayıtlı mı.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public GenericResponse<User> ValidateUser(string userName, string password)
        {
            GenericResponse<User> returnObject;
            returnObject = this.InitializeGenericResponse<User>(className + ".Select");
                        
            returnObject.Value = _userRepository.GetDbSet().FirstOrDefault(x => x.UserName == userName && x.Password == password);

            return returnObject;
        }

        /// <summary>
        /// Kullanıcı sistemde kayıtlı mı.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public GenericResponse<bool> IsValidateUser(string userName, string password)
        {
            GenericResponse<bool> returnObject;
            returnObject = this.InitializeGenericResponse<bool>(className + ".Select");
            var response = _userRepository.SelectByColumns(x => x.UserName == userName && x.Password == password);
            if (!response.Success)
            {
                returnObject.Results.AddRange(response.Results);
            }
            returnObject.Value = response.Value.Any();
            return returnObject;
        }

        /// <summary>
        /// Kullanıcı Email sistemde kayıtlı mı.
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public GenericResponse<bool> ValidateEmail(string email)
        {
            GenericResponse<bool> returnObject;
            returnObject = this.InitializeGenericResponse<bool>(className + ".Select");
            var response = _userRepository.SelectByColumns(x => x.Email==email);
            if (!response.Success)
            {
                returnObject.Results.AddRange(response.Results);
            }
            returnObject.Value = response.Value.Any();
            return returnObject;
        }

        /// <summary>
        /// SelectByGuid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public GenericResponse<User> SelectByGuid(Guid guid)
        {
            GenericResponse<User> returnObject;
            returnObject = this.InitializeGenericResponse<User>(className + ".Select");
            
            returnObject.Value = _userRepository.GetDbSet().FirstOrDefault(u => u.ConfirmationId == guid);
            return returnObject;
           
        }

        /// <summary>
        /// Yeni üye olan kullanıcıya onay mesajı gönder.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public void SendConfirmationMail(int userId, string email, string ConfirmationUrl)
        {
            var userResponse = _userRepository.SelectByKey(userId);
            if (userResponse.Success && userResponse.Value!=null)
            {
                string confirmationId = userResponse.Value.ConfirmationId.ToString();
                ConfirmationUrl += "/Account/ConfirmUser?confirmationId=" + confirmationId;

                var message = new MailMessage("elalmis.ne@gmail.com", email)
                {
                    Subject = "Lütfen e-posta adresinizi onaylayınız.",
                    Body = ConfirmationUrl
                };

                var client = new SmtpClient();

                client.Send(message);
            }
           
        }
        
        
    }

}
