using MetroClaim.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetroClaim.Api.Data.Configurations;

public class UserLimitConfiguration : IEntityTypeConfiguration<UserLimit>
{
    public void Configure(EntityTypeBuilder<UserLimit> builder)
    {
        builder.ToTable("user_limits");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CategoryId).HasColumnName("category_id");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.LimitUsed).HasColumnName("limit_used").HasPrecision(18, 2);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasOne(ul => ul.User)
            .WithMany(u => u.UserLimits)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ul => ul.Category)
            .WithMany(c => c.UserLimits)
            .HasForeignKey(ul => ul.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}