using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using ReferensApp.DAL;
using ReferensApp.Models;
using ReferensApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace ReferensApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReferenceRepository _repo;

        public HomeController(IReferenceRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            var vm = new DatePickerViewModel(_repo);
            return View(vm.GetDates());
        }

        [HttpPost]
        public ActionResult Index(string user, int month, int day, int hour)
        {
            var vm = new DatePickerViewModel(_repo);

            _repo.UpdateMeeting(new Meeting()
            {
                Date = new DateTime(2019, month, day, hour, 0, 0),
                BookedBy = user,
                IsBooked = true
            });

            return View(vm.GetDates());
        }

        [HttpPost]
        public ActionResult Delete(int bookingId)
        {
            //Delete booking
            if (bookingId != 0)
            {
                _repo.DeleteMeeting(bookingId);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (CreateUser(login))
            {
                return Redirect(Url.Action("Index", "Home"));
            }

            return View(login);
        }

        public bool CreateUser(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;
                var roleManager = HttpContext.GetOwinContext().GetUserManager<RoleManager<AppRole>>();

                //Add Admin Role if admin logs in
                if (!roleManager.RoleExists("Admin") && login.User == "Admin")
                {
                    roleManager.Create(new AppRole("Admin"));
                }

                //Create admin user and add to admin role
                if (userManager.Find(login.User, login.Password) == null)
                {
                    userManager.Create(new AppUser() { UserName = login.User, Email = "Admin@gmail.com" }, login.Password);

                    if (login.User == "Admin")
                    {
                        userManager.AddToRole(userManager.FindByName("Admin").Id, "Admin");
                    }
                }

                AppUser user = userManager.Find(login.User, login.Password);
                if (user != null)
                {
                    var ident = userManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    //use the instance that has been created. 
                    authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                    return true;
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return false;
        }
    }
}