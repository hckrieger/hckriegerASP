using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hckrieger.Migrations
{
    public partial class GivePostVisibilityBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Posts");
        }
    }
}
