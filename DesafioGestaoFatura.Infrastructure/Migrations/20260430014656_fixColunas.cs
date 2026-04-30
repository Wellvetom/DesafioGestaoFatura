using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioGestaoFatura.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixColunas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Faturas_FaturaId",
                table: "Itens");

            migrationBuilder.AlterColumn<Guid>(
                name: "FaturaId",
                table: "Itens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Faturas_FaturaId",
                table: "Itens",
                column: "FaturaId",
                principalTable: "Faturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Faturas_FaturaId",
                table: "Itens");

            migrationBuilder.AlterColumn<Guid>(
                name: "FaturaId",
                table: "Itens",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Faturas_FaturaId",
                table: "Itens",
                column: "FaturaId",
                principalTable: "Faturas",
                principalColumn: "Id");
        }
    }
}
