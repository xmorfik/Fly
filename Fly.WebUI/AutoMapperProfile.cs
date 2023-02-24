using AutoMapper;
using Fly.Core.Entities;
using Fly.Shared.DataTransferObjects;
using Fly.WebUI.Models;

namespace Fly.WebUI;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Aircraft, CreateAircraftDto>().ReverseMap();
        CreateMap<SeatsDto, CreateAircraftDto>().ReverseMap();
    }
}
