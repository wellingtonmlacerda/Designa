using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Designa.Migrations
{
    /// <inheritdoc />
    public partial class AlterTbParte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListasNegra_Publicadores_IrmaoId",
                table: "ListasNegra");

            migrationBuilder.DropIndex(
                name: "IX_PublicadorPartes_ParteId",
                table: "PublicadorPartes");

            migrationBuilder.DropIndex(
                name: "IX_ListasNegra_IrmaoId",
                table: "ListasNegra");

            migrationBuilder.DropColumn(
                name: "IrmaoId",
                table: "ListasNegra");

            migrationBuilder.AddColumn<int>(
                name: "PublicadorParteId",
                table: "Partes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PublicadorPartes_ParteId",
                table: "PublicadorPartes",
                column: "ParteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListasNegra_PublicadorId",
                table: "ListasNegra",
                column: "PublicadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListasNegra_Publicadores_PublicadorId",
                table: "ListasNegra",
                column: "PublicadorId",
                principalTable: "Publicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListasNegra_Publicadores_PublicadorId",
                table: "ListasNegra");

            migrationBuilder.DropIndex(
                name: "IX_PublicadorPartes_ParteId",
                table: "PublicadorPartes");

            migrationBuilder.DropIndex(
                name: "IX_ListasNegra_PublicadorId",
                table: "ListasNegra");

            migrationBuilder.DropColumn(
                name: "PublicadorParteId",
                table: "Partes");

            migrationBuilder.AddColumn<int>(
                name: "IrmaoId",
                table: "ListasNegra",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PublicadorPartes_ParteId",
                table: "PublicadorPartes",
                column: "ParteId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasNegra_IrmaoId",
                table: "ListasNegra",
                column: "IrmaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListasNegra_Publicadores_IrmaoId",
                table: "ListasNegra",
                column: "IrmaoId",
                principalTable: "Publicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
