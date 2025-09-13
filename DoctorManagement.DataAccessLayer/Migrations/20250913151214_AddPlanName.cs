using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorManagement.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicalAidPlanName",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicalAidPlanName",
                table: "Patients");
        }
    }
}
