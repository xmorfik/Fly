using Fly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.Data.Configuration;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Aircraft).WithMany(x => x.Seats);
        builder.HasMany(x => x.Tickets).WithOne(x => x.Seat);
    }
}