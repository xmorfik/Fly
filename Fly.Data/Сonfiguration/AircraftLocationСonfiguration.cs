using Fly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.Data.Сonfiguration;

public class AircraftLocationCongiguration : IEntityTypeConfiguration<AircraftLocation>
{
    public void Configure(EntityTypeBuilder<AircraftLocation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Aircraft).WithMany(x => x.AircraftLocations);
		builder.HasOne(x => x.Flight).WithMany(x => x.AircraftLocations);
	}
}