using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatefieldthenandwhen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Script",
                schema: "Workflow",
                table: "RuleAction");

            migrationBuilder.AddColumn<string>(
                name: "ThenScript",
                schema: "Workflow",
                table: "RuleAction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhenScript",
                schema: "Workflow",
                table: "RuleAction",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThenScript",
                schema: "Workflow",
                table: "RuleAction");

            migrationBuilder.DropColumn(
                name: "WhenScript",
                schema: "Workflow",
                table: "RuleAction");

            migrationBuilder.AddColumn<string>(
                name: "Script",
                schema: "Workflow",
                table: "RuleAction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
