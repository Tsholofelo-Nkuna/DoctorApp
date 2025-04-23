using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorManagement.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddAdressForiegnKeyToDoctorEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PracticeSiteId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PracticeSiteId",
                table: "Doctors",
                column: "PracticeSiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Addresses_PracticeSiteId",
                table: "Doctors",
                column: "PracticeSiteId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Addresses_PracticeSiteId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_PracticeSiteId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "PracticeSiteId",
                table: "Doctors");
        }
    }
}
