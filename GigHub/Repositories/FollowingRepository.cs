﻿using GigHub.Data;
using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowingRepository
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
