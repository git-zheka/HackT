using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hack.Migrations
{
    /// <inheritdoc />
    public partial class _1ddad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "HistoryEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "HistoryEvents");
        }
    }
}
