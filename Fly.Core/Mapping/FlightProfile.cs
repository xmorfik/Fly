using AutoMapper;
using Fly.Core.DataTransferObjects;
using Fly.Core.Entities;

namespace Fly.Core.Mapping;

public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<Flight, FlightDTO>();
    }
}
