using Ardalis.Specification;
using FluentAssertions;
using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Services;
using Fly.Tests.Helpers;
using Moq;

namespace Fly.Tests.Api.Controllers;

public class FlightsServiceTests
{
    public readonly Mock<IRepository<Flight>> _repository = new ();
    public readonly List<Flight> _flights;

    public FlightsServiceTests()
    {
        _flights = FlightHelper.Flights;
        _repository.Setup(x => x.ListAsync(It.IsAny<ISpecification<Flight>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_flights);
    }

    [Fact]
    public async Task GetListAsync_NoParameters_ReturnsAll()
    {
        var service = new FlightService(_repository.Object);
        var result = await service.GetListAsync(new FlightParameter(),new Page());
        var data = result.Data;
        data.Count().Should().Be(_flights.Count);
    }
}