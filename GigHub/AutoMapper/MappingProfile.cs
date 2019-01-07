using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}
