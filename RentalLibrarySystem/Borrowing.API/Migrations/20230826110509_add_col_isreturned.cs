using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrowing.API.Migrations
{
    /// <inheritdoc />
    public partial class addcolisreturned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "Borrows",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "Borrows");
        }
    }
}
