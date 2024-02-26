using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_templatecomponents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemplateComponent",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    placeholder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    groupID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateComponent", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateComponent",
                schema: "Workflow");
        }
    }
}
