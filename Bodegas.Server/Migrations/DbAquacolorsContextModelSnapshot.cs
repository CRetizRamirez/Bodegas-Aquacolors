﻿// <auto-generated />
using System;
using Bodegas.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bodegas.Server.Migrations
{
    [DbContext(typeof(DbAquacolorsContext))]
    partial class DbAquacolorsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bodegas.Server.Models.BodegasA", b =>
                {
                    b.Property<int>("IdBodega")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idBodega");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBodega"));

                    b.Property<string>("Bodega")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.HasKey("IdBodega")
                        .HasName("PK__Bodega__5C0091B7A887274B");

                    b.ToTable("BodegasA", (string)null);
                });

            modelBuilder.Entity("Bodegas.Server.Models.StockAqua", b =>
                {
                    b.Property<int>("IdStock")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idStock");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStock"));

                    b.Property<string>("Accion")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Articulo")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime");

                    b.Property<int>("IdBodega")
                        .HasColumnType("int")
                        .HasColumnName("idBodega");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("idUsuario");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.HasKey("IdStock")
                        .HasName("PK__StockAqu__A4B76DE526326946");

                    b.ToTable("StockAqua", (string)null);
                });

            modelBuilder.Entity("Bodegas.Server.Models.UsuariosA", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idUsuario");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.HasKey("IdUsuario")
                        .HasName("PK__Usuario__3213E83F31A51043");

                    b.ToTable("UsuariosA", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}