using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL_ASP_5.Migrations
{
    public partial class off : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakeStroke_DriverLicenses_DriverLicenseId",
                table: "TakeStroke");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakeStroke",
                table: "TakeStroke");

            migrationBuilder.RenameTable(
                name: "TakeStroke",
                newName: "TakeStrokes");

            migrationBuilder.RenameIndex(
                name: "IX_TakeStroke_DriverLicenseId",
                table: "TakeStrokes",
                newName: "IX_TakeStrokes_DriverLicenseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakeStrokes",
                table: "TakeStrokes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TakeStrokes_DriverLicenses_DriverLicenseId",
                table: "TakeStrokes",
                column: "DriverLicenseId",
                principalTable: "DriverLicenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakeStrokes_DriverLicenses_DriverLicenseId",
                table: "TakeStrokes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakeStrokes",
                table: "TakeStrokes");

            migrationBuilder.RenameTable(
                name: "TakeStrokes",
                newName: "TakeStroke");

            migrationBuilder.RenameIndex(
                name: "IX_TakeStrokes_DriverLicenseId",
                table: "TakeStroke",
                newName: "IX_TakeStroke_DriverLicenseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakeStroke",
                table: "TakeStroke",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TakeStroke_DriverLicenses_DriverLicenseId",
                table: "TakeStroke",
                column: "DriverLicenseId",
                principalTable: "DriverLicenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
