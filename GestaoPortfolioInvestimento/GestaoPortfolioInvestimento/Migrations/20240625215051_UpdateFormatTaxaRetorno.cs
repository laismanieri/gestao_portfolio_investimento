using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoPortfolioInvestimento.Migrations
{
    public partial class UpdateFormatTaxaRetorno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TaxaRetorno",
                table: "ProdutosFinanceiros",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TaxaRetorno",
                table: "ProdutosFinanceiros",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
