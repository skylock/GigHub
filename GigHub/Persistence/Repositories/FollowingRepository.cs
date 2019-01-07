using GigHub.Data;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Following> GetFollowings(string userId, string gigArtistId)
        {
            return _context.Followings
                .Where(f => f.FolloweeId == gigArtistId && f.FollowerId == userId)
                .ToList();
        }
    }
}
