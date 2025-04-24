using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorManagement.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddDataSourceTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DataSourceEntity_TitleId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataSourceEntity",
                table: "DataSourceEntity");

            migrationBuilder.RenameTable(
                name: "DataSourceEntity",
                newName: "DataSource");

            migrationBuilder.RenameIndex(
                name: "IX_DataSourceEntity_TypeCode",
                table: "DataSource",
                newName: "IX_DataSource_TypeCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataSource",
                table: "DataSource",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DataSource_TitleId",
                table: "Doctors",
                column: "TitleId",
                principalTable: "DataSource",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DataSource_TitleId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataSource",
                table: "DataSource");

            migrationBuilder.RenameTable(
                name: "DataSource",
                newName: "DataSourceEntity");

            migrationBuilder.RenameIndex(
                name: "IX_DataSource_TypeCode",
                table: "DataSourceEntity",
                newName: "IX_DataSourceEntity_TypeCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataSourceEntity",
                table: "DataSourceEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DataSourceEntity_TitleId",
                table: "Doctors",
                column: "TitleId",
                principalTable: "DataSourceEntity",
                principalColumn: "Id");
        }
    }
}
