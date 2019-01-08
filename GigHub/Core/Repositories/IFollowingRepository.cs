using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string followeeId);
        void Add(Following following);
        void Remove(Following following);
    }
}