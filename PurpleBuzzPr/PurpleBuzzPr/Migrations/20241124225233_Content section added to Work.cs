using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurpleBuzzPr.Migrations
{
    /// <inheritdoc />
    public partial class ContentsectionaddedtoWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Works",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Works");
        }
    }
}
