using Fly.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Fly.Data;

public class FlyDbContext : IdentityDbContext
{
    public DbSet<AircraftLocation> AircraftLocations { get; set; }

    public DbSet<Aircraft> Aircrafts { get; set; }

    public DbSet<Airline> Airlines { get; set; }

    public DbSet<Airport> Airports { get; set; }

    public DbSet<Flight> Flights { get; set; }

    public DbSet<Seat> Seats { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<Passenger> Clients { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public DbSet<Message> Messages { get; set; }


    public FlyDbContext(DbContextOptions<FlyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}