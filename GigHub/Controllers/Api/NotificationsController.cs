using AutoMapper;
using GigHub.Core;
using GigHub.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace GigHub.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationsController(IUnitOfWork unitOfWork,
                                       IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var notifications = _unitOfWork.Notifications.GetNewNotificationsFor(userId);

            return notifications.Select(n => _mapper.Map<NotificationDto>(n));
        }



        [HttpPost]
        public IActionResult MarkAsRead()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var notifications = _unitOfWork.UserNotifications.GetUserNotificationsFor(userId);

            foreach (var notification in notifications)
            {
                notification.Read();
            }

            _unitOfWork.Complete();

            return Ok();
        }
    }
}