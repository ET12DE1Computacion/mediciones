using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VisorWeb.Models
{
    public partial class MedicionesContext : DbContext
    {
        public MedicionesContext()
        {
        }

        public MedicionesContext(DbContextOptions<MedicionesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Informe> Informe { get; set; }
        public virtual DbSet<Medicion> Medicion { get; set; }
        public virtual DbSet<Tipomedicion> Tipomedicion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Informe>(entity =>
            {
                entity.HasKey(e => e.IdInforme);

                entity.ToTable("informe", "mediciones");

                entity.HasIndex(e => e.IdTipoMedicion)
                    .HasName("fk_Informe_TipoMedicion1_idx");

                entity.Property(e => e.IdInforme)
                    .HasColumnName("idInforme")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.IdTipoMedicion)
                    .HasColumnName("idTipoMedicion")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Maximo).HasColumnName("maximo");

                entity.Property(e => e.Minimo).HasColumnName("minimo");

                entity.Property(e => e.Promedio).HasColumnName("promedio");

                entity.HasOne(d => d.IdTipoMedicionNavigation)
                    .WithMany(p => p.Informe)
                    .HasForeignKey(d => d.IdTipoMedicion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Informe_TipoMedicion1");
            });

            modelBuilder.Entity<Medicion>(entity =>
            {
                entity.HasKey(e => e.IdMedicion);

                entity.ToTable("medicion", "mediciones");

                entity.HasIndex(e => e.IdTipoMedicion)
                    .HasName("fk_Medicion_TipoMedicion_idx");

                entity.Property(e => e.IdMedicion)
                    .HasColumnName("idMedicion")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.FechaHora).HasColumnName("fechaHora");

                entity.Property(e => e.IdTipoMedicion)
                    .HasColumnName("idTipoMedicion")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Valor).HasColumnName("valor");

                entity.HasOne(d => d.IdTipoMedicionNavigation)
                    .WithMany(p => p.Medicion)
                    .HasForeignKey(d => d.IdTipoMedicion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Medicion_TipoMedicion");
            });

            modelBuilder.Entity<Tipomedicion>(entity =>
            {
                entity.HasKey(e => e.IdTipoMedicion);

                entity.ToTable("tipomedicion", "mediciones");

                entity.HasIndex(e => e.TipoMedicion1)
                    .HasName("tipoMedicion_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdTipoMedicion)
                    .HasColumnName("idTipoMedicion")
                    .HasColumnType("tinyint(3) unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TipoMedicion1)
                    .IsRequired()
                    .HasColumnName("tipoMedicion")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });
        }
    }
}
