using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IUserNotificationsRepository
    {
        IEnumerable<UserNotification> GetUserNotificationsFor(string userId);
    }
}