using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IAttendancesRepository Attendances { get; }
        IGenreRepository Genres { get; }
        IFollowingRepository Following { get; }
        INotificationsRepository Notifications { get; }
        IUserNotificationsRepository UserNotifications { get; }
        void Complete();
    }
}