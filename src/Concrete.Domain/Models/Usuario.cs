using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete.Domain.Models
{
    public class Usuario
    {
        public Usuario()
        {
            Telefones = new List<Telefone>();
        }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public ICollection<Telefone> Telefones { get; set; }
        public string DataCriacao { get; set; }
        public string DataAtualizacao { get; set; }
        public string DataUltimoLogin { get; set; }
        public string Token { get; set; }
    }
}
