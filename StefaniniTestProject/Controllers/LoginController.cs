using System;
using System.Web.Mvc;
using System.Web.Security;
using StefaniniTestProject.Models;
using StefaniniTestProject.Repositories;

namespace StefaniniTestProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepository _loginRepository;

        public LoginController()
        {
            _loginRepository = new LoginRepository();
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string error;
                if (_loginRepository.CanLogin(model.Email, FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "MD5"), out error))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if(!String.IsNullOrWhiteSpace(error))
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View("Index", model);
        }

        [HttpPost]
        public JsonResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}