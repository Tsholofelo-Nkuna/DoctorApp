using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorManagement.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDoctorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DataSource_TitleId",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "TitleId",
                table: "Doctors",
                newName: "DataSourceTitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_TitleId",
                table: "Doctors",
                newName: "IX_Doctors_DataSourceTitleId");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DataSource_DataSourceTitleId",
                table: "Doctors",
                column: "DataSourceTitleId",
                principalTable: "DataSource",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DataSource_DataSourceTitleId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "DataSourceTitleId",
                table: "Doctors",
                newName: "TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_DataSourceTitleId",
                table: "Doctors",
                newName: "IX_Doctors_TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DataSource_TitleId",
                table: "Doctors",
                column: "TitleId",
                principalTable: "DataSource",
                principalColumn: "Id");
        }
    }
}
