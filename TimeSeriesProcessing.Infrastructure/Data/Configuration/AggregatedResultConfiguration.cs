using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeSeriesProcessing.Domain.Models;

namespace TimeSeriesProcessing.Infrastructure.Data.Configuration;

public class AggregatedResultConfiguration : IEntityTypeConfiguration<AggregatedResult>
{
    public void Configure(EntityTypeBuilder<AggregatedResult> builder)
    {
        builder.ToTable("Results");
        
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.FileName)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(r => r.DeltaDate)
            .IsRequired()
            .HasColumnType("integer");
        
        builder.Property(r => r.MinDate)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(r => r.AvgExecutionTime)
            .IsRequired()
            .HasColumnType("timestamp with time zone");
        
        builder.Property(r => r.AvgValue)
            .IsRequired()
            .HasColumnType("double precision");
        
        builder.Property(r => r.MedianValue)
            .IsRequired()
            .HasColumnType("double precision");
        
        builder.Property(r => r.MinValue)
            .IsRequired()
            .HasColumnType("double precision");
        
        builder.Property(r => r.MaxValue)
            .IsRequired()
            .HasColumnType("double precision");
    }
}