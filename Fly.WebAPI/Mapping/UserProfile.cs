using AutoMapper;
using Fly.Core.DataTransferObjects;
using Fly.Core.Entities;

namespace Fly.WebAPI.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserForRegistrationDto>().ReverseMap();
    }
}
