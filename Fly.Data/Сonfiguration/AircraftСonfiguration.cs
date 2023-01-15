using Fly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.Data.Ñonfiguration;

public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
{
    public void Configure(EntityTypeBuilder<Aircraft> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Airline).WithMany(x => x.Aircrafts).IsRequired(false);
        builder.HasOne(x => x.Airport).WithMany(x => x.Aircrafts).IsRequired(false);
        builder.HasMany(x => x.AircraftLocations).WithOne(x => x.Aircraft);
        builder.HasMany(x => x.Seats).WithOne(x => x.Aircraft);
        builder.HasMany(x => x.Flights).WithOne(x => x.Aircraft);
    }
}