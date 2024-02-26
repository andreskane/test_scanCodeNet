using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bulkprocess4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                schema: "Workflow",
                table: "RuleAction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Workflow",
                table: "RuleAction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "RequestBy",
                schema: "Workflow",
                table: "RuleAction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestType",
                schema: "Workflow",
                table: "RuleAction",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestBy",
                schema: "Workflow",
                table: "RuleAction");

            migrationBuilder.DropColumn(
                name: "RequestType",
                schema: "Workflow",
                table: "RuleAction");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                schema: "Workflow",
                table: "RuleAction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Workflow",
                table: "RuleAction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
