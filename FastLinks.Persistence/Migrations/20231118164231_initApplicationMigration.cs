using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastLinks.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initApplicationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlLinks",
                columns: table => new
                {
                    ShortUrlAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UrlAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfEntries = table.Column<int>(type: "int", nullable: false),
                    UserCreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlLinks", x => x.ShortUrlAddress);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlLinks");
        }
    }
}
