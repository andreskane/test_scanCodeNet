using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bulkprocess1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DataType",
                schema: "Workflow",
                table: "TemplateComponent",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BulkProcessId",
                schema: "Workflow",
                table: "DynamicForm",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BulkProcess",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlacementPreference = table.Column<int>(type: "int", nullable: false),
                    ComponentOfReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkProcess", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SelectedRule",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedRule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Component",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typeComponent = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Component", x => x.id);
                    table.ForeignKey(
                        name: "FK_Component_BulkProcess_id",
                        column: x => x.id,
                        principalSchema: "Workflow",
                        principalTable: "BulkProcess",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentProperty",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlacementPreference = table.Column<bool>(type: "bit", nullable: false),
                    ComponentOfReference = table.Column<bool>(type: "bit", nullable: false),
                    SelectedRuleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ComponentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentProperty", x => x.id);
                    table.ForeignKey(
                        name: "FK_ComponentProperty_Component_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "Workflow",
                        principalTable: "Component",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ComponentProperty_SelectedRule_SelectedRuleId",
                        column: x => x.SelectedRuleId,
                        principalTable: "SelectedRule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicForm_BulkProcessId",
                schema: "Workflow",
                table: "DynamicForm",
                column: "BulkProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentProperty_ComponentId",
                schema: "Workflow",
                table: "ComponentProperty",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentProperty_SelectedRuleId",
                schema: "Workflow",
                table: "ComponentProperty",
                column: "SelectedRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicForm_BulkProcess_BulkProcessId",
                schema: "Workflow",
                table: "DynamicForm",
                column: "BulkProcessId",
                principalSchema: "Workflow",
                principalTable: "BulkProcess",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicForm_BulkProcess_BulkProcessId",
                schema: "Workflow",
                table: "DynamicForm");

            migrationBuilder.DropTable(
                name: "ComponentProperty",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "Component",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "SelectedRule");

            migrationBuilder.DropTable(
                name: "BulkProcess",
                schema: "Workflow");

            migrationBuilder.DropIndex(
                name: "IX_DynamicForm_BulkProcessId",
                schema: "Workflow",
                table: "DynamicForm");

            migrationBuilder.DropColumn(
                name: "BulkProcessId",
                schema: "Workflow",
                table: "DynamicForm");

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                schema: "Workflow",
                table: "TemplateComponent",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
