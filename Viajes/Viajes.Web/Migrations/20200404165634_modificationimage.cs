using Microsoft.EntityFrameworkCore.Migrations;

namespace Viajes.Web.Migrations
{
    public partial class modificationimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ReceiptPath",
                table: "TripDetails",
                newName: "PicturePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PicturePath",
                table: "TripDetails",
                newName: "ReceiptPath");

            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
