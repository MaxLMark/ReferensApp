using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReferensApp.Models
{
    public class Meeting
    {
        [Key]
        [Column(Order = 0)]
        public int MeetingId { get; set; }
        public DateTime Date { get; set; }
        public bool IsBooked { get; set; }
        public string BookedBy { get; set; }

        //TODO: Fix this!
        public int Hour { get; set; }
    }
}