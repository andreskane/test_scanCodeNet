using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bulkprocess2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentProperty_Component_ComponentId",
                schema: "Workflow",
                table: "ComponentProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicForm_BulkProcess_BulkProcessId",
                schema: "Workflow",
                table: "DynamicForm");

            migrationBuilder.DropTable(
                name: "Component",
                schema: "Workflow");

            migrationBuilder.DropIndex(
                name: "IX_DynamicForm_BulkProcessId",
                schema: "Workflow",
                table: "DynamicForm");

            migrationBuilder.DropIndex(
                name: "IX_ComponentProperty_ComponentId",
                schema: "Workflow",
                table: "ComponentProperty");

            migrationBuilder.DropColumn(
                name: "BulkProcessId",
                schema: "Workflow",
                table: "DynamicForm");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                schema: "Workflow",
                table: "ComponentProperty");

            migrationBuilder.AddColumn<long>(
                name: "BulckComponentId",
                schema: "Workflow",
                table: "ComponentProperty",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "BulckComponent",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typeComponent = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    BulkProcessId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_BulckComponent", x => x.id);
                    table.ForeignKey(
                        name: "FK_BulckComponent_BulkProcess_BulkProcessId",
                        column: x => x.BulkProcessId,
                        principalSchema: "Workflow",
                        principalTable: "BulkProcess",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicFormBulckProcess",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DynamicFormId = table.Column<long>(type: "bigint", nullable: false),
                    BulkProcessId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_DynamicFormBulckProcess", x => x.id);
                    table.ForeignKey(
                        name: "FK_DynamicFormBulckProcess_BulkProcess_BulkProcessId",
                        column: x => x.BulkProcessId,
                        principalSchema: "Workflow",
                        principalTable: "BulkProcess",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_DynamicFormBulckProcess_DynamicForm_DynamicFormId",
                        column: x => x.DynamicFormId,
                        principalSchema: "Workflow",
                        principalTable: "DynamicForm",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentProperty_BulckComponentId",
                schema: "Workflow",
                table: "ComponentProperty",
                column: "BulckComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_BulckComponent_BulkProcessId",
                schema: "Workflow",
                table: "BulckComponent",
                column: "BulkProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormBulckProcess_BulkProcessId",
                schema: "Workflow",
                table: "DynamicFormBulckProcess",
                column: "BulkProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormBulckProcess_DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormBulckProcess",
                column: "DynamicFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentProperty_BulckComponent_BulckComponentId",
                schema: "Workflow",
                table: "ComponentProperty",
                column: "BulckComponentId",
                principalSchema: "Workflow",
                principalTable: "BulckComponent",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentProperty_BulckComponent_BulckComponentId",
                schema: "Workflow",
                table: "ComponentProperty");

            migrationBuilder.DropTable(
                name: "BulckComponent",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "DynamicFormBulckProcess",
                schema: "Workflow");

            migrationBuilder.DropIndex(
                name: "IX_ComponentProperty_BulckComponentId",
                schema: "Workflow",
                table: "ComponentProperty");

            migrationBuilder.DropColumn(
                name: "BulckComponentId",
                schema: "Workflow",
                table: "ComponentProperty");

            migrationBuilder.AddColumn<long>(
                name: "BulkProcessId",
                schema: "Workflow",
                table: "DynamicForm",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ComponentId",
                schema: "Workflow",
                table: "ComponentProperty",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Component",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    typeComponent = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentProperty_Component_ComponentId",
                schema: "Workflow",
                table: "ComponentProperty",
                column: "ComponentId",
                principalSchema: "Workflow",
                principalTable: "Component",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicForm_BulkProcess_BulkProcessId",
                schema: "Workflow",
                table: "DynamicForm",
                column: "BulkProcessId",
                principalSchema: "Workflow",
                principalTable: "BulkProcess",
                principalColumn: "id");
        }
    }
}
