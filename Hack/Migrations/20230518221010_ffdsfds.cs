using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hack.Migrations
{
    /// <inheritdoc />
    public partial class ffdsfds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCoinIT",
                table: "UserCryptoStates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCoinIT",
                table: "UserCryptoStates");
        }
    }
}
