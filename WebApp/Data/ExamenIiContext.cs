using System;
using System.Collections.Generic;
using AplicacionWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AplicacionWeb.Data;

public partial class ExamenIiContext : DbContext
{
    public ExamenIiContext()
    {
    }

    public ExamenIiContext(DbContextOptions<ExamenIiContext> options): base(options)
    {
    }

    public virtual DbSet<LugarEvento> LugarEventos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LugarEvento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LugarEve__3213E83F082ACD07");

            entity.ToTable("LugarEvento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.Deshabilitado).HasColumnName("deshabilitado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Pais)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pais");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
