using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLEventSphere.Migrations
{
    /// <inheritdoc />
    public partial class FixNamingConsistency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvaliableSeats",
                table: "Events",
                newName: "AvailableSeats");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "Categories",
                newName: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvailableSeats",
                table: "Events",
                newName: "AvaliableSeats");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "categoryId");
        }
    }
}
