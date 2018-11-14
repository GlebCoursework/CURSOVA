using CURSOVA.Models;
using CURSOVA.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CURSOVA.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public void test()
        {
            
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return PartialView("_LogIn");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> LogInPost(LoginModel loginModel)
        {
            if (ModelState.IsValid && loginModel.Login!=null)
            {
                ApplicationUser user = null;
                user = await UserManager.FindAsync(loginModel.Login, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Login or password are not valid");
                }
                else if (user.Bannes)
                {
                    ModelState.AddModelError("", "This User Is Banned");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                    return null;
                }
            }
            return PartialView("_LogIn", loginModel);
        }

        public ActionResult Index()
        {
            return View("View");
        }

        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Registration(RegistrationModel model)
        {
            if (ModelState.IsValid
                && await UserManager.FindByEmailAsync(model.Email) == null
                && await UserManager.FindByNameAsync(model.UserName) == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    Name = model.Name,
                    Surname = model.SurName,
                    BoughtLists = new List<BoughtList>(),
                    Bannes = false
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    UserManager.AddToRole(user.Id, "user");
                    return null;
                }
            }
            return PartialView("_Registration", model);
        }

        public ActionResult GetRegistration()
        {
            return PartialView("_Registration");
        }

        [Authorize]
        public ActionResult PersonalCabinet()
        {
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            UserModel userModel = new UserModel()
            {
                Email = user.Email,
                Login = user.UserName,
                Name = user.Name,
                Surname = user.Surname
            };
            return View(userModel);
        }

        [Authorize]
        public ActionResult ButtonsForChange()
        {
            return PartialView("_ButtonsForChange");
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetChangePassword()
        {
            return PartialView("_ChangePassword");
        }

        [Authorize]


        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindByName(User.Identity.Name);

                IdentityResult result = UserManager.ChangePassword(user.Id, changePasswordModel.OldPassword, changePasswordModel.NewPassword);

                if (result.Succeeded)
                {
                    return null;
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Bad password");
                }
            }

            return PartialView("_ChangePassword", changePasswordModel);
        }

        public ActionResult GetChangeEmail()
        {
            return PartialView("_ChangeEmail");
        }

        // неможна юзера мінять напряму тількі через юзерменеджера, да і заміна мила без підтвердження пароля не по-феншую 
        //public ActionResult ChangeEmail(ChangeEmailModel changeEmailModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ApplicationUser user = UserManager.FindByName(User.Identity.Name);
        //        user.Email = changeEmailModel.NewEmail;
        //        if (user.Email == changeEmailModel.NewEmail)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("NewEmail", "Some troubles happened while changing email address");
        //        }
        //    }
        //    return PartialView("_ChangeEmail", changeEmailModel);
        //}


        //поправлений варіант, добавлена перевірка пароля і чи нове мило не заняте
        //неможна держать 2 юзерів на 1 милі, потім ще для реєстрації допишу перевірки
        public ActionResult ChangeEmail(ChangeEmailModel changeEmailModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindByName(User.Identity.Name);
                IdentityResult result = null;
                if (UserManager.CheckPassword(user, changeEmailModel.Password) && UserManager.FindByEmail(changeEmailModel.NewEmail) == null)
                {
                    result = UserManager.SetEmail(user.Id, changeEmailModel.NewEmail);
                }
                if (result != null)
                {
                    if (result.Succeeded)
                    {
                        return null;
                    }
                    else
                    {
                        ModelState.AddModelError("NewEmail", "Some troubles happened while changing email address");

                    }
                }
                else
                {
                    ModelState.AddModelError("NewEmail", "this email is used");
                }
            }
            return PartialView("_ChangeEmail", changeEmailModel);
        }
    }
}