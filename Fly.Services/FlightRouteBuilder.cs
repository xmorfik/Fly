using AutoMapper;
using Fly.Core.Entities;
using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using GeoCoordinatePortable;
using Microsoft.Extensions.Logging;

namespace Fly.Services;

public class FlightsRouteBuilder : IRouteBuilder<Flight, LocationDto>
{
    private readonly IMapper _mapper;
    private readonly ILogger<FlightsRouteBuilder> _logger;
    public FlightsRouteBuilder(
        IMapper mapper,
        ILogger<FlightsRouteBuilder> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    public LocationDto GetLocation(Flight flight)
    {
        var xDiff = flight.DepartureAirport.Latitude - flight.ArrivalAirport.Latitude;
        var yDiff = flight.ArrivalAirport.Longitude - flight.DepartureAirport.Longitude;
        var totalTimeSpan = flight.ArrivalDateTime - flight.DepartureDateTime;
        var timePassed = DateTime.Now - flight.DepartureDateTime;
        if (timePassed < TimeSpan.Zero)
        {
            timePassed = TimeSpan.Zero;
        }
        var progress = Math.Abs(Math.Round((double)(timePassed / totalTimeSpan), 2));
        var angle = CalculatePlaneAngle(flight);
        var startLatitude = flight.DepartureAirport.Latitude;
        var startLongitude = flight.DepartureAirport.Longitude;
        if (progress < 0)
        {
            progress = 0;
        }
        else if (progress > 1)
        {
            progress = 1;
        }
        var result = new LocationDto()
        {
            AircraftId = flight.AircraftId,
            FlightId = flight.Id,
            Latitude = startLatitude - xDiff * progress,
            Longitude = startLongitude + yDiff * progress,
            DateTime = DateTime.Now,
            DirectionAngle = angle
        };
        _logger.LogInformation($"Calculated location for flight {flight.Id} : {result.Latitude}/{result.Longitude}, angle : {result.DirectionAngle} on {DateTime.Now}, progress: {progress}");
        return result;
    }

    private int CalculateDistance(Flight flight)
    {
        var sCoord = new GeoCoordinate(flight.DepartureAirport.Latitude, flight.DepartureAirport.Longitude);
        var eCoord = new GeoCoordinate(flight.ArrivalAirport.Latitude, flight.ArrivalAirport.Longitude);
        return (int)sCoord.GetDistanceTo(eCoord);
    }

    private int CalculatePlaneAngle(Flight flight)
    {
        var xDiff = flight.ArrivalAirport.Latitude - flight.DepartureAirport.Latitude;
        var yDiff = flight.ArrivalAirport.Longitude - flight.DepartureAirport.Longitude;
        return (int)(Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI);
    }
}
