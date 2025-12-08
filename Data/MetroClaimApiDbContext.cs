using MetroClaim.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MetroClaim.Api.Data;

public class MetroClaimApiDbContext : DbContext
{
    public MetroClaimApiDbContext(DbContextOptions<MetroClaimApiDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<UserLimit> UserLimits { get; set; }
    public DbSet<Reimbursement> Reimbursements { get; set; }
    public DbSet<ReimbursementItem> ReimbursementItems { get; set; }
    public DbSet<ApprovalLog> ApprovalLogs { get; set; }
    public DbSet<Trip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelbuilder)
    {
        modelbuilder.ApplyConfigurationsFromAssembly(typeof(MetroClaimApiDbContext).Assembly);

        /*
        SEED
        */
    }
}
