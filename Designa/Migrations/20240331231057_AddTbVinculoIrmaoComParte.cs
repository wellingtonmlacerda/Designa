using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Designa.Migrations
{
    /// <inheritdoc />
    public partial class AddTbVinculoIrmaoComParte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partes_Reunioes_ReuniaoId",
                table: "Partes");

            migrationBuilder.AlterColumn<int>(
                name: "ReuniaoId",
                table: "Partes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "IrmaoParte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParteId = table.Column<int>(type: "INTEGER", nullable: false),
                    IrmaoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IrmaoParte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IrmaoParte_Irmaos_IrmaoId",
                        column: x => x.IrmaoId,
                        principalTable: "Irmaos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IrmaoParte_Partes_ParteId",
                        column: x => x.ParteId,
                        principalTable: "Partes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IrmaoParte_IrmaoId",
                table: "IrmaoParte",
                column: "IrmaoId");

            migrationBuilder.CreateIndex(
                name: "IX_IrmaoParte_ParteId",
                table: "IrmaoParte",
                column: "ParteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partes_Reunioes_ReuniaoId",
                table: "Partes",
                column: "ReuniaoId",
                principalTable: "Reunioes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partes_Reunioes_ReuniaoId",
                table: "Partes");

            migrationBuilder.DropTable(
                name: "IrmaoParte");

            migrationBuilder.AlterColumn<int>(
                name: "ReuniaoId",
                table: "Partes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Partes_Reunioes_ReuniaoId",
                table: "Partes",
                column: "ReuniaoId",
                principalTable: "Reunioes",
                principalColumn: "Id");
        }
    }
}
