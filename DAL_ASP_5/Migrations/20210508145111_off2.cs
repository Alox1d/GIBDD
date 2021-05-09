using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL_ASP_5.Migrations
{
    public partial class off2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverLicenses_CarOwners_CarOwnerId",
                table: "DriverLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarOwners_CarOwnerId",
                table: "Vehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverLicenses_CarOwners_CarOwnerId",
                table: "DriverLicenses",
                column: "CarOwnerId",
                principalTable: "CarOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarOwners_CarOwnerId",
                table: "Vehicles",
                column: "CarOwnerId",
                principalTable: "CarOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverLicenses_CarOwners_CarOwnerId",
                table: "DriverLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarOwners_CarOwnerId",
                table: "Vehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverLicenses_CarOwners_CarOwnerId",
                table: "DriverLicenses",
                column: "CarOwnerId",
                principalTable: "CarOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarOwners_CarOwnerId",
                table: "Vehicles",
                column: "CarOwnerId",
                principalTable: "CarOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
