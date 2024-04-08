using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Designa.Migrations
{
    /// <inheritdoc />
    public partial class AlterModeloIrmaoToPublicador2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IrmaoPartes_Irmaos_PublicadorAjudanteId",
                table: "IrmaoPartes");

            migrationBuilder.DropForeignKey(
                name: "FK_IrmaoPartes_Irmaos_PublicadorId",
                table: "IrmaoPartes");

            migrationBuilder.DropForeignKey(
                name: "FK_IrmaoPartes_Partes_ParteId",
                table: "IrmaoPartes");

            migrationBuilder.DropForeignKey(
                name: "FK_Irmaos_Irmaos_MaeId",
                table: "Irmaos");

            migrationBuilder.DropForeignKey(
                name: "FK_Irmaos_Irmaos_PaiId",
                table: "Irmaos");

            migrationBuilder.DropForeignKey(
                name: "FK_ListasNegra_Irmaos_IrmaoId",
                table: "ListasNegra");

            migrationBuilder.DropForeignKey(
                name: "FK_ListasNegra_Irmaos_PublicadorListaNegraId",
                table: "ListasNegra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Irmaos",
                table: "Irmaos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IrmaoPartes",
                table: "IrmaoPartes");

            migrationBuilder.RenameTable(
                name: "Irmaos",
                newName: "Publicadores");

            migrationBuilder.RenameTable(
                name: "IrmaoPartes",
                newName: "PublicadorPartes");

            migrationBuilder.RenameIndex(
                name: "IX_Irmaos_PaiId",
                table: "Publicadores",
                newName: "IX_Publicadores_PaiId");

            migrationBuilder.RenameIndex(
                name: "IX_Irmaos_MaeId",
                table: "Publicadores",
                newName: "IX_Publicadores_MaeId");

            migrationBuilder.RenameIndex(
                name: "IX_IrmaoPartes_PublicadorId",
                table: "PublicadorPartes",
                newName: "IX_PublicadorPartes_PublicadorId");

            migrationBuilder.RenameIndex(
                name: "IX_IrmaoPartes_PublicadorAjudanteId",
                table: "PublicadorPartes",
                newName: "IX_PublicadorPartes_PublicadorAjudanteId");

            migrationBuilder.RenameIndex(
                name: "IX_IrmaoPartes_ParteId",
                table: "PublicadorPartes",
                newName: "IX_PublicadorPartes_ParteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publicadores",
                table: "Publicadores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicadorPartes",
                table: "PublicadorPartes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListasNegra_Publicadores_IrmaoId",
                table: "ListasNegra",
                column: "IrmaoId",
                principalTable: "Publicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListasNegra_Publicadores_PublicadorListaNegraId",
                table: "ListasNegra",
                column: "PublicadorListaNegraId",
                principalTable: "Publicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publicadores_Publicadores_MaeId",
                table: "Publicadores",
                column: "MaeId",
                principalTable: "Publicadores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicadores_Publicadores_PaiId",
                table: "Publicadores",
                column: "PaiId",
                principalTable: "Publicadores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicadorPartes_Partes_ParteId",
                table: "PublicadorPartes",
                column: "ParteId",
                principalTable: "Partes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicadorPartes_Publicadores_PublicadorAjudanteId",
                table: "PublicadorPartes",
                column: "PublicadorAjudanteId",
                principalTable: "Publicadores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicadorPartes_Publicadores_PublicadorId",
                table: "PublicadorPartes",
                column: "PublicadorId",
                principalTable: "Publicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListasNegra_Publicadores_IrmaoId",
                table: "ListasNegra");

            migrationBuilder.DropForeignKey(
                name: "FK_ListasNegra_Publicadores_PublicadorListaNegraId",
                table: "ListasNegra");

            migrationBuilder.DropForeignKey(
                name: "FK_Publicadores_Publicadores_MaeId",
                table: "Publicadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Publicadores_Publicadores_PaiId",
                table: "Publicadores");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicadorPartes_Partes_ParteId",
                table: "PublicadorPartes");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicadorPartes_Publicadores_PublicadorAjudanteId",
                table: "PublicadorPartes");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicadorPartes_Publicadores_PublicadorId",
                table: "PublicadorPartes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicadorPartes",
                table: "PublicadorPartes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publicadores",
                table: "Publicadores");

            migrationBuilder.RenameTable(
                name: "PublicadorPartes",
                newName: "IrmaoPartes");

            migrationBuilder.RenameTable(
                name: "Publicadores",
                newName: "Irmaos");

            migrationBuilder.RenameIndex(
                name: "IX_PublicadorPartes_PublicadorId",
                table: "IrmaoPartes",
                newName: "IX_IrmaoPartes_PublicadorId");

            migrationBuilder.RenameIndex(
                name: "IX_PublicadorPartes_PublicadorAjudanteId",
                table: "IrmaoPartes",
                newName: "IX_IrmaoPartes_PublicadorAjudanteId");

            migrationBuilder.RenameIndex(
                name: "IX_PublicadorPartes_ParteId",
                table: "IrmaoPartes",
                newName: "IX_IrmaoPartes_ParteId");

            migrationBuilder.RenameIndex(
                name: "IX_Publicadores_PaiId",
                table: "Irmaos",
                newName: "IX_Irmaos_PaiId");

            migrationBuilder.RenameIndex(
                name: "IX_Publicadores_MaeId",
                table: "Irmaos",
                newName: "IX_Irmaos_MaeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IrmaoPartes",
                table: "IrmaoPartes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Irmaos",
                table: "Irmaos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IrmaoPartes_Irmaos_PublicadorAjudanteId",
                table: "IrmaoPartes",
                column: "PublicadorAjudanteId",
                principalTable: "Irmaos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IrmaoPartes_Irmaos_PublicadorId",
                table: "IrmaoPartes",
                column: "PublicadorId",
                principalTable: "Irmaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IrmaoPartes_Partes_ParteId",
                table: "IrmaoPartes",
                column: "ParteId",
                principalTable: "Partes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Irmaos_Irmaos_MaeId",
                table: "Irmaos",
                column: "MaeId",
                principalTable: "Irmaos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Irmaos_Irmaos_PaiId",
                table: "Irmaos",
                column: "PaiId",
                principalTable: "Irmaos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListasNegra_Irmaos_IrmaoId",
                table: "ListasNegra",
                column: "IrmaoId",
                principalTable: "Irmaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListasNegra_Irmaos_PublicadorListaNegraId",
                table: "ListasNegra",
                column: "PublicadorListaNegraId",
                principalTable: "Irmaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
