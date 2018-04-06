using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.Domain.Models;

namespace Concrete.Domain.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Usuario Adicionar(Usuario usuario);
        Usuario ObterPorId(Guid id);
        IEnumerable<Usuario> ObterTodos();
        void Atualizar(Usuario usuario);
    }
}
