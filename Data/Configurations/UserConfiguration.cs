using MetroClaim.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetroClaim.Api.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("id");
        builder.Property(u => u.EmployeeId).HasColumnName("employee_id").HasMaxLength(255);
        builder.Property(u => u.FullName).HasColumnName("full_name").HasMaxLength(255);
        builder.Property(u => u.Salary).HasColumnName("salary").HasPrecision(18, 2);
        builder.Property(u => u.DueReimbursement).HasColumnName("due_reimbursement").HasPrecision(18, 2);
        builder.Property(u => u.BankAccountNumber).HasColumnName("bank_account_number").HasMaxLength(255);
        builder.Property(x => x.ManagerId).HasColumnName("manager_id");
        builder.Property(u => u.CreatedAt).HasColumnName("created_at");
        builder.Property(u => u.UpdatedAt).HasColumnName("updated_at");

        builder.HasOne(u => u.Manager)
               .WithMany(u => u.Subordinates)
               .HasForeignKey(u => u.ManagerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Account)
               .WithOne(a => a.User)
               .HasForeignKey<Account>(a => a.UserId);
    }
}