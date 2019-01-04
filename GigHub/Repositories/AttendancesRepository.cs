using GigHub.Data;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendancesRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendancesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public IEnumerable<Attendance> GetAttendance(int gigId, string userId)
        {
            return _context.Attendances
                .Where(a => a.GigId == gigId && a.AttendeeId == userId)
                .ToList();
        }
    }
}
