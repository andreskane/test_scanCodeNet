using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeNameRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFormRule_Rule_RuleId",
                schema: "Workflow",
                table: "DynamicFormRule");

            migrationBuilder.DropForeignKey(
                name: "FK_RuleAction_Rule_RuleId",
                schema: "Workflow",
                table: "RuleAction");

            migrationBuilder.DropTable(
                name: "Rule",
                schema: "Workflow");

            migrationBuilder.CreateTable(
                name: "RuleDynamic",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_RuleDynamic", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFormRule_RuleDynamic_RuleId",
                schema: "Workflow",
                table: "DynamicFormRule",
                column: "RuleId",
                principalSchema: "Workflow",
                principalTable: "RuleDynamic",
                principalColumn: "id");
            //todo: fix this
            //migrationBuilder.AddForeignKey(
            //    name: "FK_RuleAction_RuleDynamic_RuleId",
            //    schema: "Workflow",
            //    table: "RuleAction",
            //    column: "RuleId",
            //    principalSchema: "Workflow",
            //    principalTable: "RuleDynamic",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFormRule_RuleDynamic_RuleId",
                schema: "Workflow",
                table: "DynamicFormRule");

            migrationBuilder.DropForeignKey(
                name: "FK_RuleAction_RuleDynamic_RuleId",
                schema: "Workflow",
                table: "RuleAction");

            migrationBuilder.DropTable(
                name: "RuleDynamic",
                schema: "Workflow");

            migrationBuilder.CreateTable(
                name: "Rule",
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
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    KeyDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rule", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFormRule_Rule_RuleId",
                schema: "Workflow",
                table: "DynamicFormRule",
                column: "RuleId",
                principalSchema: "Workflow",
                principalTable: "Rule",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RuleAction_Rule_RuleId",
                schema: "Workflow",
                table: "RuleAction",
                column: "RuleId",
                principalSchema: "Workflow",
                principalTable: "Rule",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
