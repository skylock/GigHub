using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Persistence.Repositories
{
    public interface IAttendancesRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        IEnumerable<Attendance> GetAttendance(int gigId, string userId);
    }
}