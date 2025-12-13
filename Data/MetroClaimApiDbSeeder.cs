using MetroClaim.Api.Models;
using Microsoft.EntityFrameworkCore;
using MetroClaim.Api.Utilities;

namespace MetroClaim.Api.Data;

public static class MetroClaimApiDbSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // 1. Roles
        var roleAdminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var roleManagerId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var roleFinanceId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var roleEmployeeId = Guid.Parse("44444444-4444-4444-4444-444444444444");

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = roleAdminId, Name = "Admin", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Role { Id = roleManagerId, Name = "Manager", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Role { Id = roleFinanceId, Name = "Finance", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Role { Id = roleEmployeeId, Name = "Employee", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );

        // 2. Categories
        var catTripId = Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA");
        var catHotelId = Guid.Parse("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB");
        var catTransportId = Guid.Parse("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC");

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = catTripId, Name = "Trip", Limit = 0, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }, // Limit Gede
            new Category { Id = catHotelId, Name = "Hotel", Limit = 5000000, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { Id = catTransportId, Name = "Transportation", Limit = 2000000, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );

        // 3. Admin & Finance
        var adminId = Guid.Parse("A1111111-1111-1111-1111-111111111111");
        var financeId = Guid.Parse("F1111111-1111-1111-1111-111111111111");

        SeedUser(modelBuilder, adminId, "Admin User", "admin@metroclaim.com", "Admin123", "ADMIN001", null, roleAdminId);
        SeedUser(modelBuilder, financeId, "Finance User", "finance@metroclaim.com", "Finance123", "FIN001", null, roleFinanceId);

        // 4. Managers & Subordinates
        var manager1Id = Guid.Parse("D1000000-0000-0000-0000-000000000000");
        SeedUser(modelBuilder, manager1Id, "Manager One", "manager1@metroclaim.com", "Manager123", "MGR001", null, roleManagerId);

        for (int i = 1; i <= 3; i++)
        {
            var empId = Guid.Parse($"E1000000-0000-0000-0000-00000000000{i}");
            SeedUser(modelBuilder, empId, $"Employee {i} (M1)", $"emp{i}.m1@metroclaim.com", "Employee123", $"EMP10{i}", manager1Id, roleEmployeeId);
        }

        var manager2Id = Guid.Parse("D2000000-0000-0000-0000-000000000000");
        SeedUser(modelBuilder, manager2Id, "Manager Two", "manager2@metroclaim.com", "Manager123", "MGR002", null, roleManagerId);

        for (int i = 1; i <= 3; i++)
        {
            var empId = Guid.Parse($"E2000000-0000-0000-0000-00000000000{i}");
            SeedUser(modelBuilder, empId, $"Employee {i} (M2)", $"emp{i}.m2@metroclaim.com", "Employee123", $"EMP20{i}", manager2Id, roleEmployeeId);
        }
    }

    private static void SeedUser(ModelBuilder modelBuilder, Guid userId, string name, string email, string password, string uniqueId, Guid? managerId, Guid roleId)
    {
        // User
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = userId,
            FullName = name,
            EmployeeId = uniqueId,
            Salary = 10000000,
            DueReimbursement = 0,
            ManagerId = managerId,
            BankAccountNumber = "1234567890",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // Account
        // Note: In real world validation/seeding hash usually requires service. 
        // We will instantiate HashHandler locally since it has no dependencies.
        var hasher = new HashHandler();
        var hash = hasher.GenerateHash(password);

        modelBuilder.Entity<Account>().HasData(new Account
        {
            // Simple trick: Flip the first byte of User Id
            Id = XORGuid(userId, 0xFF), 
            UserId = userId,
            Email = email,
            Password = hash,
            IsActive = true,
            IsUsed = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // UserRole
        modelBuilder.Entity<UserRole>().HasData(new UserRole
        {
            Id = XORGuid(userId, 0xAA),
            UserId = userId,
            RoleId = roleId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
    }

    private static Guid XORGuid(Guid original, byte b)
    {
        var bytes = original.ToByteArray();
        bytes[0] = (byte)(bytes[0] ^ b);
        return new Guid(bytes);
    }
}
