using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEshop.DataLayer.Migrations
{
    public partial class migOfferingStatusInComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProsAndCons",
                table: "Comments_Questions_Answers");

            migrationBuilder.AddColumn<int>(
                name: "OfferingStatus",
                table: "Comments_Questions_Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferingStatus",
                table: "Comments_Questions_Answers");

            migrationBuilder.AddColumn<string>(
                name: "ProsAndCons",
                table: "Comments_Questions_Answers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
