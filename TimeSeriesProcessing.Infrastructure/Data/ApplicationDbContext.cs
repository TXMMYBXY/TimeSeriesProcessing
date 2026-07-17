using Microsoft.EntityFrameworkCore;
using TimeSeriesProcessing.Domain.Models;

namespace TimeSeriesProcessing.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<MeasurementValue> Values { get; set; }
    public DbSet<AggregatedResult> Results { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}