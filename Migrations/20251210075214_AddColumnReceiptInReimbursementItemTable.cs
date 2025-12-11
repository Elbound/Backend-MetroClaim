using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroClaim.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnReceiptInReimbursementItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "receipt",
                table: "reimbursement_items",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "receipt",
                table: "reimbursement_items");
        }
    }
}
