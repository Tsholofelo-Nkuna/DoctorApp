using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorManagement.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddAcceptHomeVisitsToDoctorEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptHomeVisits",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptHomeVisits",
                table: "Doctors");
        }
    }
}
