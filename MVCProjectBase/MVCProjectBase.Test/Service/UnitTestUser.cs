using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCProjectBase.Data.Context;
using MVCProjectBase.Data.Repository;
using MVCProjectBase.Data.UnitOfWork;
using MVCProjectBase.Service.Users;
using MVCProjectBase.Core.Domain.Entity;

namespace MVCProjectBase.Test.Service
{
    [TestClass]
    public class UnitTestUser
    {
        private MvcProjectBaseContext _context;
        private IUnitOfWork _uow;
        private IUserService _userService;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new MvcProjectBaseContext();
            _uow = new UnitOfWork(_context);
            _userService = new UserService(_uow);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
            _uow.Dispose();
            _userService = null;
        }

        [TestMethod]
        public void TestMethodInsertUser()
        {
            var user = new User
            {
                DisplayName = "test display name",
                Email = "test_email@mail.com",
                LastLoginDate = DateTime.Now,
                LastLoginIp = "192.168.1.1",
                Password = "12345",
                ProfileImageUrl = "profile image",
                UserName = "test_user_insert"
            };

            _userService.Insert(user);
            Assert.AreEqual(1, _uow.SaveChanges());
           
            _userService.Delete(user);
            _uow.SaveChanges();
        }

        [TestMethod]
        public void TestMethodUpdateUser()
        {
            var user = new User
            {
                DisplayName = "test display name",
                Email = "test_email@mail.com",
                LastLoginDate = DateTime.Now,
                LastLoginIp = "192.168.1.1",
                Password = "12345",
                ProfileImageUrl = "profile image",
                UserName = "test_user_update"
            };

            _userService.Insert(user);
            _uow.SaveChanges();

            user.DisplayName = "new display name";
            user.LastLoginDate = DateTime.Now;
            _userService.Update(user);
            Assert.AreEqual(1, _uow.SaveChanges());

            var updatedUser = _userService.SelectByKey(user.Id);
            Assert.AreEqual(user, updatedUser.Value);

            _userService.Delete(user);
            _uow.SaveChanges();
        }

        [TestMethod]
        public void TestMethodDeleteUser()
        {
            var user = new User
            {
                DisplayName = "test display name",
                Email = "test_email@mail.com",
                LastLoginDate = DateTime.Now,
                LastLoginIp = "192.168.1.1",
                Password = "12345",
                ProfileImageUrl = "profile image",
                UserName = "test_user_delete"
            };

            _userService.Insert(user);
            _uow.SaveChanges();

            _userService.Delete(user);
            Assert.AreEqual(1, _uow.SaveChanges());
            Assert.IsNull(_userService.SelectByKey(user.Id).Value);
        }

        [TestMethod]
        public void TestMethodValidateUser()
        {
            var user = new User
            {
                DisplayName = "test display name",
                Email = "test_email@mail.com",
                LastLoginDate = DateTime.Now,
                LastLoginIp = "192.168.1.1",
                Password = "12345",
                ProfileImageUrl = "profile image",
                UserName = "test_user_to_validate"
            };

            _userService.Insert(user);
            _uow.SaveChanges();

            Assert.IsTrue(_userService.IsValidateUser(user.UserName, user.Password).Value);

            _userService.Delete(user);
            _uow.SaveChanges();
        }
    }
}

