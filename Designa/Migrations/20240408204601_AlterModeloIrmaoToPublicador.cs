using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Designa.Migrations
{
    /// <inheritdoc />
    public partial class AlterModeloIrmaoToPublicador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IrmaoPartes_Irmaos_AjudanteId",
                table: "IrmaoPartes");

            migrationBuilder.DropForeignKey(
                name: "FK_Irmaos_Irmaos_MaeId",
                table: "Irmaos");

            migrationBuilder.DropForeignKey(
                name: "FK_Irmaos_Irmaos_PaiId",
                table: "Irmaos");

            migrationBuilder.DropForeignKey(
                name: "FK_ListasNegra_Irmaos_IrmaoListaNegraId",
                table: "ListasNegra");

            migrationBuilder.DropIndex(
                name: "IX_IrmaoPartes_AjudanteId",
                table: "IrmaoPartes");

            migrationBuilder.DropColumn(
                name: "AjudanteId",
                table: "IrmaoPartes");

            migrationBuilder.RenameColumn(
                name: "IrmaoListaNegraId",
                table: "ListasNegra",
                newName: "PublicadorListaNegraId");

            migrationBuilder.RenameIndex(
                name: "IX_ListasNegra_IrmaoListaNegraId",
                table: "ListasNegra",
                newName: "IX_ListasNegra_PublicadorListaNegraId");

            migrationBuilder.AddColumn<int>(
                name: "PublicadorId",
                table: "ListasNegra",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PaiId",
                table: "Irmaos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "MaeId",
                table: "Irmaos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "PublicadorAjudanteId",
                table: "IrmaoPartes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IrmaoPartes_PublicadorAjudanteId",
                table: "IrmaoPartes",
                column: "PublicadorAjudanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_IrmaoPartes_Irmaos_PublicadorAjudanteId",
                table: "IrmaoPartes",
                column: "PublicadorAjudanteId",
                principalTable: "Irmaos",
                principalColumn: "Id");

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
                name: "FK_ListasNegra_Irmaos_PublicadorListaNegraId",
                table: "ListasNegra",
                column: "PublicadorListaNegraId",
                principalTable: "Irmaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IrmaoPartes_Irmaos_PublicadorAjudanteId",
                table: "IrmaoPartes");

            migrationBuilder.DropForeignKey(
                name: "FK_Irmaos_Irmaos_MaeId",
                table: "Irmaos");

            migrationBuilder.DropForeignKey(
                name: "FK_Irmaos_Irmaos_PaiId",
                table: "Irmaos");

            migrationBuilder.DropForeignKey(
                name: "FK_ListasNegra_Irmaos_PublicadorListaNegraId",
                table: "ListasNegra");

            migrationBuilder.DropIndex(
                name: "IX_IrmaoPartes_PublicadorAjudanteId",
                table: "IrmaoPartes");

            migrationBuilder.DropColumn(
                name: "PublicadorId",
                table: "ListasNegra");

            migrationBuilder.DropColumn(
                name: "PublicadorAjudanteId",
                table: "IrmaoPartes");

            migrationBuilder.RenameColumn(
                name: "PublicadorListaNegraId",
                table: "ListasNegra",
                newName: "IrmaoListaNegraId");

            migrationBuilder.RenameIndex(
                name: "IX_ListasNegra_PublicadorListaNegraId",
                table: "ListasNegra",
                newName: "IX_ListasNegra_IrmaoListaNegraId");

            migrationBuilder.AlterColumn<int>(
                name: "PaiId",
                table: "Irmaos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaeId",
                table: "Irmaos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AjudanteId",
                table: "IrmaoPartes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IrmaoPartes_AjudanteId",
                table: "IrmaoPartes",
                column: "AjudanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_IrmaoPartes_Irmaos_AjudanteId",
                table: "IrmaoPartes",
                column: "AjudanteId",
                principalTable: "Irmaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Irmaos_Irmaos_MaeId",
                table: "Irmaos",
                column: "MaeId",
                principalTable: "Irmaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Irmaos_Irmaos_PaiId",
                table: "Irmaos",
                column: "PaiId",
                principalTable: "Irmaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListasNegra_Irmaos_IrmaoListaNegraId",
                table: "ListasNegra",
                column: "IrmaoListaNegraId",
                principalTable: "Irmaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
