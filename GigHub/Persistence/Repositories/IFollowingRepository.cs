using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Persistence.Repositories
{
    public interface IFollowingRepository
    {
        IEnumerable<Following> GetFollowings(string userId, string gigArtistId);
    }
}