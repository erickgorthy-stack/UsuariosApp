using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Infra.Data.Mappings
{
    public class PerfilMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("Perfil"); // nome da tabela
            builder.HasKey(p => p.Id); //Chave Primária
            builder.Property(p => p.Id) //Campo
                .HasColumnName("ID");
            builder.Property(p => p.Nome) //Campo
                .HasColumnName("NOME")
                .HasMaxLength(25)
                .IsRequired();
            builder.HasIndex(p => p.Nome)
                .IsUnique(); //Campo único
        }
    }
}
