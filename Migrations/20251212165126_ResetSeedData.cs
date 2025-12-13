using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MetroClaim.Api.Migrations
{
    /// <inheritdoc />
    public partial class ResetSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("a11111ee-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("d10000ff-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("d20000ff-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e10000ff-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e10000ff-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e10000ff-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e20000ff-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e20000ff-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e20000ff-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("f11111ee-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("a11111bb-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("d10000aa-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("d20000aa-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e10000aa-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e10000aa-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e10000aa-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e20000aa-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e20000aa-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e20000aa-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("f11111bb-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a1111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e1000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e1000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e1000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e2000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e2000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e2000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("d1000000-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("d2000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "created_at", "limit", "name", "period", "updated_at" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5113), 0m, "Trip", "Monthly", new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5114) },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5119), 5000000m, "Hotel", "Monthly", new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5120) },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5122), 2000000m, "Transportation", "Monthly", new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5122) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4866), "Admin", new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4867) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4870), "Manager", new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4870) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4872), "Finance", new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4872) },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4874), "Employee", new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4874) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "bank_account_number", "created_at", "due_reimbursement", "employee_id", "full_name", "manager_id", "salary", "updated_at" },
                values: new object[,]
                {
                    { new Guid("a1111111-1111-1111-1111-111111111111"), "1234567890", new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5166), 0m, "ADMIN001", "Admin User", null, 10000000m, new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5167) },
                    { new Guid("d1000000-0000-0000-0000-000000000000"), "1234567890", new DateTime(2025, 12, 12, 16, 39, 22, 551, DateTimeKind.Utc).AddTicks(7491), 0m, "MGR001", "Manager One", null, 10000000m, new DateTime(2025, 12, 12, 16, 39, 22, 551, DateTimeKind.Utc).AddTicks(7492) },
                    { new Guid("d2000000-0000-0000-0000-000000000000"), "1234567890", new DateTime(2025, 12, 12, 16, 39, 25, 184, DateTimeKind.Utc).AddTicks(7873), 0m, "MGR002", "Manager Two", null, 10000000m, new DateTime(2025, 12, 12, 16, 39, 25, 184, DateTimeKind.Utc).AddTicks(7875) },
                    { new Guid("f1111111-1111-1111-1111-111111111111"), "1234567890", new DateTime(2025, 12, 12, 16, 39, 21, 965, DateTimeKind.Utc).AddTicks(6746), 0m, "FIN001", "Finance User", null, 10000000m, new DateTime(2025, 12, 12, 16, 39, 21, 965, DateTimeKind.Utc).AddTicks(6746) }
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "id", "created_at", "email", "expired", "is_active", "is_used", "otp", "password", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("a11111ee-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 16, 39, 21, 965, DateTimeKind.Utc).AddTicks(6191), "admin@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$Xl9BFLBQpbP.dGwUYaVG8e0L6ZWQladp28w5BW82HX5fFUW7pnsSG", new DateTime(2025, 12, 12, 16, 39, 21, 965, DateTimeKind.Utc).AddTicks(6192), new Guid("a1111111-1111-1111-1111-111111111111") },
                    { new Guid("d10000ff-0000-0000-0000-000000000000"), new DateTime(2025, 12, 12, 16, 39, 23, 202, DateTimeKind.Utc).AddTicks(4632), "manager1@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$/.P8LgtxsOQcDtd1WF/3huYseDftdQEmSALI.gUxCmhxQ2fFQaQNS", new DateTime(2025, 12, 12, 16, 39, 23, 202, DateTimeKind.Utc).AddTicks(4632), new Guid("d1000000-0000-0000-0000-000000000000") },
                    { new Guid("d20000ff-0000-0000-0000-000000000000"), new DateTime(2025, 12, 12, 16, 39, 25, 777, DateTimeKind.Utc).AddTicks(919), "manager2@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$RTuCzlhh2F05W0a445pDBOKa0ho36WnjB0X9tZ26Z1inePowLY1Je", new DateTime(2025, 12, 12, 16, 39, 25, 777, DateTimeKind.Utc).AddTicks(919), new Guid("d2000000-0000-0000-0000-000000000000") },
                    { new Guid("f11111ee-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 16, 39, 22, 551, DateTimeKind.Utc).AddTicks(7354), "finance@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$8AoVzJkQoCuxXw3lE8Ku/ObI3jfcX0/ZpQ3wBdz5L1HiL9z0bxZYG", new DateTime(2025, 12, 12, 16, 39, 22, 551, DateTimeKind.Utc).AddTicks(7354), new Guid("f1111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "created_at", "role_id", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("a11111bb-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 16, 39, 21, 965, DateTimeKind.Utc).AddTicks(6704), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 16, 39, 21, 965, DateTimeKind.Utc).AddTicks(6705), new Guid("a1111111-1111-1111-1111-111111111111") },
                    { new Guid("d10000aa-0000-0000-0000-000000000000"), new DateTime(2025, 12, 12, 16, 39, 23, 202, DateTimeKind.Utc).AddTicks(4703), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 12, 16, 39, 23, 202, DateTimeKind.Utc).AddTicks(4703), new Guid("d1000000-0000-0000-0000-000000000000") },
                    { new Guid("d20000aa-0000-0000-0000-000000000000"), new DateTime(2025, 12, 12, 16, 39, 25, 777, DateTimeKind.Utc).AddTicks(1016), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 12, 16, 39, 25, 777, DateTimeKind.Utc).AddTicks(1017), new Guid("d2000000-0000-0000-0000-000000000000") },
                    { new Guid("f11111bb-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 16, 39, 22, 551, DateTimeKind.Utc).AddTicks(7426), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 12, 16, 39, 22, 551, DateTimeKind.Utc).AddTicks(7426), new Guid("f1111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "bank_account_number", "created_at", "due_reimbursement", "employee_id", "full_name", "manager_id", "salary", "updated_at" },
                values: new object[,]
                {
                    { new Guid("e1000000-0000-0000-0000-000000000001"), "1234567890", new DateTime(2025, 12, 12, 16, 39, 23, 202, DateTimeKind.Utc).AddTicks(4872), 0m, "EMP101", "Employee 1 (M1)", new Guid("d1000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 16, 39, 23, 202, DateTimeKind.Utc).AddTicks(4872) },
                    { new Guid("e1000000-0000-0000-0000-000000000002"), "1234567890", new DateTime(2025, 12, 12, 16, 39, 23, 947, DateTimeKind.Utc).AddTicks(653), 0m, "EMP102", "Employee 2 (M1)", new Guid("d1000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 16, 39, 23, 947, DateTimeKind.Utc).AddTicks(654) },
                    { new Guid("e1000000-0000-0000-0000-000000000003"), "1234567890", new DateTime(2025, 12, 12, 16, 39, 24, 526, DateTimeKind.Utc).AddTicks(6039), 0m, "EMP103", "Employee 3 (M1)", new Guid("d1000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 16, 39, 24, 526, DateTimeKind.Utc).AddTicks(6040) },
                    { new Guid("e2000000-0000-0000-0000-000000000001"), "1234567890", new DateTime(2025, 12, 12, 16, 39, 25, 777, DateTimeKind.Utc).AddTicks(1471), 0m, "EMP201", "Employee 1 (M2)", new Guid("d2000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 16, 39, 25, 777, DateTimeKind.Utc).AddTicks(1473) },
                    { new Guid("e2000000-0000-0000-0000-000000000002"), "1234567890", new DateTime(2025, 12, 12, 16, 39, 26, 435, DateTimeKind.Utc).AddTicks(423), 0m, "EMP202", "Employee 2 (M2)", new Guid("d2000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 16, 39, 26, 435, DateTimeKind.Utc).AddTicks(423) },
                    { new Guid("e2000000-0000-0000-0000-000000000003"), "1234567890", new DateTime(2025, 12, 12, 16, 39, 27, 243, DateTimeKind.Utc).AddTicks(2391), 0m, "EMP203", "Employee 3 (M2)", new Guid("d2000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 16, 39, 27, 243, DateTimeKind.Utc).AddTicks(2391) }
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "id", "created_at", "email", "expired", "is_active", "is_used", "otp", "password", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("e10000ff-0000-0000-0000-000000000001"), new DateTime(2025, 12, 12, 16, 39, 23, 947, DateTimeKind.Utc).AddTicks(182), "emp1.m1@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$akLAmgzm5w0ktTGyxV3VTOUIJ8vU4bug4qC1vVYwkNwDV2tjVxLbK", new DateTime(2025, 12, 12, 16, 39, 23, 947, DateTimeKind.Utc).AddTicks(183), new Guid("e1000000-0000-0000-0000-000000000001") },
                    { new Guid("e10000ff-0000-0000-0000-000000000002"), new DateTime(2025, 12, 12, 16, 39, 24, 526, DateTimeKind.Utc).AddTicks(4490), "emp2.m1@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$cQmhUFtQjRDuZFc9ld8EAuX/gBOmmB4Mz7lt5GGOhpShU3W21y.om", new DateTime(2025, 12, 12, 16, 39, 24, 526, DateTimeKind.Utc).AddTicks(4491), new Guid("e1000000-0000-0000-0000-000000000002") },
                    { new Guid("e10000ff-0000-0000-0000-000000000003"), new DateTime(2025, 12, 12, 16, 39, 25, 184, DateTimeKind.Utc).AddTicks(7088), "emp3.m1@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$w0W96BTZVZMMO5wcFF9dduVNv3yaHu60vbZ397cGikIm4onsd2xKq", new DateTime(2025, 12, 12, 16, 39, 25, 184, DateTimeKind.Utc).AddTicks(7088), new Guid("e1000000-0000-0000-0000-000000000003") },
                    { new Guid("e20000ff-0000-0000-0000-000000000001"), new DateTime(2025, 12, 12, 16, 39, 26, 435, DateTimeKind.Utc).AddTicks(226), "emp1.m2@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$Y0L28Ls7eM5D5CHUzpHEG.ApIYPILOay6FPktankorj93g2YqeA2i", new DateTime(2025, 12, 12, 16, 39, 26, 435, DateTimeKind.Utc).AddTicks(226), new Guid("e2000000-0000-0000-0000-000000000001") },
                    { new Guid("e20000ff-0000-0000-0000-000000000002"), new DateTime(2025, 12, 12, 16, 39, 27, 243, DateTimeKind.Utc).AddTicks(1974), "emp2.m2@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$1EpMeugCbXK9mdBhmW/jw.liYlmaY3E4NY2e9ho/xPtjNLTdjHwBK", new DateTime(2025, 12, 12, 16, 39, 27, 243, DateTimeKind.Utc).AddTicks(1974), new Guid("e2000000-0000-0000-0000-000000000002") },
                    { new Guid("e20000ff-0000-0000-0000-000000000003"), new DateTime(2025, 12, 12, 16, 39, 27, 906, DateTimeKind.Utc).AddTicks(5407), "emp3.m2@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$gWyFinTbpXxP0IrHdJEYS.QZ1yEwNaHZ9jam4HW/xvJQh1oH311AG", new DateTime(2025, 12, 12, 16, 39, 27, 906, DateTimeKind.Utc).AddTicks(5408), new Guid("e2000000-0000-0000-0000-000000000003") }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "created_at", "role_id", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("e10000aa-0000-0000-0000-000000000001"), new DateTime(2025, 12, 12, 16, 39, 23, 947, DateTimeKind.Utc).AddTicks(301), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 39, 23, 947, DateTimeKind.Utc).AddTicks(301), new Guid("e1000000-0000-0000-0000-000000000001") },
                    { new Guid("e10000aa-0000-0000-0000-000000000002"), new DateTime(2025, 12, 12, 16, 39, 24, 526, DateTimeKind.Utc).AddTicks(4647), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 39, 24, 526, DateTimeKind.Utc).AddTicks(4648), new Guid("e1000000-0000-0000-0000-000000000002") },
                    { new Guid("e10000aa-0000-0000-0000-000000000003"), new DateTime(2025, 12, 12, 16, 39, 25, 184, DateTimeKind.Utc).AddTicks(7722), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 39, 25, 184, DateTimeKind.Utc).AddTicks(7722), new Guid("e1000000-0000-0000-0000-000000000003") },
                    { new Guid("e20000aa-0000-0000-0000-000000000001"), new DateTime(2025, 12, 12, 16, 39, 26, 435, DateTimeKind.Utc).AddTicks(306), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 39, 26, 435, DateTimeKind.Utc).AddTicks(307), new Guid("e2000000-0000-0000-0000-000000000001") },
                    { new Guid("e20000aa-0000-0000-0000-000000000002"), new DateTime(2025, 12, 12, 16, 39, 27, 243, DateTimeKind.Utc).AddTicks(2251), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 39, 27, 243, DateTimeKind.Utc).AddTicks(2252), new Guid("e2000000-0000-0000-0000-000000000002") },
                    { new Guid("e20000aa-0000-0000-0000-000000000003"), new DateTime(2025, 12, 12, 16, 39, 27, 906, DateTimeKind.Utc).AddTicks(5487), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 39, 27, 906, DateTimeKind.Utc).AddTicks(5488), new Guid("e2000000-0000-0000-0000-000000000003") }
                });
        }
    }
}
