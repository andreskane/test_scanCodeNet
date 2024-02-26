using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeName2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFormItem_DynamicFormTemplate_WorkflowTemplateId",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFormItem_DynamicForm_WorkflowId",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.DropIndex(
                name: "IX_DynamicFormItem_WorkflowId",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.DropIndex(
                name: "IX_DynamicFormItem_WorkflowTemplateId",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.RenameColumn(
                name: "WorkflowTemplateId",
                schema: "Workflow",
                table: "DynamicFormItem",
                newName: "DynamicFormTemplateId1");

            migrationBuilder.RenameColumn(
                name: "WorkflowId",
                schema: "Workflow",
                table: "DynamicFormItem",
                newName: "DynamicFormTemplateId");

            migrationBuilder.AddColumn<long>(
                name: "DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DynamicFormId1",
                schema: "Workflow",
                table: "DynamicFormItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormItem_DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormItem",
                column: "DynamicFormId",
                unique: true,
                filter: "[DynamicFormId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormItem_DynamicFormId1",
                schema: "Workflow",
                table: "DynamicFormItem",
                column: "DynamicFormId1");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormItem_DynamicFormTemplateId",
                schema: "Workflow",
                table: "DynamicFormItem",
                column: "DynamicFormTemplateId",
                unique: true,
                filter: "[DynamicFormTemplateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormItem_DynamicFormTemplateId1",
                schema: "Workflow",
                table: "DynamicFormItem",
                column: "DynamicFormTemplateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFormItem_DynamicFormTemplate_DynamicFormTemplateId",
                schema: "Workflow",
                table: "DynamicFormItem",
                column: "DynamicFormTemplateId",
                principalSchema: "Workflow",
                principalTable: "DynamicFormTemplate",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFormItem_DynamicFormTemplate_DynamicFormTemplateId1",
                schema: "Workflow",
                table: "DynamicFormItem",
                column: "DynamicFormTemplateId1",
                principalSchema: "Workflow",
                principalTable: "DynamicFormTemplate",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFormItem_DynamicForm_DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormItem",
                column: "DynamicFormId",
                principalSchema: "Workflow",
                principalTable: "DynamicForm",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFormItem_DynamicForm_DynamicFormId1",
                schema: "Workflow",
                table: "DynamicFormItem",
                column: "DynamicFormId1",
                principalSchema: "Workflow",
                principalTable: "DynamicForm",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFormItem_DynamicFormTemplate_DynamicFormTemplateId",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFormItem_DynamicFormTemplate_DynamicFormTemplateId1",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFormItem_DynamicForm_DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFormItem_DynamicForm_DynamicFormId1",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.DropIndex(
                name: "IX_DynamicFormItem_DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.DropIndex(
                name: "IX_DynamicFormItem_DynamicFormId1",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.DropIndex(
                name: "IX_DynamicFormItem_DynamicFormTemplateId",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.DropIndex(
                name: "IX_DynamicFormItem_DynamicFormTemplateId1",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.DropColumn(
                name: "DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.DropColumn(
                name: "DynamicFormId1",
                schema: "Workflow",
                table: "DynamicFormItem");

            migrationBuilder.RenameColumn(
                name: "DynamicFormTemplateId1",
                schema: "Workflow",
                table: "DynamicFormItem",
                newName: "WorkflowTemplateId");

            migrationBuilder.RenameColumn(
                name: "DynamicFormTemplateId",
                schema: "Workflow",
                table: "DynamicFormItem",
                newName: "WorkflowId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFormItem_DynamicFormTemplate_WorkflowTemplateId",
                schema: "Workflow",
                table: "DynamicFormItem",
                column: "WorkflowTemplateId",
                principalSchema: "Workflow",
                principalTable: "DynamicFormTemplate",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFormItem_DynamicForm_WorkflowId",
                schema: "Workflow",
                table: "DynamicFormItem",
                column: "WorkflowId",
                principalSchema: "Workflow",
                principalTable: "DynamicForm",
                principalColumn: "id");
        }
    }
}
