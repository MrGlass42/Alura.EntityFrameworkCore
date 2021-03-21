using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Alura.Loja.Testes.ConsoleApp.Migrations
{
    public partial class Promocao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PromocaoId",
                table: "Produtos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Promocoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataTermino = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocoes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_PromocaoId",
                table: "Produtos",
                column: "PromocaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Promocoes_PromocaoId",
                table: "Produtos",
                column: "PromocaoId",
                principalTable: "Promocoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Promocoes_PromocaoId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Promocoes");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_PromocaoId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "PromocaoId",
                table: "Produtos");
        }
    }
}
