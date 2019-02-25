using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReferensApp.Models;

namespace ReferensApp.DAL
{
    public class ReferenceRepository : IReferenceRepository
    {
        private readonly ReferenceDBContext _context;
        public ReferenceRepository(ReferenceDBContext context)
        {
            _context = context;
        }

        public void CreateMeeting(Meeting meeting)
        {
            if (_context.Meetings.Where(m => m.Date == meeting.Date).ToList().Count() == 0)
            {
                _context.Meetings.Add(meeting);
                _context.SaveChanges();
            }
        }

        public void DeleteMeeting(int Id)
        {
            var meeting = _context.Meetings.Where(m => m.MeetingId == Id).FirstOrDefault();
            if (meeting != null)
            {
                _context.Meetings.Remove(meeting);
                _context.SaveChanges();
            }
        }

        public List<Meeting> GetAllMeetings()
        {
            return _context.Meetings.ToList();
        }

        public void UpdateMeeting(Meeting meeting)
        {
            var meetingToChange = _context.Meetings.SingleOrDefault(m => m.Date == meeting.Date);

            if (meetingToChange != null)
            {
                meetingToChange.BookedBy = meeting.BookedBy;
                meetingToChange.IsBooked = true;
                _context.SaveChanges();
            }

        }
    }
}