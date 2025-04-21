using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorManagement.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserInfoColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_CreatedById",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_LastModifiedById",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_AspNetUsers_CreatedById",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_AspNetUsers_LastModifiedById",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AspNetUsers_CreatedById",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AspNetUsers_LastModifiedById",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_CreatedById",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_LastModifiedById",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_CreatedById",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_LastModifiedById",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_CreatedById",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_LastModifiedById",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_CreatedById",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_LastModifiedById",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CreatedById",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_LastModifiedById",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByUserId",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CreatedById",
                table: "Patients",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_LastModifiedById",
                table: "Patients",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CreatedById",
                table: "Doctors",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_LastModifiedById",
                table: "Doctors",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CreatedById",
                table: "Contacts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_LastModifiedById",
                table: "Contacts",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CreatedById",
                table: "Addresses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_LastModifiedById",
                table: "Addresses",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_CreatedById",
                table: "Addresses",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_LastModifiedById",
                table: "Addresses",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_AspNetUsers_CreatedById",
                table: "Contacts",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_AspNetUsers_LastModifiedById",
                table: "Contacts",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AspNetUsers_CreatedById",
                table: "Doctors",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AspNetUsers_LastModifiedById",
                table: "Doctors",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_CreatedById",
                table: "Patients",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_LastModifiedById",
                table: "Patients",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
