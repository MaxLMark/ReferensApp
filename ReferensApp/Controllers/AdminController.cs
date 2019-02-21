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
                IsBooked = false
            };

            _repo.CreateMeeting(newBooking);

            return View(GetDates());
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