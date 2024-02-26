using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changenamefield2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcessType",
                schema: "Workflow",
                table: "BulckComponent",
                newName: "InputId");

            migrationBuilder.RenameColumn(
                name: "PlacementPreference",
                schema: "Workflow",
                table: "BulckComponent",
                newName: "IsHidden");

            migrationBuilder.RenameColumn(
                name: "ComponentOfReference",
                schema: "Workflow",
                table: "BulckComponent",
                newName: "Required");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Required",
                schema: "Workflow",
                table: "BulckComponent",
                newName: "ComponentOfReference");

            migrationBuilder.RenameColumn(
                name: "IsHidden",
                schema: "Workflow",
                table: "BulckComponent",
                newName: "PlacementPreference");

            migrationBuilder.RenameColumn(
                name: "InputId",
                schema: "Workflow",
                table: "BulckComponent",
                newName: "ProcessType");
        }
    }
}
