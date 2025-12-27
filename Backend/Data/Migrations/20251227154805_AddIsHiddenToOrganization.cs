using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationSystemBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsHiddenToOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Teachers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Organizations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Organizations");
        }
    }
}
