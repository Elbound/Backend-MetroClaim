using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MetroClaim.Api.Migrations
{
    /// <inheritdoc />
    public partial class ApplySeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "created_at", "limit", "name", "period", "updated_at" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2676), 0m, "Trip", "Monthly", new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2678) },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2685), 5000000m, "Hotel", "Monthly", new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2685) },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2689), 2000000m, "Transportation", "Monthly", new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2689) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2124), "Admin", new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2125) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2129), "Manager", new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2130) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2132), "Finance", new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2132) },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2135), "Employee", new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2135) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "bank_account_number", "created_at", "due_reimbursement", "employee_id", "full_name", "manager_id", "salary", "updated_at" },
                values: new object[,]
                {
                    { new Guid("a1111111-1111-1111-1111-111111111111"), "1234567890", new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2749), 0m, "ADMIN001", "Admin User", null, 10000000m, new DateTime(2025, 12, 12, 16, 52, 22, 747, DateTimeKind.Utc).AddTicks(2750) },
                    { new Guid("d1000000-0000-0000-0000-000000000000"), "1234567890", new DateTime(2025, 12, 12, 16, 52, 24, 501, DateTimeKind.Utc).AddTicks(2226), 0m, "MGR001", "Manager One", null, 10000000m, new DateTime(2025, 12, 12, 16, 52, 24, 501, DateTimeKind.Utc).AddTicks(2227) },
                    { new Guid("d2000000-0000-0000-0000-000000000000"), "1234567890", new DateTime(2025, 12, 12, 16, 52, 27, 993, DateTimeKind.Utc).AddTicks(3017), 0m, "MGR002", "Manager Two", null, 10000000m, new DateTime(2025, 12, 12, 16, 52, 27, 993, DateTimeKind.Utc).AddTicks(3018) },
                    { new Guid("f1111111-1111-1111-1111-111111111111"), "1234567890", new DateTime(2025, 12, 12, 16, 52, 23, 673, DateTimeKind.Utc).AddTicks(400), 0m, "FIN001", "Finance User", null, 10000000m, new DateTime(2025, 12, 12, 16, 52, 23, 673, DateTimeKind.Utc).AddTicks(401) }
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "id", "created_at", "email", "expired", "is_active", "is_used", "otp", "password", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("a11111ee-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 16, 52, 23, 672, DateTimeKind.Utc).AddTicks(9340), "admin@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$n65b8ftGIBxYtKSVCX3bzunn5RbYJ4NgHZ3pbh0MW9/XzX3te6sMq", new DateTime(2025, 12, 12, 16, 52, 23, 672, DateTimeKind.Utc).AddTicks(9341), new Guid("a1111111-1111-1111-1111-111111111111") },
                    { new Guid("d10000ff-0000-0000-0000-000000000000"), new DateTime(2025, 12, 12, 16, 52, 25, 353, DateTimeKind.Utc).AddTicks(2928), "manager1@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$OpwEvyiqQu5CZymE.VxN7eK9CZdFrnW3UqtBsQ86phDCUAPzeSv3q", new DateTime(2025, 12, 12, 16, 52, 25, 353, DateTimeKind.Utc).AddTicks(2929), new Guid("d1000000-0000-0000-0000-000000000000") },
                    { new Guid("d20000ff-0000-0000-0000-000000000000"), new DateTime(2025, 12, 12, 16, 52, 28, 922, DateTimeKind.Utc).AddTicks(636), "manager2@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$T8nsR5KD7olnmHiuLtvcTuds6222D.5JatQxAwctplJ0bt91TxP6O", new DateTime(2025, 12, 12, 16, 52, 28, 922, DateTimeKind.Utc).AddTicks(637), new Guid("d2000000-0000-0000-0000-000000000000") },
                    { new Guid("f11111ee-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 16, 52, 24, 501, DateTimeKind.Utc).AddTicks(2081), "finance@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$.TwX74OX44k/KIrPGPokgO8nUVUOBU2VGpMzO7IYm5x2.AxDo.R.e", new DateTime(2025, 12, 12, 16, 52, 24, 501, DateTimeKind.Utc).AddTicks(2081), new Guid("f1111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "created_at", "role_id", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("a11111bb-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 16, 52, 23, 673, DateTimeKind.Utc).AddTicks(323), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 16, 52, 23, 673, DateTimeKind.Utc).AddTicks(323), new Guid("a1111111-1111-1111-1111-111111111111") },
                    { new Guid("d10000aa-0000-0000-0000-000000000000"), new DateTime(2025, 12, 12, 16, 52, 25, 353, DateTimeKind.Utc).AddTicks(3028), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 12, 16, 52, 25, 353, DateTimeKind.Utc).AddTicks(3029), new Guid("d1000000-0000-0000-0000-000000000000") },
                    { new Guid("d20000aa-0000-0000-0000-000000000000"), new DateTime(2025, 12, 12, 16, 52, 28, 922, DateTimeKind.Utc).AddTicks(738), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 12, 16, 52, 28, 922, DateTimeKind.Utc).AddTicks(739), new Guid("d2000000-0000-0000-0000-000000000000") },
                    { new Guid("f11111bb-1111-1111-1111-111111111111"), new DateTime(2025, 12, 12, 16, 52, 24, 501, DateTimeKind.Utc).AddTicks(2159), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 12, 16, 52, 24, 501, DateTimeKind.Utc).AddTicks(2159), new Guid("f1111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "bank_account_number", "created_at", "due_reimbursement", "employee_id", "full_name", "manager_id", "salary", "updated_at" },
                values: new object[,]
                {
                    { new Guid("e1000000-0000-0000-0000-000000000001"), "1234567890", new DateTime(2025, 12, 12, 16, 52, 25, 353, DateTimeKind.Utc).AddTicks(3250), 0m, "EMP101", "Employee 1 (M1)", new Guid("d1000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 16, 52, 25, 353, DateTimeKind.Utc).AddTicks(3251) },
                    { new Guid("e1000000-0000-0000-0000-000000000002"), "1234567890", new DateTime(2025, 12, 12, 16, 52, 26, 176, DateTimeKind.Utc).AddTicks(1190), 0m, "EMP102", "Employee 2 (M1)", new Guid("d1000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 16, 52, 26, 176, DateTimeKind.Utc).AddTicks(1190) },
                    { new Guid("e1000000-0000-0000-0000-000000000003"), "1234567890", new DateTime(2025, 12, 12, 16, 52, 27, 89, DateTimeKind.Utc).AddTicks(6148), 0m, "EMP103", "Employee 3 (M1)", new Guid("d1000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 16, 52, 27, 89, DateTimeKind.Utc).AddTicks(6149) },
                    { new Guid("e2000000-0000-0000-0000-000000000001"), "1234567890", new DateTime(2025, 12, 12, 16, 52, 28, 922, DateTimeKind.Utc).AddTicks(1022), 0m, "EMP201", "Employee 1 (M2)", new Guid("d2000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 16, 52, 28, 922, DateTimeKind.Utc).AddTicks(1023) },
                    { new Guid("e2000000-0000-0000-0000-000000000002"), "1234567890", new DateTime(2025, 12, 12, 16, 52, 29, 709, DateTimeKind.Utc).AddTicks(4815), 0m, "EMP202", "Employee 2 (M2)", new Guid("d2000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 16, 52, 29, 709, DateTimeKind.Utc).AddTicks(4816) },
                    { new Guid("e2000000-0000-0000-0000-000000000003"), "1234567890", new DateTime(2025, 12, 12, 16, 52, 30, 537, DateTimeKind.Utc).AddTicks(5791), 0m, "EMP203", "Employee 3 (M2)", new Guid("d2000000-0000-0000-0000-000000000000"), 10000000m, new DateTime(2025, 12, 12, 16, 52, 30, 537, DateTimeKind.Utc).AddTicks(5792) }
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "id", "created_at", "email", "expired", "is_active", "is_used", "otp", "password", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("e10000ff-0000-0000-0000-000000000001"), new DateTime(2025, 12, 12, 16, 52, 26, 176, DateTimeKind.Utc).AddTicks(453), "emp1.m1@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$kWABRKTn9HcYEDPr/w8IWuqPxWbZ8ZqG8PV/gEEej/cy4xMz2LjLe", new DateTime(2025, 12, 12, 16, 52, 26, 176, DateTimeKind.Utc).AddTicks(454), new Guid("e1000000-0000-0000-0000-000000000001") },
                    { new Guid("e10000ff-0000-0000-0000-000000000002"), new DateTime(2025, 12, 12, 16, 52, 27, 89, DateTimeKind.Utc).AddTicks(4087), "emp2.m1@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$oJVTD0j5My0VGv/KCFzM5e.T1stPld1KFYmRRHdf5MHlGnZyW32si", new DateTime(2025, 12, 12, 16, 52, 27, 89, DateTimeKind.Utc).AddTicks(4087), new Guid("e1000000-0000-0000-0000-000000000002") },
                    { new Guid("e10000ff-0000-0000-0000-000000000003"), new DateTime(2025, 12, 12, 16, 52, 27, 993, DateTimeKind.Utc).AddTicks(2528), "emp3.m1@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$4UVy.fs.sqEC5iG5R/hDc.O/NS9ArczpQ3Pnb.cg8dM3aKL6YNyd2", new DateTime(2025, 12, 12, 16, 52, 27, 993, DateTimeKind.Utc).AddTicks(2528), new Guid("e1000000-0000-0000-0000-000000000003") },
                    { new Guid("e20000ff-0000-0000-0000-000000000001"), new DateTime(2025, 12, 12, 16, 52, 29, 709, DateTimeKind.Utc).AddTicks(4605), "emp1.m2@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$MjyOGzW/4vcEnpoH0iqO5e2AjBeSuQDmYFrtKqCodHTUSaxyl0s/y", new DateTime(2025, 12, 12, 16, 52, 29, 709, DateTimeKind.Utc).AddTicks(4606), new Guid("e2000000-0000-0000-0000-000000000001") },
                    { new Guid("e20000ff-0000-0000-0000-000000000002"), new DateTime(2025, 12, 12, 16, 52, 30, 537, DateTimeKind.Utc).AddTicks(5557), "emp2.m2@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$FuoFsgQ8aacBSijckZ7fweX/hra22MWA.XIQObBlV8uHh0WOY6mWK", new DateTime(2025, 12, 12, 16, 52, 30, 537, DateTimeKind.Utc).AddTicks(5558), new Guid("e2000000-0000-0000-0000-000000000002") },
                    { new Guid("e20000ff-0000-0000-0000-000000000003"), new DateTime(2025, 12, 12, 16, 52, 31, 401, DateTimeKind.Utc).AddTicks(4518), "emp3.m2@metroclaim.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "$2a$13$Op7eeL1hDFIF4JjQKejV.ulg5rU0Jds30P1hEC4b/etHGvBpbWuQC", new DateTime(2025, 12, 12, 16, 52, 31, 401, DateTimeKind.Utc).AddTicks(4518), new Guid("e2000000-0000-0000-0000-000000000003") }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "created_at", "role_id", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("e10000aa-0000-0000-0000-000000000001"), new DateTime(2025, 12, 12, 16, 52, 26, 176, DateTimeKind.Utc).AddTicks(542), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 52, 26, 176, DateTimeKind.Utc).AddTicks(542), new Guid("e1000000-0000-0000-0000-000000000001") },
                    { new Guid("e10000aa-0000-0000-0000-000000000002"), new DateTime(2025, 12, 12, 16, 52, 27, 89, DateTimeKind.Utc).AddTicks(4508), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 52, 27, 89, DateTimeKind.Utc).AddTicks(4508), new Guid("e1000000-0000-0000-0000-000000000002") },
                    { new Guid("e10000aa-0000-0000-0000-000000000003"), new DateTime(2025, 12, 12, 16, 52, 27, 993, DateTimeKind.Utc).AddTicks(2724), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 52, 27, 993, DateTimeKind.Utc).AddTicks(2725), new Guid("e1000000-0000-0000-0000-000000000003") },
                    { new Guid("e20000aa-0000-0000-0000-000000000001"), new DateTime(2025, 12, 12, 16, 52, 29, 709, DateTimeKind.Utc).AddTicks(4688), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 52, 29, 709, DateTimeKind.Utc).AddTicks(4688), new Guid("e2000000-0000-0000-0000-000000000001") },
                    { new Guid("e20000aa-0000-0000-0000-000000000002"), new DateTime(2025, 12, 12, 16, 52, 30, 537, DateTimeKind.Utc).AddTicks(5659), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 52, 30, 537, DateTimeKind.Utc).AddTicks(5660), new Guid("e2000000-0000-0000-0000-000000000002") },
                    { new Guid("e20000aa-0000-0000-0000-000000000003"), new DateTime(2025, 12, 12, 16, 52, 31, 401, DateTimeKind.Utc).AddTicks(4613), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 12, 16, 52, 31, 401, DateTimeKind.Utc).AddTicks(4613), new Guid("e2000000-0000-0000-0000-000000000003") }
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
