using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rewardergg.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_CreatedAt_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedAt",
                table: "UserToken",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
