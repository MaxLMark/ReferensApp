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
            return View(GetDates());
        }

        [HttpPost]
        public ActionResult Index(string user, int month, int day, int hour)
        {
            var vm = new DatePickerViewModel();
            var dt = new DateTime(2019, month, day, hour, 0, 0);
            var newBooking = new Meeting()
            {
                Date = dt,
                BookedBy = user,
                IsBooked = true
            };

            _repo.CreateMeeting(newBooking);

            return View(GetDates());
        }

        [HttpPost]
        public ActionResult Delete(int bookingId)
        {
            //Delete booking
            _repo.DeleteMeeting(bookingId);

            return RedirectToAction("Index");
        }


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                //QuickFix to create Admin role
                //var roleManager = HttpContext.GetOwinContext().GetUserManager<RoleManager<AppRole>>();
                //roleManager.Create(new AppRole(login.User));

                //QuickFix 2 create User
                //var result = userManager.Create(new AppUser() { UserName = login.User, Email = "Admin@gmail.com" }, login.Password);
                //_repo.DeleteMeeting(25);

                userManager.AddToRole(userManager.FindByName("Admin").Id, "Admin");


                AppUser user = userManager.Find(login.User, login.Password);
                if (user != null)
                {
                    var ident = userManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    //use the instance that has been created. 
                    authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                    return Redirect(Url.Action("Index", "Home"));
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(login);
        }


        public DatePickerViewModel GetDates()
        {
            var vm = new DatePickerViewModel();
            DateTime ListingStartDate = DateTime.Today.AddDays(-3);
            
            if (vm.Hours == null)
            {
                vm.Hours = new List<DateTime>();
            }

            var dates = Enumerable.Range(0, 15).Select(days => ListingStartDate.AddDays(days)).ToList();
            foreach (var date in dates)
            {
                var Dthours = Enumerable.Range(9, 6).Select(hours => date.AddHours(hours)).ToList();
                vm.Hours.AddRange(Dthours);
            }

            vm.Days = dates;

            vm.ReservedTimes = _repo.GetAllMeetings();


            return vm;
        }
    }
}