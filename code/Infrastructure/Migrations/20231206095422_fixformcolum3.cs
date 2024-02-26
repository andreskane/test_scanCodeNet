using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixformcolum3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormComponentRule",
                newName: "DynamicFormItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormComponentRule_DynamicFormItemId",
                schema: "Workflow",
                table: "DynamicFormComponentRule",
                column: "DynamicFormItemId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_DynamicFormComponentRule_DynamicFormItem_DynamicFormItemId",
            //    schema: "Workflow",
            //    table: "DynamicFormComponentRule",
            //    column: "DynamicFormItemId",
            //    principalSchema: "Workflow",
            //    principalTable: "DynamicFormItem",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFormComponentRule_DynamicFormItem_DynamicFormItemId",
                schema: "Workflow",
                table: "DynamicFormComponentRule");

            migrationBuilder.DropIndex(
                name: "IX_DynamicFormComponentRule_DynamicFormItemId",
                schema: "Workflow",
                table: "DynamicFormComponentRule");

            migrationBuilder.RenameColumn(
                name: "DynamicFormItemId",
                schema: "Workflow",
                table: "DynamicFormComponentRule",
                newName: "DynamicFormId");
        }
    }
}
