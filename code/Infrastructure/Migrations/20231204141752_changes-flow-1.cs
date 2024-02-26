using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changesflow1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanId",
                schema: "Workflow",
                table: "DynamicFormTemplate");

            migrationBuilder.AlterColumn<int>(
                name: "Version",
                schema: "Workflow",
                table: "DynamicFormTemplate",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CodeFlowActive",
                schema: "Workflow",
                table: "DynamicFormTemplate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");


            migrationBuilder.DropColumn(
            name: "Id",
            schema: "Workflow",
            table: "DynamicFormPlans");


            migrationBuilder.AddColumn<long>(
                 name: "Id",
                 schema: "Workflow",
                 table: "DynamicFormPlans",
                 type: "bigint",
                 nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Version",
                schema: "Workflow",
                table: "DynamicFormItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Version",
                schema: "Workflow",
                table: "DynamicForm",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CodeFlowActive",
                schema: "Workflow",
                table: "DynamicForm",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeFlowActive",
                schema: "Workflow",
                table: "DynamicFormTemplate");

            migrationBuilder.DropColumn(
                name: "CodeFlowActive",
                schema: "Workflow",
                table: "DynamicForm");

            migrationBuilder.AlterColumn<string>(
                name: "Version",
                schema: "Workflow",
                table: "DynamicFormTemplate",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "PlanId",
                schema: "Workflow",
                table: "DynamicFormTemplate",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "Workflow",
                table: "DynamicFormPlans",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Version",
                schema: "Workflow",
                table: "DynamicFormItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Version",
                schema: "Workflow",
                table: "DynamicForm",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
