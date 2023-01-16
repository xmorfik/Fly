using Fly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Fly.Data;

public class FlyDbContext : DbContext
{
    public DbSet<AircraftLocation> AircraftLocations { get; set; }

    public DbSet<Aircraft> Aircrafts { get; set; }

    public DbSet<Airline> Airlines { get; set; }

    public DbSet<Airport> Airports { get; set; }

    public DbSet<Flight> Flights { get; set; }

    public DbSet<Seat> Seats { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<Passenger> Clients { get; set; }


    public FlyDbContext(DbContextOptions<FlyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}