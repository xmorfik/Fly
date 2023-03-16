using Fly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.Data.Configuration;

public class AirlineConfiguration : IEntityTypeConfiguration<Airline>
{
    public void Configure(EntityTypeBuilder<Airline> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Aircrafts).WithOne(x => x.Airline);
        builder.HasMany(x => x.Managers).WithOne(x => x.Airline);
    }
}