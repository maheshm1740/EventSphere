using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLEventSphere.Migrations
{
    /// <inheritdoc />
    public partial class btest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisteredAt",
                table: "EventRegistrations",
                newName: "RegisteredOn");

            migrationBuilder.AddColumn<string>(
                name: "RegistrationCode",
                table: "EventRegistrations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationCode",
                table: "EventRegistrations");

            migrationBuilder.RenameColumn(
                name: "RegisteredOn",
                table: "EventRegistrations",
                newName: "RegisteredAt");
        }
    }
}
