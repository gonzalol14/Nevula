using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NevulaForo.Migrations
{
    /// <inheritdoc />
    public partial class AddIsBannedColumnToPublication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_banned",
                table: "Publication",
                type: "bit",
                nullable: true,
                defaultValue: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_banned",
                table: "Publication");
        }
    }
}
