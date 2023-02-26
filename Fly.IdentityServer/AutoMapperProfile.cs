using AutoMapper;
using Fly.Core.Entities;
using Fly.Shared.DataTransferObjects;
using Fly.WebUI.Models;

namespace Fly.IdentityServer;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserForRegistrationDto, User>();
        CreateMap<UserForRegistrationDto, Passenger>();
        CreateMap<ManagerForRegistrationDto, User>();
        CreateMap<ManagerForRegistrationDto, Manager>();
    }
}

