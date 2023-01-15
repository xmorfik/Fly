using Fly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.Data.—onfiguration;

public class Ticket—onfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Client).WithMany(x => x.Tickets).IsRequired(false);
        builder.HasOne(x => x.Flight).WithMany(x => x.Tickets);
        builder.HasOne(x => x.Seat).WithMany(x => x.Tickets);
    }
}
