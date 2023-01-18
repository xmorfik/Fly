using Fly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.Data.Сonfiguration;

public class ClientConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Tickets).WithOne(x => x.Passenger);
    }
}
