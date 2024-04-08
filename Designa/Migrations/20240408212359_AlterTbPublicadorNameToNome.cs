using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Designa.Migrations
{
    /// <inheritdoc />
    public partial class AlterTbPublicadorNameToNome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Publicadores",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Publicadores",
                newName: "Name");
        }
    }
}
