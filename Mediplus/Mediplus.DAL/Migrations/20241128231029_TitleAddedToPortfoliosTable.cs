using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mediplus.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TitleAddedToPortfoliosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Portfolios");
        }
    }
}
