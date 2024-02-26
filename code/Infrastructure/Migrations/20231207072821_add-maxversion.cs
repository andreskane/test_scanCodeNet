using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addmaxversion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BulckComponent_SelectedRule_SelectedRuleId",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "SelectedRule",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "SelectedRule",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SelectedRule",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SelectedRuleId",
                schema: "Workflow",
                table: "BulckComponent",
                newName: "SelectedRuleid");

            migrationBuilder.RenameIndex(
                name: "IX_BulckComponent_SelectedRuleId",
                schema: "Workflow",
                table: "BulckComponent",
                newName: "IX_BulckComponent_SelectedRuleid");

            migrationBuilder.AddColumn<int>(
                name: "MaxVersion",
                schema: "Workflow",
                table: "DynamicForm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BulckComponent_SelectedRule_SelectedRuleid",
                schema: "Workflow",
                table: "BulckComponent",
                column: "SelectedRuleid",
                principalTable: "SelectedRule",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BulckComponent_SelectedRule_SelectedRuleid",
                schema: "Workflow",
                table: "BulckComponent");

            migrationBuilder.DropColumn(
                name: "MaxVersion",
                schema: "Workflow",
                table: "DynamicForm");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "SelectedRule",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "SelectedRule",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SelectedRule",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SelectedRuleid",
                schema: "Workflow",
                table: "BulckComponent",
                newName: "SelectedRuleId");

            migrationBuilder.RenameIndex(
                name: "IX_BulckComponent_SelectedRuleid",
                schema: "Workflow",
                table: "BulckComponent",
                newName: "IX_BulckComponent_SelectedRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BulckComponent_SelectedRule_SelectedRuleId",
                schema: "Workflow",
                table: "BulckComponent",
                column: "SelectedRuleId",
                principalTable: "SelectedRule",
                principalColumn: "Id");
        }
    }
}
