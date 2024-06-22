﻿// <auto-generated />
using System;
using GestaoPortfolioInvestimento.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestaoPortfolioInvestimento.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240622174328_CriandoTabelas")]
    partial class CriandoTabelas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GestaoPortfolioInvestimento.Models.Cliente", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DataNascimento")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("GestaoPortfolioInvestimento.Models.Investimento", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ProdutoFinanceiroID")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorCompra")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("Vencimento")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("ClienteID");

                    b.HasIndex("ProdutoFinanceiroID");

                    b.ToTable("Investimentos");
                });

            modelBuilder.Entity("GestaoPortfolioInvestimento.Models.ProdutoFinanceiro", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("TaxaRetorno")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("ProdutosFinanceiros");
                });

            modelBuilder.Entity("GestaoPortfolioInvestimento.Models.Transacao", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("InvestimentoID")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("TipoTransacao")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("ID");

                    b.HasIndex("InvestimentoID");

                    b.ToTable("Transacoes");
                });

            modelBuilder.Entity("GestaoPortfolioInvestimento.Models.Investimento", b =>
                {
                    b.HasOne("GestaoPortfolioInvestimento.Models.Cliente", "Cliente")
                        .WithMany("Investimentos")
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestaoPortfolioInvestimento.Models.ProdutoFinanceiro", "ProdutoFinanceiro")
                        .WithMany("Investimentos")
                        .HasForeignKey("ProdutoFinanceiroID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("ProdutoFinanceiro");
                });

            modelBuilder.Entity("GestaoPortfolioInvestimento.Models.Transacao", b =>
                {
                    b.HasOne("GestaoPortfolioInvestimento.Models.Investimento", "Investimento")
                        .WithMany("Transacoes")
                        .HasForeignKey("InvestimentoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Investimento");
                });

            modelBuilder.Entity("GestaoPortfolioInvestimento.Models.Cliente", b =>
                {
                    b.Navigation("Investimentos");
                });

            modelBuilder.Entity("GestaoPortfolioInvestimento.Models.Investimento", b =>
                {
                    b.Navigation("Transacoes");
                });

            modelBuilder.Entity("GestaoPortfolioInvestimento.Models.ProdutoFinanceiro", b =>
                {
                    b.Navigation("Investimentos");
                });
#pragma warning restore 612, 618
        }
    }
}
