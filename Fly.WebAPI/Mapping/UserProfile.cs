using AutoMapper;
using Fly.Core.Entities;
using Fly.Shared.DataTransferObjects;

namespace Fly.WebAPI.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserForRegistrationDto>().ReverseMap();
    }
}
