﻿// <auto-generated />
using System;
using Designa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Designa.Migrations
{
    [DbContext(typeof(DesignaContext))]
    partial class DesignaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.17");

            modelBuilder.Entity("Designa.Models.Irmao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Irmaos");
                });

            modelBuilder.Entity("Designa.Models.Parte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Minutos")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Numero")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ReuniaoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ReuniaoId");

                    b.ToTable("Partes");
                });

            modelBuilder.Entity("Designa.Models.Reuniao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Issue")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Semana")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Reunioes");
                });

            modelBuilder.Entity("Designa.Models.Parte", b =>
                {
                    b.HasOne("Designa.Models.Reuniao", null)
                        .WithMany("Partes")
                        .HasForeignKey("ReuniaoId");
                });

            modelBuilder.Entity("Designa.Models.Reuniao", b =>
                {
                    b.Navigation("Partes");
                });
#pragma warning restore 612, 618
        }
    }
}
