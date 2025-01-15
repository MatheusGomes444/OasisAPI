﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OasisApi.Data;

#nullable disable

namespace Oasis_API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241205012210_Create")]
    partial class Create
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Morador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlojamentoId")
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("CPF")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("Datanascimento")
                        .HasColumnType("int");

                    b.Property<string>("Endereco")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Idade")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<string>("Nacionalidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Observacoes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RG")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<int>("Telefone")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlojamentoId");

                    b.ToTable("Moradores", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AlojamentoId = 1,
                            Ativo = true,
                            CPF = "12345678900",
                            Datanascimento = 0,
                            Endereco = "Rua Alcântara, 113",
                            Idade = 18,
                            Nacionalidade = "Brasileiro",
                            Nome = "Pedro",
                            Observacoes = "Sem observações",
                            RG = "603456789",
                            Telefone = 912345678
                        });
                });

            modelBuilder.Entity("OasisApi.Models.Alojamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CapacidadeMaxima")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Equipe")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Pertences")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Refeicoes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sexo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefone")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Alojamentos", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CapacidadeMaxima = 10,
                            Email = "exemplo1@dominio.com",
                            Equipe = "Equipe A",
                            Nome = "Albergue Vila Maria",
                            Pertences = "Roupas e sapatos",
                            Pet = "Apenas cães",
                            Refeicoes = "Café e janta",
                            Sexo = "Masculino",
                            Telefone = 123456789
                        });
                });

            modelBuilder.Entity("OasisApi.Models.FilaDeEspera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlojamentoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<int>("MoradorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlojamentoId");

                    b.HasIndex("MoradorId");

                    b.ToTable("FilasDeEspera", (string)null);
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Morador", b =>
                {
                    b.HasOne("OasisApi.Models.Alojamento", "Alojamento")
                        .WithMany("Moradores")
                        .HasForeignKey("AlojamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alojamento");
                });

            modelBuilder.Entity("OasisApi.Models.FilaDeEspera", b =>
                {
                    b.HasOne("OasisApi.Models.Alojamento", "Alojamento")
                        .WithMany()
                        .HasForeignKey("AlojamentoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Morador", "Morador")
                        .WithMany()
                        .HasForeignKey("MoradorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Alojamento");

                    b.Navigation("Morador");
                });

            modelBuilder.Entity("OasisApi.Models.Alojamento", b =>
                {
                    b.Navigation("Moradores");
                });
#pragma warning restore 612, 618
        }
    }
}
