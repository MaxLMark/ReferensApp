using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReferensApp.Models.ViewModels
{
    public class DatePickerViewModel
    {
        public List<DateTime> Days { get; set; }
        public List<Meeting> Dates { get; set; }
        public List<Meeting> ReservedTimes { get; set; }
        public List<DateTime> Hours { get; set; }

    }


}