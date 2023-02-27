using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEshop.DataLayer.Migrations
{
    public partial class mig_DeleteTheIsProductActiveFieldFromBoxes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProductActive",
                table: "Boxes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProductActive",
                table: "Boxes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
