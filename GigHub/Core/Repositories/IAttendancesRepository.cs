using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendancesRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        IEnumerable<Attendance> GetAttendance(int gigId, string userId);
    }
}