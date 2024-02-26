using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Workflow");

            migrationBuilder.CreateTable(
                name: "ListDefinition",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    KeyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_ListDefinition", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Workflow",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Workflow", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ListValue",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListId = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_ListValue", x => x.id);
                    table.ForeignKey(
                        name: "FK_ListValue_ListDefinition_ListId",
                        column: x => x.ListId,
                        principalSchema: "Workflow",
                        principalTable: "ListDefinition",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListTenantWorkflow",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListId = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkflowId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_ListTenantWorkflow", x => x.id);
                    table.ForeignKey(
                        name: "FK_ListTenantWorkflow_ListDefinition_ListId",
                        column: x => x.ListId,
                        principalSchema: "Workflow",
                        principalTable: "ListDefinition",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListTenantWorkflow_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalSchema: "Workflow",
                        principalTable: "Workflow",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowDynamicForm",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Layout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeFlow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_WorkflowDynamicForm", x => x.id);
                    table.ForeignKey(
                        name: "FK_WorkflowDynamicForm_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalSchema: "Workflow",
                        principalTable: "Workflow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowProductAttributes",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    DynamicFormId = table.Column<long>(type: "bigint", nullable: false),
                    AtributteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtraInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Optional = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_WorkflowProductAttributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_WorkflowProductAttributes_WorkflowDynamicForm_DynamicFormId",
                        column: x => x.DynamicFormId,
                        principalSchema: "Workflow",
                        principalTable: "WorkflowDynamicForm",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListTenantWorkflow_ListId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_ListTenantWorkflow_WorkflowId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_ListValue_ListId",
                schema: "Workflow",
                table: "ListValue",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDynamicForm_WorkflowId",
                schema: "Workflow",
                table: "WorkflowDynamicForm",
                column: "WorkflowId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowProductAttributes_DynamicFormId",
                schema: "Workflow",
                table: "WorkflowProductAttributes",
                column: "DynamicFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListTenantWorkflow",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "ListValue",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "WorkflowProductAttributes",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "ListDefinition",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "WorkflowDynamicForm",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "Workflow",
                schema: "Workflow");
        }
    }
}
