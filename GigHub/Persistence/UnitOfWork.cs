﻿using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IAttendancesRepository Attendances { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IFollowingRepository Following { get; private set; }
        public INotificationsRepository Notifications { get; private set; }
        public IUserNotificationsRepository UserNotifications { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Genres = new GenreRepository(context);
            Attendances = new AttendancesRepository(context);
            Following = new FollowingRepository(context);
            Notifications = new NotificationsRepository(context);
            UserNotifications = new UserNotificationsRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
