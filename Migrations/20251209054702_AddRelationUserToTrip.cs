using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroClaim.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationUserToTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "trips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_trips_user_id",
                table: "trips",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_trips_users_user_id",
                table: "trips",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trips_users_user_id",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_user_id",
                table: "trips");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "trips");
        }
    }
}
