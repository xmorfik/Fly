using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Fly.Data;

public class FlyDbContext : DbContext
{
    public FlyDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}