using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Designa.Migrations
{
    /// <inheritdoc />
    public partial class addPresidenteErespOracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PresidenteId",
                table: "Reunioes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicadorOracaoId",
                table: "Reunioes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reunioes_PresidenteId",
                table: "Reunioes",
                column: "PresidenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reunioes_PublicadorOracaoId",
                table: "Reunioes",
                column: "PublicadorOracaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reunioes_Publicadores_PresidenteId",
                table: "Reunioes",
                column: "PresidenteId",
                principalTable: "Publicadores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reunioes_Publicadores_PublicadorOracaoId",
                table: "Reunioes",
                column: "PublicadorOracaoId",
                principalTable: "Publicadores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reunioes_Publicadores_PresidenteId",
                table: "Reunioes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reunioes_Publicadores_PublicadorOracaoId",
                table: "Reunioes");

            migrationBuilder.DropIndex(
                name: "IX_Reunioes_PresidenteId",
                table: "Reunioes");

            migrationBuilder.DropIndex(
                name: "IX_Reunioes_PublicadorOracaoId",
                table: "Reunioes");

            migrationBuilder.DropColumn(
                name: "PresidenteId",
                table: "Reunioes");

            migrationBuilder.DropColumn(
                name: "PublicadorOracaoId",
                table: "Reunioes");
        }
    }
}
