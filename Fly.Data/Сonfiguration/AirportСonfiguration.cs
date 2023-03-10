using Fly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.Data.Configuration;

public class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.City).WithMany(x => x.Airports);
        builder.HasMany(x => x.Aircrafts).WithOne(x => x.Airport);
        builder.HasMany(x => x.FlightsIn).WithOne(x => x.ArrivalAirport);
        builder.HasMany(x => x.FlightsOut).WithOne(x => x.DepartureAirport);
    }
}