using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorManagement.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddAbbrColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abbr",
                table: "DataSource",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbr",
                table: "DataSource");
        }
    }
}
