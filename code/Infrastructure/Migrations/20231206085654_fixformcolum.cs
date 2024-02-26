using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixformcolum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RuleId",
                schema: "Workflow",
                table: "DynamicFormComponentRule",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormComponentRule",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ComponentName",
                schema: "Workflow",
                table: "DynamicFormComponentRule",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComponentPropertyId",
                schema: "Workflow",
                table: "DynamicFormComponentRule",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComponentPropertyId",
                schema: "Workflow",
                table: "DynamicFormComponentRule");

            migrationBuilder.AlterColumn<string>(
                name: "RuleId",
                schema: "Workflow",
                table: "DynamicFormComponentRule",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormComponentRule",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "ComponentName",
                schema: "Workflow",
                table: "DynamicFormComponentRule",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
