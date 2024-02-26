using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bulkprocess3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentProperty",
                schema: "Workflow");

            migrationBuilder.AddColumn<bool>(
                name: "ComponentOfReference",
                schema: "Workflow",
                table: "BulckComponent",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                schema: "Workflow",
                table: "BulckComponent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                schema: "Workflow",
                table: "BulckComponent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PlacementPreference",
                schema: "Workflow",
                table: "BulckComponent",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProcessType",
                schema: "Workflow",
                table: "BulckComponent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SelectedRuleId",
                schema: "Workflow",
                table: "BulckComponent",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "Workflow",
                table: "BulckComponent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BulckComponent_SelectedRuleId",
                schema: "Workflow",
                table: "BulckComponent",
                column: "SelectedRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BulckComponent_SelectedRule_SelectedRuleId",
                schema: "Workflow",
                table: "BulckComponent",
                column: "SelectedRuleId",
                principalTable: "SelectedRule",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BulckComponent_SelectedRule_SelectedRuleId",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.DropIndex(
                name: "IX_BulckComponent_SelectedRuleId",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.DropColumn(
                name: "ComponentOfReference",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.DropColumn(
                name: "PlacementPreference",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.DropColumn(
                name: "ProcessType",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.DropColumn(
                name: "SelectedRuleId",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.CreateTable(
                name: "ComponentProperty",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BulckComponentId = table.Column<long>(type: "bigint", nullable: false),
                    SelectedRuleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlacementPreference = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentOfReference = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentProperty", x => x.id);
                    table.ForeignKey(
                        name: "FK_ComponentProperty_BulckComponent_BulckComponentId",
                        column: x => x.BulckComponentId,
                        principalSchema: "Workflow",
                        principalTable: "BulckComponent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentProperty_SelectedRule_SelectedRuleId",
                        column: x => x.SelectedRuleId,
                        principalTable: "SelectedRule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentProperty_BulckComponentId",
                schema: "Workflow",
                table: "ComponentProperty",
                column: "BulckComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentProperty_SelectedRuleId",
                schema: "Workflow",
                table: "ComponentProperty",
                column: "SelectedRuleId");
        }
    }
}
