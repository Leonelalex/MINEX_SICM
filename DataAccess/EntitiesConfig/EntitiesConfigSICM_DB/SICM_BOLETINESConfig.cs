using DataAccess.Entities.SICM_DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class SICM_BOLETINESConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_BOLETINES> entity)
        {
            entity.HasKey(e => e.codigoBoletin)
                                .HasName("PK_SICM_BOLETINES");

            entity.ToTable("SICM_BOLETINES");

            entity.Property(e => e.codigoBoletin).HasColumnName("CODIGO_BOLETIN");

            //entity.HasKey(e => e.CODIGO_BOLETIN);

            entity.Property(e => e.codigoAlerta).HasColumnName("CODIGO_ALERTA"); 

            entity.Property(e => e.primerNombre).HasColumnName("PRIMER_NOMBRE"); 

            entity.Property(e => e.segundoNombre).HasColumnName("SEGUNDO_NOMBRE"); 

            entity.Property(e => e.tercerNombre).HasColumnName("TERCER_NOMBRE"); 

            entity.Property(e => e.primerApellido).HasColumnName("PRIMER_APELLIDO"); 

            entity.Property(e => e.segundoApellido).HasColumnName("SEGUNDO_APELLIDO"); 

            entity.Property(e => e.cui).HasColumnName("CUI");

            entity.Property(e => e.edad).HasColumnName("EDAD");

            entity.Property(e => e.fechaNacimiento).HasColumnName("FECHA_NACIMIENTO");

            entity.Property(e => e.fechaHora).HasColumnName("FECHA_HORA_DESAPARICION"); 

            entity.Property(e => e.foto).HasColumnName("FOTO"); 

            entity.Property(e => e.colorCabello).HasColumnName("CODIGO_COLOR_CABELLO"); 

            entity.Property(e => e.tipoCabello).HasColumnName("CODIGO_TIPO_CABELLO"); 

            entity.Property(e => e.tamanioCabello).HasColumnName("CODIGO_TAMANIO_CABELLO"); 

            entity.Property(e => e.tipoCeja).HasColumnName("CODIGO_TIPO_CEJA");

            entity.Property(e => e.colorOjos).HasColumnName("CODIGO_COLOR_OJOS"); 

            entity.Property(e => e.tipoNariz).HasColumnName("CODIGO_TIPO_NARIZ");

            entity.Property(e => e.complexion).HasColumnName("CODIGO_COMPLEXION"); 

            entity.Property(e => e.tez).HasColumnName("CODIGO_TEZ"); 

            entity.Property(e => e.notas).HasColumnName("NOTAS"); 

            entity.Property(e => e.vestimenta).HasColumnName("VESTIMENTA"); 

            entity.Property(e => e.estatura).HasColumnName("ESTATURA"); 

            entity.Property(e => e.genero).HasColumnName("GENERO"); ;

            entity.Property(e => e.seniasParticulares).HasColumnName("SENIAS_PARTICULARES"); 

            entity.Property(e => e.nacionalidad).HasColumnName("NACIONALIDAD");

            entity.Property(e => e.codigoSituacion).HasColumnName("CODIGO_SITUACION");

            entity.Property(e => e.codigoEstatus).HasColumnName("CODIGO_ESTATUS");

            entity.Property(e => e.boletin).HasColumnName("BOLETIN");

            entity.Property(e => e.nombrePadre).HasColumnName("NOMBRE_PADRE");

            entity.Property(e => e.nombreMadre).HasColumnName("NOMBRE_MADRE");

            entity.Property(e => e.responsable).HasColumnName("RESPONSABLE");

            entity.Property(e => e.alias).HasColumnName("ALIAS");

            entity.Property(e => e.observaciones).HasColumnName("OBSERVACIONES");

            entity.Property(e => e.apellidoCasada)
               .HasColumnName("APELLIDO_CASADA");

            entity.HasOne(d => d.CodigoAlertaNavigation)
            .WithMany(p => p.SicmBoletines)
            .HasForeignKey(d => d.codigoAlerta)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_SICM_BITACORA_ALERTA_SICM_ALERTAS");




        }

    } 

    
}
