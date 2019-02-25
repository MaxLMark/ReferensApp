using ReferensApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferensApp.DAL
{
    public interface IReferenceRepository
    {
        List<Meeting> GetAllMeetings();
        void DeleteMeeting(int Id);
        void CreateMeeting(Meeting meeting);
        void UpdateMeeting(Meeting meeting);
    }
}
