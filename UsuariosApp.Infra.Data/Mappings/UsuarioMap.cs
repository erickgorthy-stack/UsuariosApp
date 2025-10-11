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
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario"); // nome da tabela
            builder.HasKey(u => u.Id); //Chave Primária
            builder.Property(u => u.Id) //Campo
                .HasColumnName("ID");
            builder.Property(u => u.Nome) //Campo
                .HasColumnName("NOME")
                .HasMaxLength(25)
                .IsRequired();
            builder.HasIndex(u => u.Nome)
                .IsUnique(); //Campo único
            builder.Property(u => u.Email) //Campo
               .HasColumnName("EMAIL")
               .HasMaxLength(100)
               .IsRequired();
            builder.HasIndex(u => u.Email) //Campo único
                .IsUnique();
            builder.Property(u => u.Senha) //Campo
               .HasColumnName("SENHA")
               .HasMaxLength(100)
               .IsRequired();
            builder.Property(u => u.PerfilId) //Campo
               .HasColumnName("PERFIL_ID")
               .IsRequired();
            builder.HasOne(u => u.Perfil) //usuário tem UM perfil
                .WithMany(p => p.Usuarios) //perfil TEM MUITOS usuários
                .HasForeignKey(u => u.PerfilId); //chave estrangeira
        }
    }
}
