using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete.Domain.Models
{
    public class Telefone
    {
        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string Ddd { get; set; }
        public virtual Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
