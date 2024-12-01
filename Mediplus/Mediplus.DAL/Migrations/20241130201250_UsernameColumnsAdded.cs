using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mediplus.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UsernameColumnsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Doctors");
        }
    }
}
