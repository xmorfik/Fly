using Fly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.Data.Configuration;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.DepartureAirport).WithMany(x => x.FlightsOut);
        builder.HasOne(x => x.ArrivalAirport).WithMany(x => x.FlightsIn);
        builder.HasOne(x => x.Aircraft).WithMany(x => x.Flights);
        builder.HasMany(x => x.Tickets).WithOne(x => x.Flight);
    }
}
