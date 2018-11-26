using AutoMapper;
using GigHub.Models;

namespace GigHub.Controllers.Api
{
    public class SimpleMappings : Profile
    {
        public SimpleMappings()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}
