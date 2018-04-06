using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.Application.ViewModels;

namespace Concrete.Application.Interfaces
{
    public interface ILoginAppService : IDisposable
    {
        UsuarioViewModel Login(string email, string senha);
    }
}
