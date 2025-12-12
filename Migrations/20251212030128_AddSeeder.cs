using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MetroClaim.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "created_at", "limit", "name", "period", "updated_at" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2022), 0m, "Trip", "Monthly", new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2023) },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2027), 5000000m, "Hotel", "Monthly", new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2028) },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2030), 2000000m, "Transportation", "Monthly", new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2030) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1770), "Admin", new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1770) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1772), "Manager", new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1773) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1774), "Finance", new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1775) },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1826), "Employee", new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1827) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "bank_account_number", "created_at", "due_reimbursement", "employee_id", "full_name", "manager_id", "salary", "updated_at" },
                values: new object[,]
                {
                    { new Guid("a1111111-1111-1111-1111-111111111111"), "1234567890", new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2070), 0m, "ADMIN001", "Admin User", null, 10000000m, new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2071) },
                    { new Guid("d1000000-0000-0000-0000-000000000000"), "1234567890", new DateTime(2025, 12, 12, 3, 1, 22, 794, DateTimeKind.Utc).AddTicks(9624), 0m, "MGR001", "Manager One", null, 10000000m, new DateTime(2025, 12, 12, 3, 1, 22, 794, DateTimeKind.Utc).AddTicks(9625) },
                    { new Guid("d2000000-0000-0000-0000-000000000000"), "1234567890", new DateTime(2025, 12, 12, 3, 1, 25, 264, DateTimeKind.Utc).AddTicks(1864), 0m, "MGR002", "Manager Two", null, 10000000m, new DateTime(2025, 12, 12, 3, 1, 25, 264, DateTimeKind.Utc).AddTicks(1865) },
                    { new Guid("f1111111-1111-1111-1111-111111111111"), "1234567890", new DateTime(2025, 12, 12, 3, 1, 22, 187, DateTimeKind.Utc).AddTicks(5858), 0m, "FIN001", "Finance User", null, 10000000m, new DateTime(2025, 12, 12, 3, 1, 22, 187, DateTimeKind.Utc).AddTicks(5860) }
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "id", "created_at", "email", "expired", "is_active", "is_used", "otp", "password", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("a11111ee-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 3, 1, 22, 187, DateTimeKind.Utc).AddTicks(3851), "admin@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$J9/S0xp87S8D62fyEeqYtuyiLPGtbvpucBTtD0ui6S4hboqKs3N/6", new DateTime(2025, 12, 12, 3, 1, 22, 187, DateTimeKind.Utc).AddTicks(3857), new Guid("a1111111-1111-1111-1111-111111111111") },
                    { new Guid("d10000ff-0000-0000-0000-000000000000"), new DateTime(2025, 12, 12, 3, 1, 23, 386, DateTimeKind.Utc).AddTicks(2685), "manager1@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$wT480s7iWmVBwQeHWlfy/O3xQXwAHpNaEAnjWHszupvdSiJLU1VAS", new DateTime(2025, 12, 12, 3, 1, 23, 386, DateTimeKind.Utc).AddTicks(2686), new Guid("d1000000-0000-0000-0000-000000000000") },
                    { new Guid("d20000ff-0000-0000-0000-000000000000"), new DateTime(2025, 12, 12, 3, 1, 25, 890, DateTimeKind.Utc).AddTicks(5506), "manager2@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$kfwkMnZ2IgP1nFQo/LzNTOtcA6n5w2AH0jM/YQwshBMkB67E0ulha", new DateTime(2025, 12, 12, 3, 1, 25, 890, DateTimeKind.Utc).AddTicks(5507), new Guid("d2000000-0000-0000-0000-000000000000") },
                    { new Guid("f11111ee-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 3, 1, 22, 794, DateTimeKind.Utc).AddTicks(9501), "finance@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$2k65vtwF7/1k3TR/DwmYMe6XPRy3FZ/KS16MY8ozymfqQemTpGWq.", new DateTime(2025, 12, 12, 3, 1, 22, 794, DateTimeKind.Utc).AddTicks(9502), new Guid("f1111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "created_at", "role_id", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("a11111bb-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 3, 1, 22, 187, DateTimeKind.Utc).AddTicks(5404), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 3, 1, 22, 187, DateTimeKind.Utc).AddTicks(5405), new Guid("a1111111-1111-1111-1111-111111111111") },
                    { new Guid("d10000aa-0000-0000-0000-000000000000"), new DateTime(2025, 12, 12, 3, 1, 23, 386, DateTimeKind.Utc).AddTicks(2754), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 12, 3, 1, 23, 386, DateTimeKind.Utc).AddTicks(2755), new Guid("d1000000-0000-0000-0000-000000000000") },
                    { new Guid("d20000aa-0000-0000-0000-000000000000"), new DateTime(2025, 12, 12, 3, 1, 25, 890, DateTimeKind.Utc).AddTicks(5585), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 12, 3, 1, 25, 890, DateTimeKind.Utc).AddTicks(5586), new Guid("d2000000-0000-0000-0000-000000000000") },
                    { new Guid("f11111bb-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 3, 1, 22, 794, DateTimeKind.Utc).AddTicks(9568), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 12, 3, 1, 22, 794, DateTimeKind.Utc).AddTicks(9571), new Guid("f1111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "bank_account_number", "created_at", "due_reimbursement", "employee_id", "full_name", "manager_id", "salary", "updated_at" },
                values: new object[,]
                {
                    { new Guid("e1000000-0000-0000-0000-000000000001"), "1234567890", new DateTime(2025, 12, 12, 3, 1, 23, 386, DateTimeKind.Utc).AddTicks(2942), 0m, "EMP101", "Employee 1 (M1)", new Guid("d1000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 3, 1, 23, 386, DateTimeKind.Utc).AddTicks(2942) },
                    { new Guid("e1000000-0000-0000-0000-000000000002"), "1234567890", new DateTime(2025, 12, 12, 3, 1, 23, 973, DateTimeKind.Utc).AddTicks(3504), 0m, "EMP102", "Employee 2 (M1)", new Guid("d1000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 3, 1, 23, 973, DateTimeKind.Utc).AddTicks(3505) },
                    { new Guid("e1000000-0000-0000-0000-000000000003"), "1234567890", new DateTime(2025, 12, 12, 3, 1, 24, 583, DateTimeKind.Utc).AddTicks(5823), 0m, "EMP103", "Employee 3 (M1)", new Guid("d1000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 3, 1, 24, 583, DateTimeKind.Utc).AddTicks(5823) },
                    { new Guid("e2000000-0000-0000-0000-000000000001"), "1234567890", new DateTime(2025, 12, 12, 3, 1, 25, 890, DateTimeKind.Utc).AddTicks(5915), 0m, "EMP201", "Employee 1 (M2)", new Guid("d2000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 3, 1, 25, 890, DateTimeKind.Utc).AddTicks(5915) },
                    { new Guid("e2000000-0000-0000-0000-000000000002"), "1234567890", new DateTime(2025, 12, 12, 3, 1, 26, 517, DateTimeKind.Utc).AddTicks(8172), 0m, "EMP202", "Employee 2 (M2)", new Guid("d2000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 3, 1, 26, 517, DateTimeKind.Utc).AddTicks(8172) },
                    { new Guid("e2000000-0000-0000-0000-000000000003"), "1234567890", new DateTime(2025, 12, 12, 3, 1, 27, 102, DateTimeKind.Utc).AddTicks(1795), 0m, "EMP203", "Employee 3 (M2)", new Guid("d2000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 3, 1, 27, 102, DateTimeKind.Utc).AddTicks(1796) }
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "id", "created_at", "email", "expired", "is_active", "is_used", "otp", "password", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("e10000ff-0000-0000-0000-000000000001"), new DateTime(2025, 12, 12, 3, 1, 23, 973, DateTimeKind.Utc).AddTicks(2669), "emp1.m1@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$QSl8IHpknxaJ2qjneFjgoOJchyvDFk5NTpSLeIr4UkBLrX8wQTtHm", new DateTime(2025, 12, 12, 3, 1, 23, 973, DateTimeKind.Utc).AddTicks(2670), new Guid("e1000000-0000-0000-0000-000000000001") },
                    { new Guid("e10000ff-0000-0000-0000-000000000002"), new DateTime(2025, 12, 12, 3, 1, 24, 583, DateTimeKind.Utc).AddTicks(4389), "emp2.m1@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$AkF/jHqVwkJ42iWnaHlhBudo782Vqd1CbtjooXBc6k6gHWBcdH6M2", new DateTime(2025, 12, 12, 3, 1, 24, 583, DateTimeKind.Utc).AddTicks(4389), new Guid("e1000000-0000-0000-0000-000000000002") },
                    { new Guid("e10000ff-0000-0000-0000-000000000003"), new DateTime(2025, 12, 12, 3, 1, 25, 264, DateTimeKind.Utc).AddTicks(1440), "emp3.m1@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$lID4XD6M7LbxtgSNLN9s2e/VX7k44ZLh1g.XQjlitXm26H/91nVu.", new DateTime(2025, 12, 12, 3, 1, 25, 264, DateTimeKind.Utc).AddTicks(1440), new Guid("e1000000-0000-0000-0000-000000000003") },
                    { new Guid("e20000ff-0000-0000-0000-000000000001"), new DateTime(2025, 12, 12, 3, 1, 26, 517, DateTimeKind.Utc).AddTicks(7915), "emp1.m2@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$A4sFbMdkjxrhzE0CrbZAQ.sZ4HnO6J7ZwEkgPhlR5OM2hpYDI0PJi", new DateTime(2025, 12, 12, 3, 1, 26, 517, DateTimeKind.Utc).AddTicks(7915), new Guid("e2000000-0000-0000-0000-000000000001") },
                    { new Guid("e20000ff-0000-0000-0000-000000000002"), new DateTime(2025, 12, 12, 3, 1, 27, 102, DateTimeKind.Utc).AddTicks(1536), "emp2.m2@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$MjuQC18tn.KSjRpwFSado.V.qGC/giUqI5pH6kiioBXBw2Pttu/K6", new DateTime(2025, 12, 12, 3, 1, 27, 102, DateTimeKind.Utc).AddTicks(1536), new Guid("e2000000-0000-0000-0000-000000000002") },
                    { new Guid("e20000ff-0000-0000-0000-000000000003"), new DateTime(2025, 12, 12, 3, 1, 27, 716, DateTimeKind.Utc).AddTicks(6672), "emp3.m2@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$5HMsmgMSAi6yWKnWFe2dXebuCubUNBHCI4XdpDk1CcCSo/zh1Y.Mm", new DateTime(2025, 12, 12, 3, 1, 27, 716, DateTimeKind.Utc).AddTicks(6673), new Guid("e2000000-0000-0000-0000-000000000003") }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "created_at", "role_id", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("e10000aa-0000-0000-0000-000000000001"), new DateTime(2025, 12, 12, 3, 1, 23, 973, DateTimeKind.Utc).AddTicks(2780), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 3, 1, 23, 973, DateTimeKind.Utc).AddTicks(2782), new Guid("e1000000-0000-0000-0000-000000000001") },
                    { new Guid("e10000aa-0000-0000-0000-000000000002"), new DateTime(2025, 12, 12, 3, 1, 24, 583, DateTimeKind.Utc).AddTicks(4531), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 3, 1, 24, 583, DateTimeKind.Utc).AddTicks(4532), new Guid("e1000000-0000-0000-0000-000000000002") },
                    { new Guid("e10000aa-0000-0000-0000-000000000003"), new DateTime(2025, 12, 12, 3, 1, 25, 264, DateTimeKind.Utc).AddTicks(1587), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 3, 1, 25, 264, DateTimeKind.Utc).AddTicks(1587), new Guid("e1000000-0000-0000-0000-000000000003") },
                    { new Guid("e20000aa-0000-0000-0000-000000000001"), new DateTime(2025, 12, 12, 3, 1, 26, 517, DateTimeKind.Utc).AddTicks(7986), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 3, 1, 26, 517, DateTimeKind.Utc).AddTicks(7987), new Guid("e2000000-0000-0000-0000-000000000001") },
                    { new Guid("e20000aa-0000-0000-0000-000000000002"), new DateTime(2025, 12, 12, 3, 1, 27, 102, DateTimeKind.Utc).AddTicks(1615), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 3, 1, 27, 102, DateTimeKind.Utc).AddTicks(1615), new Guid("e2000000-0000-0000-0000-000000000002") },
                    { new Guid("e20000aa-0000-0000-0000-000000000003"), new DateTime(2025, 12, 12, 3, 1, 27, 716, DateTimeKind.Utc).AddTicks(6741), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 3, 1, 27, 716, DateTimeKind.Utc).AddTicks(6742), new Guid("e2000000-0000-0000-0000-000000000003") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
