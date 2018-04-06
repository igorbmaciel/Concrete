using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.Domain.Models;

namespace Concrete.Data.ContextConfig
{
    public class TelefoneContextConfig : EntityTypeConfiguration<Telefone>
    {
        public TelefoneContextConfig()
        {
            ToTable("Telefone");
            HasKey(x => x.Id);

            Property(t => t.Numero)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.Ddd)
                .IsRequired()
                .HasMaxLength(100);

            HasRequired(t => t.Usuario)
                .WithMany(u => u.Telefones)
                .HasForeignKey(t => t.UsuarioId);

        }
    }
}
