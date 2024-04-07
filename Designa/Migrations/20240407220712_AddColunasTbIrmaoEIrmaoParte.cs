using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Designa.Migrations
{
    /// <inheritdoc />
    public partial class AddColunasTbIrmaoEIrmaoParte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IrmaoParte_Irmaos_IrmaoId",
                table: "IrmaoParte");

            migrationBuilder.DropForeignKey(
                name: "FK_IrmaoParte_Partes_ParteId",
                table: "IrmaoParte");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IrmaoParte",
                table: "IrmaoParte");

            migrationBuilder.RenameTable(
                name: "IrmaoParte",
                newName: "IrmaoPartes");

            migrationBuilder.RenameColumn(
                name: "IrmaoId",
                table: "IrmaoPartes",
                newName: "PublicadorId");

            migrationBuilder.RenameIndex(
                name: "IX_IrmaoParte_ParteId",
                table: "IrmaoPartes",
                newName: "IX_IrmaoPartes_ParteId");

            migrationBuilder.RenameIndex(
                name: "IX_IrmaoParte_IrmaoId",
                table: "IrmaoPartes",
                newName: "IX_IrmaoPartes_PublicadorId");

            migrationBuilder.AddColumn<byte>(
                name: "EMenorIdade",
                table: "Irmaos",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "MaeId",
                table: "Irmaos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaiId",
                table: "Irmaos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Irmaos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AjudanteId",
                table: "IrmaoPartes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "IrmaoPartes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_IrmaoPartes",
                table: "IrmaoPartes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ListasNegra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IrmaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    IrmaoListaNegraId = table.Column<int>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasNegra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListasNegra_Irmaos_IrmaoId",
                        column: x => x.IrmaoId,
                        principalTable: "Irmaos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListasNegra_Irmaos_IrmaoListaNegraId",
                        column: x => x.IrmaoListaNegraId,
                        principalTable: "Irmaos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Irmaos_MaeId",
                table: "Irmaos",
                column: "MaeId");

            migrationBuilder.CreateIndex(
                name: "IX_Irmaos_PaiId",
                table: "Irmaos",
                column: "PaiId");

            migrationBuilder.CreateIndex(
                name: "IX_IrmaoPartes_AjudanteId",
                table: "IrmaoPartes",
                column: "AjudanteId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasNegra_IrmaoId",
                table: "ListasNegra",
                column: "IrmaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasNegra_IrmaoListaNegraId",
                table: "ListasNegra",
                column: "IrmaoListaNegraId");

            migrationBuilder.AddForeignKey(
                name: "FK_IrmaoPartes_Irmaos_AjudanteId",
                table: "IrmaoPartes",
                column: "AjudanteId",
                principalTable: "Irmaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Irmaos_Irmaos_PaiId",
                table: "Irmaos",
                column: "PaiId",
                principalTable: "Irmaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IrmaoPartes_Irmaos_AjudanteId",
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

            migrationBuilder.DropTable(
                name: "ListasNegra");

            migrationBuilder.DropIndex(
                name: "IX_Irmaos_MaeId",
                table: "Irmaos");

            migrationBuilder.DropIndex(
                name: "IX_Irmaos_PaiId",
                table: "Irmaos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IrmaoPartes",
                table: "IrmaoPartes");

            migrationBuilder.DropIndex(
                name: "IX_IrmaoPartes_AjudanteId",
                table: "IrmaoPartes");

            migrationBuilder.DropColumn(
                name: "EMenorIdade",
                table: "Irmaos");

            migrationBuilder.DropColumn(
                name: "MaeId",
                table: "Irmaos");

            migrationBuilder.DropColumn(
                name: "PaiId",
                table: "Irmaos");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Irmaos");

            migrationBuilder.DropColumn(
                name: "AjudanteId",
                table: "IrmaoPartes");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "IrmaoPartes");

            migrationBuilder.RenameTable(
                name: "IrmaoPartes",
                newName: "IrmaoParte");

            migrationBuilder.RenameColumn(
                name: "PublicadorId",
                table: "IrmaoParte",
                newName: "IrmaoId");

            migrationBuilder.RenameIndex(
                name: "IX_IrmaoPartes_PublicadorId",
                table: "IrmaoParte",
                newName: "IX_IrmaoParte_IrmaoId");

            migrationBuilder.RenameIndex(
                name: "IX_IrmaoPartes_ParteId",
                table: "IrmaoParte",
                newName: "IX_IrmaoParte_ParteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IrmaoParte",
                table: "IrmaoParte",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IrmaoParte_Irmaos_IrmaoId",
                table: "IrmaoParte",
                column: "IrmaoId",
                principalTable: "Irmaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IrmaoParte_Partes_ParteId",
                table: "IrmaoParte",
                column: "ParteId",
                principalTable: "Partes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
