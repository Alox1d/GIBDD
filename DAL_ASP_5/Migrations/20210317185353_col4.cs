using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL_ASP_5.Migrations
{
    public partial class col4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarOwners_DriverLicenses_DriverLicenseId",
                table: "CarOwners");

            migrationBuilder.DropIndex(
                name: "IX_CarOwners_DriverLicenseId",
                table: "CarOwners");

            migrationBuilder.DropColumn(
                name: "DriverLicenseId",
                table: "CarOwners");

            migrationBuilder.AddColumn<int>(
                name: "CarOwnerId",
                table: "DriverLicenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DriverLicenses_CarOwnerId",
                table: "DriverLicenses",
                column: "CarOwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverLicenses_CarOwners_CarOwnerId",
                table: "DriverLicenses",
                column: "CarOwnerId",
                principalTable: "CarOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverLicenses_CarOwners_CarOwnerId",
                table: "DriverLicenses");

            migrationBuilder.DropIndex(
                name: "IX_DriverLicenses_CarOwnerId",
                table: "DriverLicenses");

            migrationBuilder.DropColumn(
                name: "CarOwnerId",
                table: "DriverLicenses");

            migrationBuilder.AddColumn<int>(
                name: "DriverLicenseId",
                table: "CarOwners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CarOwners_DriverLicenseId",
                table: "CarOwners",
                column: "DriverLicenseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarOwners_DriverLicenses_DriverLicenseId",
                table: "CarOwners",
                column: "DriverLicenseId",
                principalTable: "DriverLicenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
