using AutoMapper;
using Fly.Core.Entities;
using Fly.Shared.DataTransferObjects;
using Fly.WebUI.Models;

namespace Fly.WebUI.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Aircraft, AircarftForCreationDto>().ReverseMap();
        CreateMap<SeatsDto, AircarftForCreationDto>().ReverseMap();
    }
}
