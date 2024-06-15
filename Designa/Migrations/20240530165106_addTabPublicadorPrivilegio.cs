using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Designa.Migrations
{
    /// <inheritdoc />
    public partial class addTabPublicadorPrivilegio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Privilegio",
                table: "Publicadores");

            migrationBuilder.CreateTable(
                name: "PublicadorPrivilegio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Privilegio = table.Column<int>(type: "INTEGER", nullable: false),
                    PublicadorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicadorPrivilegio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicadorPrivilegio_Publicadores_PublicadorId",
                        column: x => x.PublicadorId,
                        principalTable: "Publicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicadorPrivilegio_PublicadorId",
                table: "PublicadorPrivilegio",
                column: "PublicadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicadorPrivilegio");

            migrationBuilder.AddColumn<int>(
                name: "Privilegio",
                table: "Publicadores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
