using Ardalis.Specification;
using FluentAssertions;
using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Fly.Services;
using Fly.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fly.Tests.Api.Services;

public class FlightsServiceTests
{
    public readonly Mock<IRepository<Flight>> Repository = new();
    public readonly Mock<IRepository<Aircraft>> AircraftRepository = new();
    public readonly Mock<ILogger<FlightService>> Logger = new();
    public readonly Mock<IScheduleService<Flight>> ScheduleService = new();
    public readonly Mock<IFlightOnCreationService> FlightOnCreationService = new();
    public readonly List<Flight> Flights;
    public readonly Page Page = new Page() { PageSize = 5 };

    public FlightsServiceTests()
    {
        Flights = FlightHelper.Flights;
    }

    [Fact]
    public async Task GetListAsync_NoParameters_ReturnsPage()
    {
        Repository.Setup(x => x.ListAsync(
            It.IsAny<FlightListSpec>(), CancellationToken.None))
            .ReturnsAsync(Flights.Take(Page.PageSize).ToList());
        Repository.Setup(x => x.CountAsync(
            It.IsAny<FlightListSpec>(), CancellationToken.None))
            .ReturnsAsync(Flights.Count);

        var service = new FlightService(
            Repository.Object,
            Logger.Object,
            ScheduleService.Object,
            AircraftRepository.Object,
            FlightOnCreationService.Object);

        var result = await service.GetListAsync(new FlightParameter(), Page);
        var data = result;

        data.Count().Should().BeGreaterThanOrEqualTo(0);
        data.Count().Should().BeLessThanOrEqualTo(Page.PageSize);
        data.MetaData.TotalCount.Should().Be(Flights.Count);
    }

    [Fact]
    public async Task GetAsync_ReturnsOne()
    {
        Repository.Setup(x => x.FirstOrDefaultAsync(
            It.IsAny<FlightSpec>(), CancellationToken.None))
            .ReturnsAsync(Flights.FirstOrDefault());

        var service = new FlightService(
            Repository.Object,
            Logger.Object,
            ScheduleService.Object,
            AircraftRepository.Object,
            FlightOnCreationService.Object);

        var result = await service.GetAsync(1);
        var data = result;

        data.Succeeded.Should().Be(true);
        data.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAsync_ReturnsNull()
    {
        Repository.Setup(x => x.FirstOrDefaultAsync(
            It.IsAny<FlightSpec>(), CancellationToken.None))
            .ReturnsAsync((Flight)null);

        var service = new FlightService(
            Repository.Object,
            Logger.Object,
            ScheduleService.Object,
            AircraftRepository.Object,
            FlightOnCreationService.Object);

        var result = await service.GetAsync(-1);
        var data = result;

        data.Succeeded.Should().Be(false);
        data.Data.Id.Should().BeNull();
    }
}