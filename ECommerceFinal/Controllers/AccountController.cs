using EcommerceEntities;
using ECommerceFinal.Models;
using EcommerceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace ECommerceFinal.Controllers
{
    public class AccountController : Controller
    {
        UserService userService = new UserService();

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserVM model)
        {
            User user = new User(); // since same values so yyeha
            user.Name = model.Name;
            user.Email = model.Email;
            user.Password = model.Password;

            var AllUsers = userService.GetUsers();
            // No same emails s

            userService.SaveUser(user);
            return RedirectToAction("Login");

        }


        [HttpPost]
        public ActionResult Login(UserVM model)
        {
            var Users = userService.GetUsers();

            User user = Users.Where(x => x.Email == model.Email && x.Password == model.Password).First();

            FormsAuthentication.SetAuthCookie(model.Email, false);


            return View();
        }



        public ActionResult SignOut( )
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }




    }
}