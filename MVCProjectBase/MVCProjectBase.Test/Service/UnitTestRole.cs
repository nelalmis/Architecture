using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCProjectBase.Core.Domain.Entity;
using MVCProjectBase.Data.Context;
using MVCProjectBase.Data.Repository;
using MVCProjectBase.Service.Roles;
using MVCProjectBase.Data.UnitOfWork;
using MVCProjectBase.Service.Users;

namespace MVCProject.Test
{
    [TestClass]
    public class UnitTestRole
    {
        private MvcProjectBaseContext _context;
        private IUnitOfWork _uow;
        private IRoleService _roleService;
        private IUserService _userService;
        [TestInitialize]
        public void TestInitialize()
        {
            _context = new MvcProjectBaseContext();
            _uow = new UnitOfWork(_context);
            _roleService = new RoleService(_uow);
            _userService = new UserService(_uow);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
            _uow.Dispose();
            _roleService = null;
            _userService = null;
        }

        [TestMethod]
        public void TestMethodInsertRole()
        {
            var role = new Role
            {
                RoleName="Admin"
            };

            _roleService.Insert(role);
            Assert.AreEqual(1, _uow.SaveChanges());

            _roleService.Delete(role);
            _uow.SaveChanges();
        }

        [TestMethod]
        public void TestMethodUpdateRole()
        {
            var role = new Role
            {
                RoleName = "Admin"
            };

            _roleService.Insert(role);
            _uow.SaveChanges();

            role.RoleName = "User";
            
            _roleService.Update(role);
            Assert.AreEqual(1, _uow.SaveChanges());

            var updatedUser = _roleService.SelectByKey(role.Id);
            Assert.AreEqual(role, updatedUser.Value);

            _roleService.Delete(role);
            _uow.SaveChanges();
        }

        [TestMethod]
        public void TestMethodDeleteRole()
        {
            var role = new Role
            {
                RoleName = "Admin"
            };

            _roleService.Insert(role);
            _uow.SaveChanges();

            _roleService.Delete(role);
            Assert.AreEqual(1, _uow.SaveChanges());
            Assert.IsNull(_roleService.SelectByKey(role.Id).Value);
        }

        [TestMethod]
        public void TestMethodIsUserInRole()
        {
            var user = new User
            {
                DisplayName = "test display name",
                Email = "test_email@mail.com",
                LastLoginDate = DateTime.Now,
                LastLoginIp = "192.168.1.1",
                Password = "12345",
                ProfileImageUrl = "profile image",
                UserName = "test_user_insert",
            };

            var role = new Role
            {
                RoleName = "Admin"
            };
          
            user.Roles.Add(role);
            _userService.Insert(user);
            _roleService.Insert(role);
            _uow.SaveChanges();

            Assert.IsTrue(_roleService.IsUserInRole(user.UserName, role.RoleName));

            _roleService.Delete(role);
            _userService.Delete(user);
            _uow.SaveChanges();
        }

    }
}
