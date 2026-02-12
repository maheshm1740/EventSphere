using AutoMapper;
using DLEventSphere.DTO_s;
using DLEventSphere.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEventSphere
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegistration, User>();

            CreateMap<CreateEventDto, Event>();

            CreateMap<UpdateEvent, Event>()
                .ForMember(dest => dest.EventId, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizerId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<User, UserResponseDto>();

            CreateMap<Event, EventResponseDto>()
                .ForMember(dest => dest.OrganizerName,
                    opt => opt.MapFrom(src => src.Organizer.Name))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
