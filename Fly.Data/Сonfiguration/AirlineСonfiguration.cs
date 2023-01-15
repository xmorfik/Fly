using Fly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.Data.—onfiguration;

public class Airline—onfiguration : IEntityTypeConfiguration<Airline>
{
    public void Configure(EntityTypeBuilder<Airline> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Aircrafts).WithOne(x => x.Airline);
    }
}