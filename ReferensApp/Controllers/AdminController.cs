using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ReferensApp.DAL;
using ReferensApp.Models;
using ReferensApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReferensApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IReferenceRepository _repo;

        public AdminController(IReferenceRepository repo)
        {
            _repo = repo;
        }

        [Route("Admin")]
        public ActionResult Index()
        {
            var vm = new DatePickerViewModel(_repo);

            return View(vm.GetDates());
        }

        [HttpPost]
        public ActionResult Index(string user, int month, int day, int hour, string button, int? bookingid)
        {
            var vm = new DatePickerViewModel(_repo);

            if (button == "Stäng bokning")
            {
                _repo.DeleteMeeting((int)bookingid);
                return View(vm.GetDates());
            }

            _repo.CreateMeeting(new Meeting()
            {
                Date = new DateTime(2019, month, day, hour, 0, 0),
                BookedBy = user,
                IsBooked = false
            });

            return View(vm.GetDates());
        }
    }
}