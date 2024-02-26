using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicFormProductAttributes",
                schema: "Workflow");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicFormProductAttributes",
                schema: "Workflow",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DynamicFormId = table.Column<long>(type: "bigint", nullable: false),
                    AtributteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Optional = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicFormProductAttributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_DynamicFormProductAttributes_DynamicFormItem_DynamicFormId",
                        column: x => x.DynamicFormId,
                        principalSchema: "Workflow",
                        principalTable: "DynamicFormItem",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormProductAttributes_DynamicFormId",
                schema: "Workflow",
                table: "DynamicFormProductAttributes",
                column: "DynamicFormId");
        }
    }
}
