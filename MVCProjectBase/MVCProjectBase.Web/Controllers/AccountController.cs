using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVCProjectBase.Web.Models;
using MVCProjectBase.Data.UnitOfWork;
using MVCProjectBase.Service.Users;
using System.Web.Security;
using MVCProjectBase.Core.Domain.Entity;
using System.Text.RegularExpressions;
using MVCProjectBase.Service;
using MVCProjectBase.Service.Roles;

namespace MVCProjectBase.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        private readonly IRoleService _roleService;

        public AccountController(IUserService userService,IRoleService roleService, IUnitOfWork uow)
            : base(uow)
        {
            _userService = userService;
            _roleService = roleService;          
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   var roleResponse= _roleService.SelectByColumns(u => u.RoleName == "User");
                    
                    User user = new User
                    {
                        ConfirmationId = Guid.NewGuid(),
                        DisplayName = model.Email,
                        IsConfirmed = false,
                        LastLoginDate = DateTime.Now,
                        LastLoginIp = Request.UserHostAddress,
                        Password = model.Password,
                        ProfileImageUrl = "Content/Images/no_profile_image.png",
                        Email = model.Email,
                        UserName = model.Email,
                        IsActive = false,
                        IsEditable = true,
                        IsDeletable = true 
                                                          
                    };

                     user.Roles.Add(roleResponse.Value.FirstOrDefault());

                    _userService.Insert(user);
                    _uow.SaveChanges();
                    _userService.SendConfirmationMail(user.Id, user.Email, Request.Url.GetLeftPart(UriPartial.Authority));

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kullanıcı oluşturma başarısız!");
                }
            }
            
            return View(model);
        }
        
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel  model, string ReturnUrl)
        {
            var userResponse = _userService.ValidateUser(model.Email, model.Password);
            if (ModelState.IsValid && userResponse.Success&& userResponse.Value != null)
            {
                if (!userResponse.Value.IsConfirmed)
                {
                    TempData["EpostaOnayMesaj"] = "E-posta adresiniz onaylı değildir. Lütfen e-posta adresinizdeki linki kullanarak e-posta adresinizi onaylayınız.";

                    return View();
                }
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                return RedirectToLocal(ReturnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı ve ya şifre geçersiz!");
            }

            return View(model);
        }

        // RegisterModel içerisindeki Email alanını
        // RemoteAttribute ile kontrol eder
        public JsonResult ValidateEmail(string Email)
        {
            var responseValidate = _userService.ValidateEmail(Email);

            if (responseValidate.Success && responseValidate.Value)
            {
                return Json("Girdiğiniz e-posta adresi sistemde zaten mevcut!", JsonRequestBehavior.AllowGet);
            }

            return Json(!responseValidate.Value, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ConfirmUser(Guid confirmationId)
        {
            if (string.IsNullOrEmpty(confirmationId.ToString()) || (!Regex.IsMatch(confirmationId.ToString(),
                   @"[0-9a-f]{8}\-([0-9a-f]{4}\-){3}[0-9a-f]{12}")))
            {
                TempData["EpostaOnayMesaj"] = "Hesap geçerli değil. Lütfen e-posta adresinizdeki linke tekrar tıklayınız.";

                return View();
            }
            else
            {
                var userResponse = _userService.SelectByGuid(confirmationId);
                if (userResponse.Success && userResponse.Value != null)
                {
                    var user = userResponse.Value;
                    if (!user.IsConfirmed)
                    {
                        user.IsConfirmed = true;
                        _userService.Update(user);
                        _uow.SaveChanges();

                        FormsAuthentication.SetAuthCookie(user.UserName, true);
                        TempData["EpostaOnayMesaj"] = "E-posta adresinizi onayladığınız için teşekkürler. Artık sitemize üyesiniz.";

                        return RedirectToAction("Wellcome");
                    }
                    else
                    {
                        TempData["EpostaOnayMesaj"] = "E-posta adresiniz zaten onaylı. Giriş yapabilirsiniz.";

                        return RedirectToAction("Login");
                    }
                }
            }
            return View();

        }

        public ActionResult Wellcome()
        {
                        
            return View();
        }

        #region private methods
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    
        protected override void Dispose(bool disposing)
        {
            _uow.Dispose();
            base.Dispose(disposing);
        }
    }
}
