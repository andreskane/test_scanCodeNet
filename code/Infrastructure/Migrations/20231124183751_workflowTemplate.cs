using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class workflowTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowDynamicForm_Workflow_WorkflowId",
                schema: "Workflow",
                table: "WorkflowDynamicForm");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowDynamicForm_WorkflowId",
                schema: "Workflow",
                table: "WorkflowDynamicForm");

            migrationBuilder.AlterColumn<long>(
                name: "WorkflowId",
                schema: "Workflow",
                table: "WorkflowDynamicForm",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "WorkflowTemplateId",
                schema: "Workflow",
                table: "WorkflowDynamicForm",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WorkflowTemplateId",
                schema: "Workflow",
                table: "Rule",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WorkflowTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkflowTemplate",
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
                    table.PrimaryKey("PK_WorkflowTemplate", x => x.id);
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
                name: "IX_Rule_WorkflowTemplateId",
                schema: "Workflow",
                table: "Rule",
                column: "WorkflowTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ListTenantWorkflow_WorkflowTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                column: "WorkflowTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListTenantWorkflow_WorkflowTemplate_WorkflowTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow",
                column: "WorkflowTemplateId",
                principalSchema: "Workflow",
                principalTable: "WorkflowTemplate",
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
                name: "FK_WorkflowDynamicForm_WorkflowTemplate_WorkflowTemplateId",
                schema: "Workflow",
                table: "WorkflowDynamicForm",
                column: "WorkflowTemplateId",
                principalSchema: "Workflow",
                principalTable: "WorkflowTemplate",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowDynamicForm_Workflow_WorkflowId",
                schema: "Workflow",
                table: "WorkflowDynamicForm",
                column: "WorkflowId",
                principalSchema: "Workflow",
                principalTable: "Workflow",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListTenantWorkflow_WorkflowTemplate_WorkflowTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_Rule_WorkflowTemplate_WorkflowTemplateId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowDynamicForm_WorkflowTemplate_WorkflowTemplateId",
                schema: "Workflow",
                table: "WorkflowDynamicForm");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowDynamicForm_Workflow_WorkflowId",
                schema: "Workflow",
                table: "WorkflowDynamicForm");

            migrationBuilder.DropTable(
                name: "WorkflowTemplate",
                schema: "Workflow");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowDynamicForm_WorkflowId",
                schema: "Workflow",
                table: "WorkflowDynamicForm");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowDynamicForm_WorkflowTemplateId",
                schema: "Workflow",
                table: "WorkflowDynamicForm");

            migrationBuilder.DropIndex(
                name: "IX_Rule_WorkflowTemplateId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_ListTenantWorkflow_WorkflowTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow");

            migrationBuilder.DropColumn(
                name: "WorkflowTemplateId",
                schema: "Workflow",
                table: "WorkflowDynamicForm");

            migrationBuilder.DropColumn(
                name: "WorkflowTemplateId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "WorkflowTemplateId",
                schema: "Workflow",
                table: "ListTenantWorkflow");

            migrationBuilder.AlterColumn<long>(
                name: "WorkflowId",
                schema: "Workflow",
                table: "WorkflowDynamicForm",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDynamicForm_WorkflowId",
                schema: "Workflow",
                table: "WorkflowDynamicForm",
                column: "WorkflowId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowDynamicForm_Workflow_WorkflowId",
                schema: "Workflow",
                table: "WorkflowDynamicForm",
                column: "WorkflowId",
                principalSchema: "Workflow",
                principalTable: "Workflow",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
