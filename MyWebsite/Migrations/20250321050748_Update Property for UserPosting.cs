using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebsite.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertyforUserPosting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "UserPosting",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Major",
                table: "UserPosting");
        }
    }
}
