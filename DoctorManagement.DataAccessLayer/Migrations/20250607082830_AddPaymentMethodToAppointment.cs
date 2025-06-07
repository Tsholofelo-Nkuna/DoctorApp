using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorManagement.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentMethodToAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PaymentMethodId",
                table: "Appointments",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_DataSource_PaymentMethodId",
                table: "Appointments",
                column: "PaymentMethodId",
                principalTable: "DataSource",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_DataSource_PaymentMethodId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PaymentMethodId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Appointments");
        }
    }
}
