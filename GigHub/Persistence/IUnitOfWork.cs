using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IAttendancesRepository Attendances { get; }
        IGenreRepository Genres { get; }
        IFollowingRepository Following { get; }
        void Complete();
    }
}