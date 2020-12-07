﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnboardingSIGDB1.Data._Contexto;

namespace OnboardingSIGDB1.Data._Migracoes
{
    [DbContext(typeof(OnboardingSIGDB1Context))]
    [Migration("20201207193706_VincularFuncionarioComCargo")]
    partial class VincularFuncionarioComCargo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Cargos.Entidades.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("Cargo");
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Empresas.Entidades.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14);

                    b.Property<DateTime?>("DataDeFundacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Funcionarios.Entidades.CargoDoFuncionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CargoId");

                    b.Property<DateTime>("DataDeVinculo");

                    b.Property<int>("FuncionarioId");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId");

                    b.HasIndex("CargoId", "FuncionarioId")
                        .IsUnique();

                    b.ToTable("CargoDoFuncionario");
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Funcionarios.Entidades.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<DateTime?>("DataDeContratacao");

                    b.Property<int?>("EmpresaId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Funcionarios.Entidades.CargoDoFuncionario", b =>
                {
                    b.HasOne("OnboardingSIGDB1.Domain.Cargos.Entidades.Cargo", "Cargo")
                        .WithMany("ListaDeFuncionarios")
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OnboardingSIGDB1.Domain.Funcionarios.Entidades.Funcionario", "Funcionario")
                        .WithMany("ListaDeCargos")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Funcionarios.Entidades.Funcionario", b =>
                {
                    b.HasOne("OnboardingSIGDB1.Domain.Empresas.Entidades.Empresa", "Empresa")
                        .WithMany("ListaDeFuncionarios")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
