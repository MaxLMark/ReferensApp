using ReferensApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReferensApp.Models.ViewModels
{

    public class DatePickerViewModel
    {
        private readonly IReferenceRepository _repo;
        public DatePickerViewModel(IReferenceRepository repo)
        {
            _repo = repo;
        }
        public List<DateTime> Days { get; set; }
        public List<Meeting> Dates { get; set; }
        public List<Meeting> ReservedTimes { get; set; }
        public List<DateTime> Hours { get; set; }
        public DatePickerViewModel GetDates()
        {
            var vm = new DatePickerViewModel(_repo);
            DateTime ListingStartDate = DateTime.Today;

            if (vm.Hours == null)
            {
                vm.Hours = new List<DateTime>();
            }

            var dates = Enumerable.Range(0, 15).Select(days => ListingStartDate.AddDays(days)).ToList();
            foreach (var date in dates)
            {
                var Dthours = Enumerable.Range(9, 8).Select(hours => date.AddHours(hours)).ToList();
                vm.Hours.AddRange(Dthours);
            }

            vm.Days = dates;
            vm.ReservedTimes = _repo.GetAllMeetings();

            return vm;
        }
    }
}