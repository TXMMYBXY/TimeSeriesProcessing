using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeSeriesProcessing.Domain.Models;

namespace TimeSeriesProcessing.Infrastructure.Data.Configuration;

public class MeasurementValueConfiguration : IEntityTypeConfiguration<MeasurementValue>
{
    public void Configure(EntityTypeBuilder<MeasurementValue> builder)
    {
        builder.ToTable("Values");
        
        builder.HasKey(v => v.Id);
        
        builder.Property(v => v.Date)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(v => v.ExecutionTime)
            .IsRequired()
            .HasColumnType("integer");
        
        builder.Property(v => v.Value)
            .IsRequired()
            .HasColumnType("double precision");

        builder
            .HasOne(v => v.Result)
            .WithMany(v => v.Values)
            .HasForeignKey(v => v.ResultId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}