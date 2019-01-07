using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Persistence.Repositories
{
    public interface IGigRepository
    {
        Gig GetGig(int gigId);
        void Add(Gig gig);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
    }
}