﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Data;

#nullable disable

namespace Store.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20240521205347_Initial1")]
    partial class Initial1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Store.Models.Avtor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DatumRagjanje")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nacionalnost")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Pol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Avtor");
                });

            modelBuilder.Entity("Store.Models.AvtorKniga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvtorId")
                        .HasColumnType("int");

                    b.Property<int>("KnigaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AvtorId");

                    b.HasIndex("KnigaId");

                    b.ToTable("AvtorKniga");
                });

            modelBuilder.Entity("Store.Models.Kniga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BrStrani")
                        .HasColumnType("int");

                    b.Property<int?>("Godina")
                        .HasColumnType("int");

                    b.Property<string>("Izdavac")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Opis")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SlikaUrl")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Tirazh")
                        .HasColumnType("int");

                    b.Property<string>("Zanr")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Kniga");
                });

            modelBuilder.Entity("Store.Models.AvtorKniga", b =>
                {
                    b.HasOne("Store.Models.Avtor", "Avtor")
                        .WithMany("Knigi")
                        .HasForeignKey("AvtorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Store.Models.Kniga", "Kniga")
                        .WithMany("Avtori")
                        .HasForeignKey("KnigaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Avtor");

                    b.Navigation("Kniga");
                });

            modelBuilder.Entity("Store.Models.Avtor", b =>
                {
                    b.Navigation("Knigi");
                });

            modelBuilder.Entity("Store.Models.Kniga", b =>
                {
                    b.Navigation("Avtori");
                });
#pragma warning restore 612, 618
        }
    }
}