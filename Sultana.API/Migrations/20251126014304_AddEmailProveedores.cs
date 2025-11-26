using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sultana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailProveedores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Proveedores",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Proveedores");
        }
    }
}
