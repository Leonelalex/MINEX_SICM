using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccesCore
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

        public virtual DbSet<GlobCiudad> GlobCiudad { get; set; }
        public virtual DbSet<GlobDivision> GlobDivision { get; set; }
        public virtual DbSet<GlobGenero> GlobGenero { get; set; }
        public virtual DbSet<GlobMisionesExterior> GlobMisionesExterior { get; set; }
        public virtual DbSet<GlobPais> GlobPais { get; set; }
        public virtual DbSet<GlobTipoMision> GlobTipoMision { get; set; }
        public virtual DbSet<SicmAccionesAlerta> SicmAccionesAlerta { get; set; }
        public virtual DbSet<SicmAlertaMisiones> SicmAlertaMisiones { get; set; }
        public virtual DbSet<SicmAlertas> SicmAlertas { get; set; }
        public virtual DbSet<SicmAvistamientos> SicmAvistamientos { get; set; }
        public virtual DbSet<SicmBitacoraAlerta> SicmBitacoraAlerta { get; set; }
        public virtual DbSet<SicmColorCabello> SicmColorCabello { get; set; }
        public virtual DbSet<SicmColorOjos> SicmColorOjos { get; set; }
        public virtual DbSet<SicmComplexion> SicmComplexion { get; set; }
        public virtual DbSet<SicmEstadosAlerta> SicmEstadosAlerta { get; set; }
        public virtual DbSet<SicmSituacionAlerta> SicmSituacionAlerta { get; set; }
        public virtual DbSet<SicmTamanioCabello> SicmTamanioCabello { get; set; }
        public virtual DbSet<SicmTez> SicmTez { get; set; }
        public virtual DbSet<SicmTipoAlerta> SicmTipoAlerta { get; set; }
        public virtual DbSet<SicmTipoCabello> SicmTipoCabello { get; set; }
        public virtual DbSet<SicmTipoCeja> SicmTipoCeja { get; set; }
        public virtual DbSet<SicmTipoNariz> SicmTipoNariz { get; set; }

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
            modelBuilder.Entity<GlobCiudad>(entity =>
            {
                entity.HasKey(e => e.CodigoCiudad)
                    .HasName("PK__GLOB_CIUDAD__30F848ED");

                entity.ToTable("GLOB_CIUDAD", "glob");

                entity.HasComment("Almacena las ciudades");

                entity.Property(e => e.CodigoCiudad)
                    .HasColumnName("CODIGO_CIUDAD")
                    .HasComment("Codigo correlativo unico para cada registro");

                entity.Property(e => e.CodigoDivision)
                    .HasColumnName("CODIGO_DIVISION")
                    .HasComment("Almacena la division");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("Almacena la descripcion");

                entity.Property(e => e.HusoHorario)
                    .HasColumnName("HUSO_HORARIO")
                    .HasComment("Almacena el huso del horario");

                entity.HasOne(d => d.CodigoDivisionNavigation)
                    .WithMany(p => p.GlobCiudad)
                    .HasForeignKey(d => d.CodigoDivision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GLOB_CIUDAD__GLOB_DIVISION");
            });

            modelBuilder.Entity<GlobDivision>(entity =>
            {
                entity.HasKey(e => e.CodigoDivision)
                    .HasName("PK__GLOB_DIVISION__1CF15040");

                entity.ToTable("GLOB_DIVISION", "glob");

                entity.HasComment("Almacena las divisiones");

                entity.Property(e => e.CodigoDivision)
                    .HasColumnName("CODIGO_DIVISION")
                    .HasComment("Codigo correlativo unico para cada registro");

                entity.Property(e => e.CodigoPais)
                    .HasColumnName("CODIGO_PAIS")
                    .HasComment("Almacena el pais");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("Almacena la descripcion");

                entity.HasOne(d => d.CodigoPaisNavigation)
                    .WithMany(p => p.GlobDivision)
                    .HasForeignKey(d => d.CodigoPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GLOB_DIVISION__FK_GLOB_PAIS");
            });

            modelBuilder.Entity<GlobGenero>(entity =>
            {
                entity.HasKey(e => e.CodigoGenero)
                    .HasName("PK__GLOB_GENERO__153B1FDF");

                entity.ToTable("GLOB_GENERO", "glob");

                entity.HasComment("Almacena los generos");

                entity.Property(e => e.CodigoGenero)
                    .HasColumnName("CODIGO_GENERO")
                    .HasComment("Codigo correlativo unico para cada registro");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("Almacena la descripcion");
            });

            modelBuilder.Entity<GlobMisionesExterior>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GLOB_MISIONES_EXTERIOR", "glob");

                entity.Property(e => e.CircunscripcionSeccionConsular)
                    .HasColumnName("CIRCUNSCRIPCION_SECCION_CONSULAR")
                    .HasMaxLength(500);

                entity.Property(e => e.CodigoCiudad).HasColumnName("CODIGO_CIUDAD");

                entity.Property(e => e.CodigoDepartamento).HasColumnName("CODIGO_DEPARTAMENTO");

                entity.Property(e => e.CodigoPais).HasColumnName("CODIGO_PAIS");

                entity.Property(e => e.CodigoTipoMision).HasColumnName("CODIGO_TIPO_MISION");

                entity.Property(e => e.CorreoElectronico)
                    .HasColumnName("CORREO_ELECTRONICO")
                    .HasMaxLength(150);

                entity.Property(e => e.Direccion)
                    .HasColumnName("DIRECCION")
                    .HasMaxLength(250);

                entity.Property(e => e.Fax)
                    .HasColumnName("FAX")
                    .HasMaxLength(150);

                entity.Property(e => e.FechaAcreditacion)
                    .HasColumnName("FECHA_ACREDITACION")
                    .HasColumnType("datetime");

                entity.Property(e => e.FiestaNacional)
                    .HasColumnName("FIESTA_NACIONAL")
                    .HasMaxLength(250);

                entity.Property(e => e.Horario)
                    .HasColumnName("HORARIO")
                    .HasMaxLength(150);

                entity.Property(e => e.IdMisionExterior).HasColumnName("ID_MISION_EXTERIOR");

                entity.Property(e => e.Moneda).HasColumnName("MONEDA");

                entity.Property(e => e.NombreMision)
                    .IsRequired()
                    .HasColumnName("NOMBRE_MISION")
                    .HasMaxLength(250);

                entity.Property(e => e.NombreMisionTransferencia)
                    .IsRequired()
                    .HasColumnName("NOMBRE_MISION_TRANSFERENCIA")
                    .HasMaxLength(250);

                entity.Property(e => e.Notas)
                    .HasColumnName("NOTAS")
                    .HasMaxLength(250);

                entity.Property(e => e.SitioWeb)
                    .HasColumnName("SITIO_WEB")
                    .HasMaxLength(250);

                entity.Property(e => e.Telefono)
                    .HasColumnName("TELEFONO")
                    .HasMaxLength(150);

                entity.Property(e => e.Zip)
                    .HasColumnName("ZIP")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.CodigoTipoMisionNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodigoTipoMision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GLOB_MISIONES_EXTERIOR_GLOB_TIPO_MISION");

                entity.HasOne(d => d.IdMisionExteriorNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMisionExterior)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GLOB_MISIONES_EXTERIOR_SICM_ALERTA_MISIONES");
            });

            modelBuilder.Entity<GlobPais>(entity =>
            {
                entity.HasKey(e => e.CodigoPais)
                    .HasName("PK__GLOB_PAIS__2E1BDC42");

                entity.ToTable("GLOB_PAIS", "glob");

                entity.HasComment("Almacena los paises");

                entity.Property(e => e.CodigoPais)
                    .HasColumnName("CODIGO_PAIS")
                    .HasComment("Codigo correlativo unico para cada pais");

                entity.Property(e => e.CategoriaOficial)
                    .IsRequired()
                    .HasColumnName("CATEGORIA_OFICIAL")
                    .HasMaxLength(1)
                    .HasComment("Almacena la categoria oficial");

                entity.Property(e => e.CategoriaOrdinaria)
                    .IsRequired()
                    .HasColumnName("CATEGORIA_ORDINARIA")
                    .HasMaxLength(1)
                    .HasComment("Almacena la categoria ordinaria");

                entity.Property(e => e.CodigoIsoAlpha3)
                    .IsRequired()
                    .HasColumnName("CODIGO_ISO_ALPHA3")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Almacena el codigo de iso");

                entity.Property(e => e.CodigoMoneda)
                    .HasColumnName("CODIGO_MONEDA")
                    .HasComment("Almacena el codigo de moneda");

                entity.Property(e => e.CodigoRegion)
                    .HasColumnName("CODIGO_REGION")
                    .HasComment("Almacena el codigo de region");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("Almacena la descripcion");

                entity.Property(e => e.FechaInicioRelaciones)
                    .HasColumnName("FECHA_INICIO_RELACIONES")
                    .HasColumnType("datetime")
                    .HasComment("Almacena la fecha de inicio de relaciones");

                entity.Property(e => e.HtmlReloj)
                    .HasColumnName("HTML_RELOJ")
                    .HasColumnType("text");

                entity.Property(e => e.HusoHorario)
                    .HasColumnName("HUSO_HORARIO")
                    .HasComment("Almacena el huso de horari");

                entity.Property(e => e.ImgBandera)
                    .HasColumnName("IMG_BANDERA")
                    .HasColumnType("image")
                    .HasComment("Almacena la bandera");

                entity.Property(e => e.ImgMapa)
                    .HasColumnName("IMG_MAPA")
                    .HasColumnType("image");

                entity.Property(e => e.Nacionalidad)
                    .HasColumnName("NACIONALIDAD")
                    .HasMaxLength(250)
                    .HasComment("Almacena la nacionalidad");
            });

            modelBuilder.Entity<GlobTipoMision>(entity =>
            {
                entity.HasKey(e => e.CodigoTipoMision)
                    .HasName("PK_MDCG_TIPO_MISION");

                entity.ToTable("GLOB_TIPO_MISION", "glob");

                entity.Property(e => e.CodigoTipoMision).HasColumnName("CODIGO_TIPO_MISION");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Eliminado).HasColumnName("ELIMINADO");

                entity.Property(e => e.FechaEliminacion)
                    .HasColumnName("FECHA_ELIMINACION")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnName("FECHA_MODIFICACION")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("FECHA_REGISTRO")
                    .HasColumnType("datetime");

                entity.Property(e => e.NombreTipoMision)
                    .IsRequired()
                    .HasColumnName("NOMBRE_TIPO_MISION")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioElimina)
                    .HasColumnName("USUARIO_ELIMINA")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioModifica)
                    .HasColumnName("USUARIO_MODIFICA")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioRegistra)
                    .HasColumnName("USUARIO_REGISTRA")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SicmAccionesAlerta>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_SCIM_ACCIONES_ALERTA");

                entity.ToTable("SICM_ACCIONES_ALERTA");

                entity.Property(e => e.Codigo).HasColumnName("CODIGO");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

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
                    .HasConstraintName("FK_SICM_ALERTA_MISIONES_SICM_ALERTAS");
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

                entity.Property(e => e.CodigoColorCabello).HasColumnName("CODIGO_COLOR_CABELLO");

                entity.Property(e => e.CodigoColorOjos).HasColumnName("CODIGO_COLOR_OJOS");

                entity.Property(e => e.CodigoComplexion).HasColumnName("CODIGO_COMPLEXION");

                entity.Property(e => e.CodigoMunicipio).HasColumnName("CODIGO_MUNICIPIO");

                entity.Property(e => e.CodigoPais).HasColumnName("CODIGO_PAIS");

                entity.Property(e => e.CodigoSituacion).HasColumnName("CODIGO_SITUACION");

                entity.Property(e => e.CodigoTamanioCabello).HasColumnName("CODIGO_TAMANIO_CABELLO");

                entity.Property(e => e.CodigoTez).HasColumnName("CODIGO_TEZ");

                entity.Property(e => e.CodigoTipoAlerta).HasColumnName("CODIGO_TIPO_ALERTA");

                entity.Property(e => e.CodigoTipoCabello).HasColumnName("CODIGO_TIPO_CABELLO");

                entity.Property(e => e.CodigoTipoCeja).HasColumnName("CODIGO_TIPO_CEJA");

                entity.Property(e => e.CodigoTipoNariz).HasColumnName("CODIGO_TIPO_NARIZ");

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

                entity.Property(e => e.Edad).HasColumnName("EDAD");

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoAlerta).HasColumnName("ESTADO_ALERTA");

                entity.Property(e => e.Estatura).HasColumnName("ESTATURA");

                entity.Property(e => e.FechaActivacion)
                    .HasColumnName("FECHA_ACTIVACION")
                    .HasColumnType("date");

                entity.Property(e => e.FechaHora)
                    .HasColumnName("FECHA_HORA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Foto)
                    .HasColumnName("FOTO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Genero).HasColumnName("GENERO");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observaciones)
                    .HasColumnName("OBSERVACIONES")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Oficio)
                    .HasColumnName("OFICIO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SeniasParticulares)
                    .HasColumnName("SENIAS_PARTICULARES")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Vestimenta)
                    .HasColumnName("VESTIMENTA")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodigoColorCabelloNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.CodigoColorCabello)
                    .HasConstraintName("FK_SICM_ALERTAS_SICM_COLOR_CABELLO");

                entity.HasOne(d => d.CodigoColorOjosNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.CodigoColorOjos)
                    .HasConstraintName("FK_SICM_ALERTAS_SICM_COLOR_OJOS");

                entity.HasOne(d => d.CodigoComplexionNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.CodigoComplexion)
                    .HasConstraintName("FK_SICM_ALERTAS_SICM_COMPLEXION");

                entity.HasOne(d => d.CodigoMunicipioNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.CodigoMunicipio)
                    .HasConstraintName("FK_SICM_ALERTAS_GLOB_CIUDAD");

                entity.HasOne(d => d.CodigoSituacionNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.CodigoSituacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICM_ALERTAS_SICM_SITUACION_ALERTA");

                entity.HasOne(d => d.CodigoTamanioCabelloNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.CodigoTamanioCabello)
                    .HasConstraintName("FK_SICM_ALERTAS_SICM_TAMANIO_CABELLO");

                entity.HasOne(d => d.CodigoTezNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.CodigoTez)
                    .HasConstraintName("FK_SICM_ALERTAS_SICM_TEZ");

                entity.HasOne(d => d.CodigoTipoAlertaNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.CodigoTipoAlerta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICM_ALERTAS_SICM_TIPO_ALERTA");

                entity.HasOne(d => d.CodigoTipoCabelloNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.CodigoTipoCabello)
                    .HasConstraintName("FK_SICM_ALERTAS_SICM_TIPO_CABELLO");

                entity.HasOne(d => d.CodigoTipoCejaNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.CodigoTipoCeja)
                    .HasConstraintName("FK_SICM_ALERTAS_SICM_TIPO_CEJA");

                entity.HasOne(d => d.CodigoTipoNarizNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.CodigoTipoNariz)
                    .HasConstraintName("FK_SICM_ALERTAS_SICM_TIPO_NARIZ");

                entity.HasOne(d => d.EstadoAlertaNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.EstadoAlerta)
                    .HasConstraintName("FK_SICM_ALERTAS_SICM_ESTADOS_ALERTA");

                entity.HasOne(d => d.GeneroNavigation)
                    .WithMany(p => p.SicmAlertas)
                    .HasForeignKey(d => d.Genero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICM_ALERTAS_GLOB_GENERO");
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

                entity.HasOne(d => d.CodigoMunicipioNavigation)
                    .WithMany(p => p.SicmAvistamientos)
                    .HasForeignKey(d => d.CodigoMunicipio)
                    .HasConstraintName("FK_SICM_AVISTAMIENTOS_GLOB_CIUDAD");
            });

            modelBuilder.Entity<SicmBitacoraAlerta>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("SICM_BITACORA_ALERTA");

                entity.Property(e => e.Codigo)
                    .HasColumnName("CODIGO")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

                entity.Property(e => e.ActualizadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Adjunto)
                    .HasColumnName("ADJUNTO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoAccion).HasColumnName("CODIGO_ACCION");

                entity.Property(e => e.CodigoAlerta).HasColumnName("CODIGO_ALERTA");

                entity.Property(e => e.CodigoEstado).HasColumnName("CODIGO_ESTADO");

                entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

                entity.Property(e => e.CreadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

                entity.Property(e => e.EliminadoPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasColumnName("FECHA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Observaciones)
                    .HasColumnName("OBSERVACIONES")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodigoAccionNavigation)
                    .WithMany(p => p.SicmBitacoraAlerta)
                    .HasForeignKey(d => d.CodigoAccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICM_BITACORA_ALERTA_SCIM_ACCIONES_ALERTA");

                entity.HasOne(d => d.CodigoAlertaNavigation)
                    .WithMany(p => p.SicmBitacoraAlerta)
                    .HasForeignKey(d => d.CodigoAlerta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICM_BITACORA_ALERTA_SICM_ALERTAS");

                entity.HasOne(d => d.CodigoEstadoNavigation)
                    .WithMany(p => p.SicmBitacoraAlerta)
                    .HasForeignKey(d => d.CodigoEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICM_BITACORA_ALERTA_SICM_ESTADOS_ALERTA");
            });

            modelBuilder.Entity<SicmColorCabello>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__SICM_COL__CC87E127ACE42C8A");

                entity.ToTable("SICM_COLOR_CABELLO");

                entity.Property(e => e.Codigo)
                    .HasColumnName("CODIGO")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SicmColorOjos>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__SICM_COL__CC87E127B0B7F049");

                entity.ToTable("SICM_COLOR_OJOS");

                entity.Property(e => e.Codigo)
                    .HasColumnName("CODIGO")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SicmComplexion>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__SICM_COM__CC87E1279C4101DE");

                entity.ToTable("SICM_COMPLEXION");

                entity.Property(e => e.Codigo)
                    .HasColumnName("CODIGO")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SicmEstadosAlerta>(entity =>
            {
                entity.HasKey(e => e.CodigoEstado);

                entity.ToTable("SICM_ESTADOS_ALERTA");

                entity.Property(e => e.CodigoEstado)
                    .HasColumnName("CODIGO_ESTADO")
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SicmSituacionAlerta>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("SICM_SITUACION_ALERTA");

                entity.Property(e => e.Codigo).HasColumnName("CODIGO");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SicmTamanioCabello>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__SICM_TAM__CC87E127D83D80E1");

                entity.ToTable("SICM_TAMANIO_CABELLO");

                entity.Property(e => e.Codigo)
                    .HasColumnName("CODIGO")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SicmTez>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__SICM_TEZ__CC87E12782733D1B");

                entity.ToTable("SICM_TEZ");

                entity.Property(e => e.Codigo)
                    .HasColumnName("CODIGO")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SicmTipoAlerta>(entity =>
            {
                entity.HasKey(e => e.CodigoTipoAlerta);

                entity.ToTable("SICM_TIPO_ALERTA");

                entity.Property(e => e.CodigoTipoAlerta)
                    .HasColumnName("CODIGO_TIPO_ALERTA")
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SicmTipoCabello>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__SICM_TIP__CC87E127A08BB119");

                entity.ToTable("SICM_TIPO_CABELLO");

                entity.Property(e => e.Codigo)
                    .HasColumnName("CODIGO")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SicmTipoCeja>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__SICM_TIP__CC87E12728DBFC04");

                entity.ToTable("SICM_TIPO_CEJA");

                entity.Property(e => e.Codigo)
                    .HasColumnName("CODIGO")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SicmTipoNariz>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__SICM_TIP__CC87E127746965D8");

                entity.ToTable("SICM_TIPO_NARIZ");

                entity.Property(e => e.Codigo)
                    .HasColumnName("CODIGO")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
