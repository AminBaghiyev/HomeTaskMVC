using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurpleBuzzPr.Migrations
{
    /// <inheritdoc />
    public partial class Baseclassesadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Works",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Works",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TeamMembers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TeamMembers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ContactMessages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ContactMessages",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ContactMessages");
        }
    }
}
