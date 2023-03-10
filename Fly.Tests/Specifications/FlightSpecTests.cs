using FluentAssertions;
using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Fly.Services;
using Fly.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fly.Tests.Api.Services;

public class FlightsSpecTests
{
    public readonly Mock<IRepository<Flight>> Repository = new();
    public readonly Mock<IRepository<Aircraft>> AircraftRepository = new();
    public readonly Mock<ILogger<FlightService>> Logger = new();
    public readonly Mock<IScheduleService<Flight>> ScheduleService = new();
    public readonly Mock<IFlightOnCreationService> FlightOnCreationService = new();
    public readonly List<Flight> Flights;

    public FlightsSpecTests()
    {
        Flights = FlightHelper.Flights;
    }

    [Fact]
    public async Task FlightListSpec_NoParameters_ReturnsAll()
    {
        var spec = new FlightListSpec(new FlightParameter(), null);
        var result = spec.Evaluate(Flights);

        result.Count().Should().Be(Flights.Count);
    }

    [Fact]
    public async Task FlightSpec_ReturnsOne()
    {
        var spec = new FlightSpec(1);
        var result = spec.Evaluate(Flights);

        result.Count().Should().Be(1);
    }

    [Fact]
    public async Task FlightSpec_ReturnsNothing()
    {
        var spec = new FlightSpec(-1);
        var result = spec.Evaluate(Flights);

        result.Count().Should().Be(0);
    }
}