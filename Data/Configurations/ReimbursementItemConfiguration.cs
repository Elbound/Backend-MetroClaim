using MetroClaim.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetroClaim.Api.Data.Configurations;

public class ReimbursementItemConfiguration : IEntityTypeConfiguration<ReimbursementItem>
{
    public void Configure(EntityTypeBuilder<ReimbursementItem> builder)
    {
        builder.ToTable("reimbursement_items");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ReimbursementId).HasColumnName("reimbursement_id");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 2);
        
        builder.Property(x => x.DateOfExpense).HasColumnName("date_of_expenses").HasColumnType("date");
        builder.Property(x => x.Receipt).HasColumnName("receipt");
        
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
    }
}