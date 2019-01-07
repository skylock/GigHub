using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        IEnumerable<Following> GetFollowings(string userId, string gigArtistId);
    }
}