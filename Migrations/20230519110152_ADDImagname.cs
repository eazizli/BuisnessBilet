using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eBusiness.Migrations
{
    public partial class ADDImagname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Teams");
        }
    }
}
