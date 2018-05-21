using AltcoinPortfolio.Entities;
using AltcoinPortfolio.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace AltcoinPortfolio.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private PortfolioContext db = new PortfolioContext();

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult LoginUser(User user)
        {
            object result = null;
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var authManager = HttpContext.GetOwinContext().Authentication;
            User foundUser = null;
            string passFromDb = this.db.Users.Where(u => u.Email == user.Email).First().PasswordHash;
            if (!string.IsNullOrEmpty(passFromDb))
            {
                /* Extract the bytes */
                byte[] hashBytes = Convert.FromBase64String(passFromDb);
                /* Get the salt */
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                /* Compute the hash on the password the user entered */
                var pbkdf2 = new Rfc2898DeriveBytes(user.Password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                bool passIsSame = true;
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        passIsSame = false;
                        break;
                    }
                }

                if (passIsSame)
                {
                    foundUser = this.db.Users.Where(u => u.Email == user.Email).First();
                }
            }
            if (foundUser != null)
            {
                var ident = userManager.CreateIdentity(foundUser, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = false }, ident);

                result = new { user.Email };
                return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            var authMnger = HttpContext.GetOwinContext().Authentication;
            authMnger.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public JsonResult GetUser(Guid userId)
        {
            User user = this.db.Users.Where(u => u.Id == userId.ToString()).FirstOrDefault();
            if (user != null)
            {
                return this.Json(user, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new { empty = "Empty" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterUser(User user)
        {
            try
            {
                bool emailExists = this.CheckEmailExists(user.Email);
                if (!emailExists)
                {
                    user.Id = Guid.NewGuid().ToString();
                    user.UserName = user.Email;
                    byte[] salt;
                    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                    var pbkdf2 = new Rfc2898DeriveBytes(user.Password, salt, 10000);
                    byte[] hash = pbkdf2.GetBytes(20);
                    byte[] hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);
                    user.PasswordHash = Convert.ToBase64String(hashBytes);
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    this.db.Users.Add(user);
                    this.db.Portfolios.Add(new Portfolio()
                    {
                        UserId = user.Id,
                        Id = Guid.NewGuid()
                    });
                    db.SaveChanges();
                }
                else
                {
                    return Json(new { success = false, message = "User already exists!" });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Error occured while trying to register user" });
            }

            return Json(new { success = true, message = "User registered" });
        }

        private bool CheckEmailExists(string email)
        {
            var foundUser = this.db.Users.Where(u => u.Email == email).FirstOrDefault();
            if (foundUser != null)
                return true;
            else
                return false;
        }
    }
}