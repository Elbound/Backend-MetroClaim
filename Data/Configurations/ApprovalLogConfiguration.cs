using MetroClaim.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetroClaim.Api.Data.Configurations;

public class ApprovalLogConfiguration : IEntityTypeConfiguration<ApprovalLog>
{
    public void Configure(EntityTypeBuilder<ApprovalLog> builder)
    {
        builder.ToTable("approval_logs");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ReimbursementId).HasColumnName("reimbursement_id");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.ApprovalLogStatus).HasColumnName("action").HasConversion<string>().HasMaxLength(50);
        builder.Property(x => x.Comment).HasColumnName("comments");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasOne(a => a.Reimbursement)
            .WithMany(r => r.ApprovalLogs)
            .HasForeignKey(a => a.ReimbursementId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.User)
            .WithMany(u => u.ApprovalLogs)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}