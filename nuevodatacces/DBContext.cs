using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace nuevodatacces
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SicmAlertaMisiones> SicmAlertaMisiones { get; set; }
        public virtual DbSet<SicmAlertas> SicmAlertas { get; set; }
        public virtual DbSet<SicmAvistamientos> SicmAvistamientos { get; set; }
        public virtual DbSet<SicmBoletines> SicmBoletines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DIIN-VESTRADA;Initial Catalog=CancilleriaGuatemalaDb2021; User ID=usr_cgdb;Password=Us3r_CgDb2019;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SicmAlertaMisiones>(entity =>
            {
                entity.HasKey(e => e.CodigoAlertaMision)
                    .HasName("PK_MDCG_TIPO_MISION");

                entity.ToTable("SICM_ALERTA_MISIONES");

                entity.Property(e => e.CodigoAlertaMision).HasColumnName("CODIGO_ALERTA_MISION");

                entity.Property(e => e.CodigoAlerta).HasColumnName("CODIGO_ALERTA");

                entity.Property(e => e.CodigoMision).HasColumnName("CODIGO_MISION");

                entity.HasOne(d => d.CodigoAlertaNavigation)
                    .WithMany(p => p.SicmAlertaMisiones)
                    .HasForeignKey(d => d.CodigoAlerta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICM_ALERTA_MISIONES_SICM_ALERTAS1");
            });

            modelBuilder.Entity<SicmAlertas>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_SICM_ALERTAS_1");

                entity.ToTable("SICM_ALERTAS");

                entity.Property(e => e.Codigo).HasColumnName("CODIGO");

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.ActualizadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoAlerta)
                    .IsRequired()
                    .HasColumnName("CODIGO_ALERTA")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoMunicipio).HasColumnName("CODIGO_MUNICIPIO");

                entity.Property(e => e.CodigoOficio)
                    .HasColumnName("CODIGO_OFICIO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoSituacion).HasColumnName("CODIGO_SITUACION");

                entity.Property(e => e.CodigoTipoAlerta).HasColumnName("CODIGO_TIPO_ALERTA");

                entity.Property(e => e.CodigoUsuario)
                    .HasColumnName("CODIGO_USUARIO")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.CreadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasColumnName("DIRECCION")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoAlerta).HasColumnName("ESTADO_ALERTA");

                entity.Property(e => e.FechaActivacion)
                    .HasColumnName("FECHA_ACTIVACION")
                    .HasColumnType("date");

                entity.Property(e => e.FechaDesactivacion)
                    .HasColumnName("FECHA_DESACTIVACION")
                    .HasColumnType("datetime");

                entity.Property(e => e.Observaciones)
                    .HasColumnName("OBSERVACIONES")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Oficio)
                    .HasColumnName("OFICIO")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SicmAvistamientos>(entity =>
            {
                entity.HasKey(e => e.CodigoAvistamiento)
                    .HasName("PK_AVISTAMIENTOS");

                entity.ToTable("SICM_AVISTAMIENTOS");

                entity.Property(e => e.CodigoAvistamiento).HasColumnName("CODIGO_AVISTAMIENTO");

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.ActualizadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Anonimo).HasColumnName("ANONIMO");

                entity.Property(e => e.CodigoAlerta).HasColumnName("CODIGO_ALERTA");

                entity.Property(e => e.CodigoMunicipio).HasColumnName("CODIGO_MUNICIPIO");

                entity.Property(e => e.CodigoUsuario)
                    .IsRequired()
                    .HasColumnName("CODIGO_USUARIO")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasColumnName("CORREO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.CreadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasColumnName("DIRECCION")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaHora)
                    .HasColumnName("FECHA_HORA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Motivo)
                    .IsRequired()
                    .HasColumnName("MOTIVO")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Observaciones)
                    .HasColumnName("OBSERVACIONES")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasColumnName("TELEFONO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodigoAlertaNavigation)
                    .WithMany(p => p.SicmAvistamientos)
                    .HasForeignKey(d => d.CodigoAlerta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICM_AVISTAMIENTOS_SICM_ALERTAS");
            });

            modelBuilder.Entity<SicmBoletines>(entity =>
            {
                entity.HasKey(e => e.CodigoBoletin);

                entity.ToTable("SICM_BOLETINES");

                entity.Property(e => e.CodigoBoletin).HasColumnName("CODIGO_BOLETIN");

                entity.Property(e => e.CodigoAlerta).HasColumnName("CODIGO_ALERTA");

                entity.Property(e => e.CodigoColorCabello).HasColumnName("CODIGO_COLOR_CABELLO");

                entity.Property(e => e.CodigoColorOjos).HasColumnName("CODIGO_COLOR_OJOS");

                entity.Property(e => e.CodigoComplexion).HasColumnName("CODIGO_COMPLEXION");

                entity.Property(e => e.CodigoTamanioCabello).HasColumnName("CODIGO_TAMANIO_CABELLO");

                entity.Property(e => e.CodigoTez).HasColumnName("CODIGO_TEZ");

                entity.Property(e => e.CodigoTipoCabello).HasColumnName("CODIGO_TIPO_CABELLO");

                entity.Property(e => e.CodigoTipoCeja).HasColumnName("CODIGO_TIPO_CEJA");

                entity.Property(e => e.CodigoTipoNariz).HasColumnName("CODIGO_TIPO_NARIZ");

                entity.Property(e => e.Edad).HasColumnName("EDAD");

                entity.Property(e => e.Estatura).HasColumnName("ESTATURA");

                entity.Property(e => e.FechaHoraDesaparicion)
                    .HasColumnName("FECHA_HORA_DESAPARICION")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("FECHA_NACIMIENTO")
                    .HasColumnType("date");

                entity.Property(e => e.Foto)
                    .HasColumnName("FOTO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Genero).HasColumnName("GENERO");

                entity.Property(e => e.Nacionalidad).HasColumnName("NACIONALIDAD");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observaciones)
                    .HasColumnName("OBSERVACIONES")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeniasParticulares)
                    .HasColumnName("SENIAS_PARTICULARES")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Vestimenta)
                    .HasColumnName("VESTIMENTA")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodigoAlertaNavigation)
                    .WithMany(p => p.SicmBoletines)
                    .HasForeignKey(d => d.CodigoAlerta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICM_BOLETINES_SICM_ALERTAS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
