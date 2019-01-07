using GigHub.Core.Repositories;

namespace GigHub.Core
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