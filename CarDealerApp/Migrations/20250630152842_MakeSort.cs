﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDealerApp.Migrations
{
    /// <inheritdoc />
    public partial class MakeSort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Makes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Makes");
        }
    }
}
