using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bodegas.Server.Models;

public partial class DbAquacolorsContext : DbContext
{
    public DbAquacolorsContext()
    {
    }

    public DbAquacolorsContext(DbContextOptions<DbAquacolorsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BodegasA> BodegasAs { get; set; }

    public virtual DbSet<StockAqua> StockAquas { get; set; }

    public virtual DbSet<UsuariosA> UsuariosAs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BodegasA>(entity =>
        {
            entity.HasKey(e => e.IdBodega).HasName("PK__Bodega__5C0091B7A887274B");

            entity.ToTable("BodegasA");

            entity.Property(e => e.IdBodega).HasColumnName("idBodega");
            entity.Property(e => e.Bodega)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StockAqua>(entity =>
        {
            entity.HasKey(e => e.IdStock).HasName("PK__StockAqu__A4B76DE526326946");

            entity.ToTable("StockAqua");

            entity.Property(e => e.IdStock).HasColumnName("idStock");
            entity.Property(e => e.Accion)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Articulo)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Clave)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdBodega).HasColumnName("idBodega");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UsuariosA>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__3213E83F31A51043");

            entity.ToTable("UsuariosA");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
