using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorManagement.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddDataSourceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TitleId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DataSourceEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    TypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSourceEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_TitleId",
                table: "Doctors",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSourceEntity_TypeCode",
                table: "DataSourceEntity",
                column: "TypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DataSourceEntity_TitleId",
                table: "Doctors",
                column: "TitleId",
                principalTable: "DataSourceEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DataSourceEntity_TitleId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "DataSourceEntity");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_TitleId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "TitleId",
                table: "Doctors");
        }
    }
}
