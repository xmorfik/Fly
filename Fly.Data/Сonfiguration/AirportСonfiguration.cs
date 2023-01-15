using Fly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.Data.—onfiguration;

public class Airport—onfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Aircrafts).WithOne(x => x.Airport);
        builder.HasMany(x => x.FlightsIn).WithOne(x => x.ArrivalAirport);
        builder.HasMany(x => x.FlightsOut).WithOne(x => x.DepartureAirport);
    }
}