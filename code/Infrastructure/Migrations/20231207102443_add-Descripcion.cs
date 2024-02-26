using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addDescripcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Workflow",
                table: "BulckComponent",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                schema: "Workflow",
                table: "BulckComponent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
