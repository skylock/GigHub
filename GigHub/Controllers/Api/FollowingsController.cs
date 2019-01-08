using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GigHub.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class FollowingsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public FollowingsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Follow(FollowingDto dto)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var following = _unitOfWork.Following.GetFollowing(userId, dto.FolloweeId);

            if (following != null)
                return BadRequest("Following already exists.");

            following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _unitOfWork.Following.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Unfollow(string id)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var following = _unitOfWork.Following.GetFollowing(userId, id);

            if (following == null)
                return NotFound();

            _unitOfWork.Following.Remove(following);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}