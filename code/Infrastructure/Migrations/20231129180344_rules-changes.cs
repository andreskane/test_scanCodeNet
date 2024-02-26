using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ruleschanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_DynamicFormTemplate_DynamicFormTemplateId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropForeignKey(
                name: "FK_Rule_DynamicForm_WorkflowId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_DynamicFormTemplateId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_WorkflowId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "DynamicFormTemplateId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "WorkflowId",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "Workflow",
                table: "RuleAction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Workflow",
                table: "Rule",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                schema: "Workflow",
                table: "Rule",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Workflow",
                table: "Rule",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Version",
                schema: "Workflow",
                table: "Rule",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DynamicFormRule",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DynamicFormId = table.Column<long>(type: "bigint", nullable: false),
                    RuleId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_DynamicFormRule", x => x.id);
                    table.ForeignKey(
                        name: "FK_DynamicFormRule_DynamicForm_DynamicFormId",
                        column: x => x.DynamicFormId,
                        principalSchema: "Workflow",
                        principalTable: "DynamicForm",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_DynamicFormRule_Rule_RuleId",
                        column: x => x.RuleId,
                        principalSchema: "Workflow",
                        principalTable: "Rule",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormRule_DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormRule",
                column: "DynamicFormId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormRule_RuleId",
                schema: "Workflow",
                table: "DynamicFormRule",
                column: "RuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicFormRule",
                schema: "Workflow");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "Workflow",
                table: "RuleAction");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "Enabled",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "Workflow",
                table: "Rule");

            migrationBuilder.AddColumn<long>(
                name: "DynamicFormTemplateId",
                schema: "Workflow",
                table: "Rule",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WorkflowId",
                schema: "Workflow",
                table: "Rule",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Rule_DynamicFormTemplateId",
                schema: "Workflow",
                table: "Rule",
                column: "DynamicFormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_WorkflowId",
                schema: "Workflow",
                table: "Rule",
                column: "WorkflowId");

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
    }
}
