using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.Domain.Models;

namespace Concrete.Data.ContextConfig
{
    public class UsuarioContextConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioContextConfig()
        {
            ToTable("Usuario");
            HasKey(x => x.Id);

            Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(150);

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            Property(u => u.Senha)
                .IsRequired()
                .HasMaxLength(100);

            Property(u => u.Token)
                .IsRequired()
                .HasMaxLength(300);

        }
    }
}
