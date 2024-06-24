using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImportOreqM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Specdocuments",
                columns: table => new
                {
                    SpecdocumentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    doctype = table.Column<string>(type: "TEXT", nullable: false),
                    moduleName = table.Column<string>(type: "TEXT", nullable: false),
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", nullable: false),
                    source = table.Column<string>(type: "TEXT", nullable: false),
                    version = table.Column<string>(type: "TEXT", nullable: false),
                    violations = table.Column<string>(type: "TEXT", nullable: false),
                    oreqmViolations = table.Column<string>(type: "TEXT", nullable: false),
                    covstatus = table.Column<string>(type: "TEXT", nullable: false),
                    internalId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specdocuments", x => x.SpecdocumentId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Specdocuments");
        }
    }
}
