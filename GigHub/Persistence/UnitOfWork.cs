using GigHub.Data;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public GigRepository Gigs { get; private set; }
        public AttendancesRepository Attendances { get; private set; }
        public GenreRepository Genres { get; private set; }
        public FollowingRepository Following { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Genres = new GenreRepository(context);
            Attendances = new AttendancesRepository(context);
            Following = new FollowingRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
