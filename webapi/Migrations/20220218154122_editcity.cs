using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class editcity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ticket_CityId",
                table: "Ticket");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CityId",
                table: "Ticket",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ticket_CityId",
                table: "Ticket");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CityId",
                table: "Ticket",
                column: "CityId",
                unique: true);
        }
    }
}
