using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebsite.Migrations
{
    /// <inheritdoc />
    public partial class Updatev2PropertyforUserPosting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarPath",
                table: "UserPosting");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "UserPosting",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "UserPosting");

            migrationBuilder.AddColumn<string>(
                name: "AvatarPath",
                table: "UserPosting",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
