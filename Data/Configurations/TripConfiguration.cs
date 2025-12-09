using MetroClaim.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetroClaim.Api.Data.Configurations;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("trips");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(255);
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(1000);
        builder.Property(x => x.Destination).HasColumnName("destination").HasMaxLength(255);
        
        builder.Property(x => x.StartDate).HasColumnName("start_date").HasColumnType("date");
        builder.Property(x => x.EndDate).HasColumnName("end_date").HasColumnType("date");
        
        builder.Property(x => x.Cost).HasColumnName("cost").HasPrecision(18, 2);
        
        builder.Property(x => x.TripStatus)
               .HasColumnName("status")
               .HasConversion<string>()
               .HasMaxLength(50);

        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasOne(t => t.User)
               .WithMany(t => t.Trips)
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}