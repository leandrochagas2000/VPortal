using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VPortal.Data.Migrations
{
    public partial class Atualizacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Contas_FornecedorId",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "FornecedorId",
                table: "Produtos",
                newName: "ContaId");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_FornecedorId",
                table: "Produtos",
                newName: "IX_Produtos_ContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Contas_ContaId",
                table: "Produtos",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Contas_ContaId",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "ContaId",
                table: "Produtos",
                newName: "FornecedorId");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_ContaId",
                table: "Produtos",
                newName: "IX_Produtos_FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Contas_FornecedorId",
                table: "Produtos",
                column: "FornecedorId",
                principalTable: "Contas",
                principalColumn: "Id");
        }
    }
}
