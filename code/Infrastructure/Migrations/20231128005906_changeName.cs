using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListTenantWorkflow_WorkflowTemplate_WorkflowTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_ListTenantWorkflow_Workflow_WorkflowId",
                schema: "Workflow",
                table: "ListTenantWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_Rule_WorkflowTemplate_WorkflowTemplateId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Workflow_WorkflowId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropTable(
                name: "WorkflowProductAttributes",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "WorkflowDynamicForm",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "WorkflowTemplate",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "Workflow",
                schema: "Workflow");

            migrationBuilder.RenameColumn(
                name: "WorkflowTemplateId",
                schema: "Workflow",
                table: "Rule",
                newName: "DynamicFormTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_Rule_WorkflowTemplateId",
                schema: "Workflow",
                table: "Rule",
                newName: "IX_Rule_DynamicFormTemplateId");

            migrationBuilder.RenameColumn(
                name: "WorkflowTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                newName: "DynamicFormTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_ListTenantWorkflow_WorkflowTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                newName: "IX_ListTenantWorkflow_DynamicFormTemplateId");

            migrationBuilder.CreateTable(
                name: "DynamicForm",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_DynamicForm", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicFormTemplate",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_DynamicFormTemplate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicFormItem",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowId = table.Column<long>(type: "bigint", nullable: true),
                    WorkflowTemplateId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_DynamicFormItem", x => x.id);
                    table.ForeignKey(
                        name: "FK_DynamicFormItem_DynamicFormTemplate_WorkflowTemplateId",
                        column: x => x.WorkflowTemplateId,
                        principalSchema: "Workflow",
                        principalTable: "DynamicFormTemplate",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_DynamicFormItem_DynamicForm_WorkflowId",
                        column: x => x.WorkflowId,
                        principalSchema: "Workflow",
                        principalTable: "DynamicForm",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DynamicFormProductAttributes",
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
                    table.PrimaryKey("PK_DynamicFormProductAttributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_DynamicFormProductAttributes_DynamicFormItem_DynamicFormId",
                        column: x => x.DynamicFormId,
                        principalSchema: "Workflow",
                        principalTable: "DynamicFormItem",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormItem_WorkflowId",
                schema: "Workflow",
                table: "DynamicFormItem",
                column: "WorkflowId",
                unique: true,
                filter: "[WorkflowId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormItem_WorkflowTemplateId",
                schema: "Workflow",
                table: "DynamicFormItem",
                column: "WorkflowTemplateId",
                unique: true,
                filter: "[WorkflowTemplateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormProductAttributes_DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormProductAttributes",
                column: "DynamicFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListTenantWorkflow_DynamicFormTemplate_DynamicFormTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                column: "DynamicFormTemplateId",
                principalSchema: "Workflow",
                principalTable: "DynamicFormTemplate",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListTenantWorkflow_DynamicForm_WorkflowId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                column: "WorkflowId",
                principalSchema: "Workflow",
                principalTable: "DynamicForm",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_DynamicFormTemplate_DynamicFormTemplateId",
                schema: "Workflow",
                table: "Rule",
                column: "DynamicFormTemplateId",
                principalSchema: "Workflow",
                principalTable: "DynamicFormTemplate",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_DynamicForm_WorkflowId",
                schema: "Workflow",
                table: "Rule",
                column: "WorkflowId",
                principalSchema: "Workflow",
                principalTable: "DynamicForm",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListTenantWorkflow_DynamicFormTemplate_DynamicFormTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_ListTenantWorkflow_DynamicForm_WorkflowId",
                schema: "Workflow",
                table: "ListTenantWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_Rule_DynamicFormTemplate_DynamicFormTemplateId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropForeignKey(
                name: "FK_Rule_DynamicForm_WorkflowId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropTable(
                name: "DynamicFormProductAttributes",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "DynamicFormItem",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "DynamicFormTemplate",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "DynamicForm",
                schema: "Workflow");

            migrationBuilder.RenameColumn(
                name: "DynamicFormTemplateId",
                schema: "Workflow",
                table: "Rule",
                newName: "WorkflowTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_Rule_DynamicFormTemplateId",
                schema: "Workflow",
                table: "Rule",
                newName: "IX_Rule_WorkflowTemplateId");

            migrationBuilder.RenameColumn(
                name: "DynamicFormTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                newName: "WorkflowTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_ListTenantWorkflow_DynamicFormTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                newName: "IX_ListTenantWorkflow_WorkflowTemplateId");

            migrationBuilder.CreateTable(
                name: "Workflow",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PlanId = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflow", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowTemplate",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PlanId = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowTemplate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowDynamicForm",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowId = table.Column<long>(type: "bigint", nullable: true),
                    WorkflowTemplateId = table.Column<long>(type: "bigint", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeFlow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Layout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowDynamicForm", x => x.id);
                    table.ForeignKey(
                        name: "FK_WorkflowDynamicForm_WorkflowTemplate_WorkflowTemplateId",
                        column: x => x.WorkflowTemplateId,
                        principalSchema: "Workflow",
                        principalTable: "WorkflowTemplate",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_WorkflowDynamicForm_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalSchema: "Workflow",
                        principalTable: "Workflow",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowProductAttributes",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DynamicFormId = table.Column<long>(type: "bigint", nullable: false),
                    AtributteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Optional = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
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
                name: "IX_WorkflowDynamicForm_WorkflowId",
                schema: "Workflow",
                table: "WorkflowDynamicForm",
                column: "WorkflowId",
                unique: true,
                filter: "[WorkflowId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDynamicForm_WorkflowTemplateId",
                schema: "Workflow",
                table: "WorkflowDynamicForm",
                column: "WorkflowTemplateId",
                unique: true,
                filter: "[WorkflowTemplateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowProductAttributes_DynamicFormId",
                schema: "Workflow",
                table: "WorkflowProductAttributes",
                column: "DynamicFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListTenantWorkflow_WorkflowTemplate_WorkflowTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                column: "WorkflowTemplateId",
                principalSchema: "Workflow",
                principalTable: "WorkflowTemplate",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListTenantWorkflow_Workflow_WorkflowId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                column: "WorkflowId",
                principalSchema: "Workflow",
                principalTable: "Workflow",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_WorkflowTemplate_WorkflowTemplateId",
                schema: "Workflow",
                table: "Rule",
                column: "WorkflowTemplateId",
                principalSchema: "Workflow",
                principalTable: "WorkflowTemplate",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Workflow_WorkflowId",
                schema: "Workflow",
                table: "Rule",
                column: "WorkflowId",
                principalSchema: "Workflow",
                principalTable: "Workflow",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
