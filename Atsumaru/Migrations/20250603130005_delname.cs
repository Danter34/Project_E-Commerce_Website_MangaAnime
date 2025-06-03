using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atsumaru.Migrations
{
    /// <inheritdoc />
    public partial class delname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Deliverys");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Deliverys",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Deliverys",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Deliverys",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
