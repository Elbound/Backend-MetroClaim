using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroClaim.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("a11111ee-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 21, 965, DateTimeKind.Utc).AddTicks(6191), "$2a$13$Xl9BFLBQpbP.dGwUYaVG8e0L6ZWQladp28w5BW82HX5fFUW7pnsSG", new DateTime(2025, 12, 12, 16, 39, 21, 965, DateTimeKind.Utc).AddTicks(6192) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("d10000ff-0000-0000-0000-000000000000"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 23, 202, DateTimeKind.Utc).AddTicks(4632), "$2a$13$/.P8LgtxsOQcDtd1WF/3huYseDftdQEmSALI.gUxCmhxQ2fFQaQNS", new DateTime(2025, 12, 12, 16, 39, 23, 202, DateTimeKind.Utc).AddTicks(4632) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("d20000ff-0000-0000-0000-000000000000"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 25, 777, DateTimeKind.Utc).AddTicks(919), "$2a$13$RTuCzlhh2F05W0a445pDBOKa0ho36WnjB0X9tZ26Z1inePowLY1Je", new DateTime(2025, 12, 12, 16, 39, 25, 777, DateTimeKind.Utc).AddTicks(919) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e10000ff-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 23, 947, DateTimeKind.Utc).AddTicks(182), "$2a$13$akLAmgzm5w0ktTGyxV3VTOUIJ8vU4bug4qC1vVYwkNwDV2tjVxLbK", new DateTime(2025, 12, 12, 16, 39, 23, 947, DateTimeKind.Utc).AddTicks(183) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e10000ff-0000-0000-0000-000000000002"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 24, 526, DateTimeKind.Utc).AddTicks(4490), "$2a$13$cQmhUFtQjRDuZFc9ld8EAuX/gBOmmB4Mz7lt5GGOhpShU3W21y.om", new DateTime(2025, 12, 12, 16, 39, 24, 526, DateTimeKind.Utc).AddTicks(4491) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e10000ff-0000-0000-0000-000000000003"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 25, 184, DateTimeKind.Utc).AddTicks(7088), "$2a$13$w0W96BTZVZMMO5wcFF9dduVNv3yaHu60vbZ397cGikIm4onsd2xKq", new DateTime(2025, 12, 12, 16, 39, 25, 184, DateTimeKind.Utc).AddTicks(7088) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e20000ff-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 26, 435, DateTimeKind.Utc).AddTicks(226), "$2a$13$Y0L28Ls7eM5D5CHUzpHEG.ApIYPILOay6FPktankorj93g2YqeA2i", new DateTime(2025, 12, 12, 16, 39, 26, 435, DateTimeKind.Utc).AddTicks(226) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e20000ff-0000-0000-0000-000000000002"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 27, 243, DateTimeKind.Utc).AddTicks(1974), "$2a$13$1EpMeugCbXK9mdBhmW/jw.liYlmaY3E4NY2e9ho/xPtjNLTdjHwBK", new DateTime(2025, 12, 12, 16, 39, 27, 243, DateTimeKind.Utc).AddTicks(1974) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e20000ff-0000-0000-0000-000000000003"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 27, 906, DateTimeKind.Utc).AddTicks(5407), "$2a$13$gWyFinTbpXxP0IrHdJEYS.QZ1yEwNaHZ9jam4HW/xvJQh1oH311AG", new DateTime(2025, 12, 12, 16, 39, 27, 906, DateTimeKind.Utc).AddTicks(5408) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("f11111ee-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 22, 551, DateTimeKind.Utc).AddTicks(7354), "$2a$13$8AoVzJkQoCuxXw3lE8Ku/ObI3jfcX0/ZpQ3wBdz5L1HiL9z0bxZYG", new DateTime(2025, 12, 12, 16, 39, 22, 551, DateTimeKind.Utc).AddTicks(7354) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5113), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5114) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5119), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5120) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5122), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5122) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4866), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4867) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4872), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4872) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4874), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(4874) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("a11111bb-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 21, 965, DateTimeKind.Utc).AddTicks(6704), new DateTime(2025, 12, 12, 16, 39, 21, 965, DateTimeKind.Utc).AddTicks(6705) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("d10000aa-0000-0000-0000-000000000000"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 23, 202, DateTimeKind.Utc).AddTicks(4703), new DateTime(2025, 12, 12, 16, 39, 23, 202, DateTimeKind.Utc).AddTicks(4703) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("d20000aa-0000-0000-0000-000000000000"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 25, 777, DateTimeKind.Utc).AddTicks(1016), new DateTime(2025, 12, 12, 16, 39, 25, 777, DateTimeKind.Utc).AddTicks(1017) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e10000aa-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 23, 947, DateTimeKind.Utc).AddTicks(301), new DateTime(2025, 12, 12, 16, 39, 23, 947, DateTimeKind.Utc).AddTicks(301) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e10000aa-0000-0000-0000-000000000002"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 24, 526, DateTimeKind.Utc).AddTicks(4647), new DateTime(2025, 12, 12, 16, 39, 24, 526, DateTimeKind.Utc).AddTicks(4648) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e10000aa-0000-0000-0000-000000000003"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 25, 184, DateTimeKind.Utc).AddTicks(7722), new DateTime(2025, 12, 12, 16, 39, 25, 184, DateTimeKind.Utc).AddTicks(7722) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e20000aa-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 26, 435, DateTimeKind.Utc).AddTicks(306), new DateTime(2025, 12, 12, 16, 39, 26, 435, DateTimeKind.Utc).AddTicks(307) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e20000aa-0000-0000-0000-000000000002"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 27, 243, DateTimeKind.Utc).AddTicks(2251), new DateTime(2025, 12, 12, 16, 39, 27, 243, DateTimeKind.Utc).AddTicks(2252) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e20000aa-0000-0000-0000-000000000003"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 27, 906, DateTimeKind.Utc).AddTicks(5487), new DateTime(2025, 12, 12, 16, 39, 27, 906, DateTimeKind.Utc).AddTicks(5488) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("f11111bb-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 22, 551, DateTimeKind.Utc).AddTicks(7426), new DateTime(2025, 12, 12, 16, 39, 22, 551, DateTimeKind.Utc).AddTicks(7426) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a1111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5166), new DateTime(2025, 12, 12, 16, 39, 21, 186, DateTimeKind.Utc).AddTicks(5167) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("d1000000-0000-0000-0000-000000000000"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 22, 551, DateTimeKind.Utc).AddTicks(7491), new DateTime(2025, 12, 12, 16, 39, 22, 551, DateTimeKind.Utc).AddTicks(7492) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("d2000000-0000-0000-0000-000000000000"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 25, 184, DateTimeKind.Utc).AddTicks(7873), new DateTime(2025, 12, 12, 16, 39, 25, 184, DateTimeKind.Utc).AddTicks(7875) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e1000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 23, 202, DateTimeKind.Utc).AddTicks(4872), new DateTime(2025, 12, 12, 16, 39, 23, 202, DateTimeKind.Utc).AddTicks(4872) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e1000000-0000-0000-0000-000000000002"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 23, 947, DateTimeKind.Utc).AddTicks(653), new DateTime(2025, 12, 12, 16, 39, 23, 947, DateTimeKind.Utc).AddTicks(654) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e1000000-0000-0000-0000-000000000003"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 24, 526, DateTimeKind.Utc).AddTicks(6039), new DateTime(2025, 12, 12, 16, 39, 24, 526, DateTimeKind.Utc).AddTicks(6040) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e2000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 25, 777, DateTimeKind.Utc).AddTicks(1471), new DateTime(2025, 12, 12, 16, 39, 25, 777, DateTimeKind.Utc).AddTicks(1473) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e2000000-0000-0000-0000-000000000002"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 26, 435, DateTimeKind.Utc).AddTicks(423), new DateTime(2025, 12, 12, 16, 39, 26, 435, DateTimeKind.Utc).AddTicks(423) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e2000000-0000-0000-0000-000000000003"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 27, 243, DateTimeKind.Utc).AddTicks(2391), new DateTime(2025, 12, 12, 16, 39, 27, 243, DateTimeKind.Utc).AddTicks(2391) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 39, 21, 965, DateTimeKind.Utc).AddTicks(6746), new DateTime(2025, 12, 12, 16, 39, 21, 965, DateTimeKind.Utc).AddTicks(6746) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("a11111ee-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 22, 187, DateTimeKind.Utc).AddTicks(3851), "$2a$13$J9/S0xp87S8D62fyEeqYtuyiLPGtbvpucBTtD0ui6S4hboqKs3N/6", new DateTime(2025, 12, 12, 3, 1, 22, 187, DateTimeKind.Utc).AddTicks(3857) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("d10000ff-0000-0000-0000-000000000000"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 23, 386, DateTimeKind.Utc).AddTicks(2685), "$2a$13$wT480s7iWmVBwQeHWlfy/O3xQXwAHpNaEAnjWHszupvdSiJLU1VAS", new DateTime(2025, 12, 12, 3, 1, 23, 386, DateTimeKind.Utc).AddTicks(2686) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("d20000ff-0000-0000-0000-000000000000"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 25, 890, DateTimeKind.Utc).AddTicks(5506), "$2a$13$kfwkMnZ2IgP1nFQo/LzNTOtcA6n5w2AH0jM/YQwshBMkB67E0ulha", new DateTime(2025, 12, 12, 3, 1, 25, 890, DateTimeKind.Utc).AddTicks(5507) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e10000ff-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 23, 973, DateTimeKind.Utc).AddTicks(2669), "$2a$13$QSl8IHpknxaJ2qjneFjgoOJchyvDFk5NTpSLeIr4UkBLrX8wQTtHm", new DateTime(2025, 12, 12, 3, 1, 23, 973, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e10000ff-0000-0000-0000-000000000002"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 24, 583, DateTimeKind.Utc).AddTicks(4389), "$2a$13$AkF/jHqVwkJ42iWnaHlhBudo782Vqd1CbtjooXBc6k6gHWBcdH6M2", new DateTime(2025, 12, 12, 3, 1, 24, 583, DateTimeKind.Utc).AddTicks(4389) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e10000ff-0000-0000-0000-000000000003"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 25, 264, DateTimeKind.Utc).AddTicks(1440), "$2a$13$lID4XD6M7LbxtgSNLN9s2e/VX7k44ZLh1g.XQjlitXm26H/91nVu.", new DateTime(2025, 12, 12, 3, 1, 25, 264, DateTimeKind.Utc).AddTicks(1440) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e20000ff-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 26, 517, DateTimeKind.Utc).AddTicks(7915), "$2a$13$A4sFbMdkjxrhzE0CrbZAQ.sZ4HnO6J7ZwEkgPhlR5OM2hpYDI0PJi", new DateTime(2025, 12, 12, 3, 1, 26, 517, DateTimeKind.Utc).AddTicks(7915) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e20000ff-0000-0000-0000-000000000002"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 27, 102, DateTimeKind.Utc).AddTicks(1536), "$2a$13$MjuQC18tn.KSjRpwFSado.V.qGC/giUqI5pH6kiioBXBw2Pttu/K6", new DateTime(2025, 12, 12, 3, 1, 27, 102, DateTimeKind.Utc).AddTicks(1536) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("e20000ff-0000-0000-0000-000000000003"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 27, 716, DateTimeKind.Utc).AddTicks(6672), "$2a$13$5HMsmgMSAi6yWKnWFe2dXebuCubUNBHCI4XdpDk1CcCSo/zh1Y.Mm", new DateTime(2025, 12, 12, 3, 1, 27, 716, DateTimeKind.Utc).AddTicks(6673) });

            migrationBuilder.UpdateData(
                table: "accounts",
                keyColumn: "id",
                keyValue: new Guid("f11111ee-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 22, 794, DateTimeKind.Utc).AddTicks(9501), "$2a$13$2k65vtwF7/1k3TR/DwmYMe6XPRy3FZ/KS16MY8ozymfqQemTpGWq.", new DateTime(2025, 12, 12, 3, 1, 22, 794, DateTimeKind.Utc).AddTicks(9502) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2022), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2023) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2027), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2028) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2030), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2030) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1770), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1772), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1773) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1774), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1775) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1826), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(1827) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("a11111bb-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 22, 187, DateTimeKind.Utc).AddTicks(5404), new DateTime(2025, 12, 12, 3, 1, 22, 187, DateTimeKind.Utc).AddTicks(5405) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("d10000aa-0000-0000-0000-000000000000"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 23, 386, DateTimeKind.Utc).AddTicks(2754), new DateTime(2025, 12, 12, 3, 1, 23, 386, DateTimeKind.Utc).AddTicks(2755) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("d20000aa-0000-0000-0000-000000000000"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 25, 890, DateTimeKind.Utc).AddTicks(5585), new DateTime(2025, 12, 12, 3, 1, 25, 890, DateTimeKind.Utc).AddTicks(5586) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e10000aa-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 23, 973, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 12, 12, 3, 1, 23, 973, DateTimeKind.Utc).AddTicks(2782) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e10000aa-0000-0000-0000-000000000002"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 24, 583, DateTimeKind.Utc).AddTicks(4531), new DateTime(2025, 12, 12, 3, 1, 24, 583, DateTimeKind.Utc).AddTicks(4532) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e10000aa-0000-0000-0000-000000000003"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 25, 264, DateTimeKind.Utc).AddTicks(1587), new DateTime(2025, 12, 12, 3, 1, 25, 264, DateTimeKind.Utc).AddTicks(1587) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e20000aa-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 26, 517, DateTimeKind.Utc).AddTicks(7986), new DateTime(2025, 12, 12, 3, 1, 26, 517, DateTimeKind.Utc).AddTicks(7987) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e20000aa-0000-0000-0000-000000000002"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 27, 102, DateTimeKind.Utc).AddTicks(1615), new DateTime(2025, 12, 12, 3, 1, 27, 102, DateTimeKind.Utc).AddTicks(1615) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("e20000aa-0000-0000-0000-000000000003"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 27, 716, DateTimeKind.Utc).AddTicks(6741), new DateTime(2025, 12, 12, 3, 1, 27, 716, DateTimeKind.Utc).AddTicks(6742) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("f11111bb-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 22, 794, DateTimeKind.Utc).AddTicks(9568), new DateTime(2025, 12, 12, 3, 1, 22, 794, DateTimeKind.Utc).AddTicks(9571) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a1111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2070), new DateTime(2025, 12, 12, 3, 1, 21, 508, DateTimeKind.Utc).AddTicks(2071) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("d1000000-0000-0000-0000-000000000000"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 22, 794, DateTimeKind.Utc).AddTicks(9624), new DateTime(2025, 12, 12, 3, 1, 22, 794, DateTimeKind.Utc).AddTicks(9625) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("d2000000-0000-0000-0000-000000000000"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 25, 264, DateTimeKind.Utc).AddTicks(1864), new DateTime(2025, 12, 12, 3, 1, 25, 264, DateTimeKind.Utc).AddTicks(1865) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e1000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 23, 386, DateTimeKind.Utc).AddTicks(2942), new DateTime(2025, 12, 12, 3, 1, 23, 386, DateTimeKind.Utc).AddTicks(2942) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e1000000-0000-0000-0000-000000000002"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 23, 973, DateTimeKind.Utc).AddTicks(3504), new DateTime(2025, 12, 12, 3, 1, 23, 973, DateTimeKind.Utc).AddTicks(3505) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e1000000-0000-0000-0000-000000000003"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 24, 583, DateTimeKind.Utc).AddTicks(5823), new DateTime(2025, 12, 12, 3, 1, 24, 583, DateTimeKind.Utc).AddTicks(5823) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e2000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 25, 890, DateTimeKind.Utc).AddTicks(5915), new DateTime(2025, 12, 12, 3, 1, 25, 890, DateTimeKind.Utc).AddTicks(5915) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e2000000-0000-0000-0000-000000000002"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 26, 517, DateTimeKind.Utc).AddTicks(8172), new DateTime(2025, 12, 12, 3, 1, 26, 517, DateTimeKind.Utc).AddTicks(8172) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e2000000-0000-0000-0000-000000000003"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 27, 102, DateTimeKind.Utc).AddTicks(1795), new DateTime(2025, 12, 12, 3, 1, 27, 102, DateTimeKind.Utc).AddTicks(1796) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 3, 1, 22, 187, DateTimeKind.Utc).AddTicks(5858), new DateTime(2025, 12, 12, 3, 1, 22, 187, DateTimeKind.Utc).AddTicks(5860) });
        }
    }
}
