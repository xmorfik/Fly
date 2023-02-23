using AutoMapper;
using Fly.Core.Entities;
using Fly.Shared.DataTransferObjects;

namespace Fly.WebAPI.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserForRegistrationDto>().ReverseMap();
        CreateMap<AircraftLocation, LocationDto>().ReverseMap();
    }
}
