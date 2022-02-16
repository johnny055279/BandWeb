using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class editTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "City",
                newName: "CityName");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CityId",
                table: "Ticket",
                column: "CityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_City_CityId",
                table: "Ticket",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_City_CityId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_CityId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "City",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
