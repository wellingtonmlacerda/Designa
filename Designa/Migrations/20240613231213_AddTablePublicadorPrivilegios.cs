using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Designa.Migrations
{
    /// <inheritdoc />
    public partial class AddTablePublicadorPrivilegios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicadorPrivilegio_Publicadores_PublicadorId",
                table: "PublicadorPrivilegio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicadorPrivilegio",
                table: "PublicadorPrivilegio");

            migrationBuilder.RenameTable(
                name: "PublicadorPrivilegio",
                newName: "PublicadorPrivilegios");

            migrationBuilder.RenameIndex(
                name: "IX_PublicadorPrivilegio_PublicadorId",
                table: "PublicadorPrivilegios",
                newName: "IX_PublicadorPrivilegios_PublicadorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicadorPrivilegios",
                table: "PublicadorPrivilegios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicadorPrivilegios_Publicadores_PublicadorId",
                table: "PublicadorPrivilegios",
                column: "PublicadorId",
                principalTable: "Publicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicadorPrivilegios_Publicadores_PublicadorId",
                table: "PublicadorPrivilegios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicadorPrivilegios",
                table: "PublicadorPrivilegios");

            migrationBuilder.RenameTable(
                name: "PublicadorPrivilegios",
                newName: "PublicadorPrivilegio");

            migrationBuilder.RenameIndex(
                name: "IX_PublicadorPrivilegios_PublicadorId",
                table: "PublicadorPrivilegio",
                newName: "IX_PublicadorPrivilegio_PublicadorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicadorPrivilegio",
                table: "PublicadorPrivilegio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicadorPrivilegio_Publicadores_PublicadorId",
                table: "PublicadorPrivilegio",
                column: "PublicadorId",
                principalTable: "Publicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
