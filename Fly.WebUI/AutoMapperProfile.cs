using AutoMapper;
using Fly.Core.Entities;
using Fly.Core.Parameters;
using Fly.Shared.DataTransferObjects;
using Fly.WebUI.Models;

namespace Fly.WebAPI.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Flight, FlightCreateViewModel>().ReverseMap();
        CreateMap<Aircraft, AircraftCreateViewModel>().ReverseMap();
        CreateMap<FlightParameter, FlightParameterViewModel>().ReverseMap();
    }
}
