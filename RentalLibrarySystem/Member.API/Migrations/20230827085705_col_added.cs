using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Member.API.Migrations
{
    /// <inheritdoc />
    public partial class coladded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MemberNo",
                table: "MemberDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberNo",
                table: "MemberDetails");
        }
    }
}
