using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrowing.API.Migrations
{
    /// <inheritdoc />
    public partial class MemberNameBookNameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookName",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MemberName",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookName",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "MemberName",
                table: "Requests");
        }
    }
}
