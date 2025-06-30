using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDealerApp.Migrations
{
    /// <inheritdoc />
    public partial class MakeLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Makes",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Makes");
        }
    }
}
