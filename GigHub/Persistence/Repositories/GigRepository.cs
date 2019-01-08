using GigHub.Core.Models;
using GigHub.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string userId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == userId &&
                            g.DateTime > DateTime.Now &&
                            !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances)
                .ThenInclude(a => a.Attendee)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigs(string searchTerm = null)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                        g.Artist.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                        g.Genre.Name.ToLower().Contains(searchTerm.ToLower()) ||
                        g.Venue.ToLower().Contains(searchTerm.ToLower()));
            }

            return upcomingGigs.ToList();
        }
    }
}
