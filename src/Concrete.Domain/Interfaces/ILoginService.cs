using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.Domain.Models;

namespace Concrete.Domain.Interfaces
{
    public interface ILoginService : IDisposable
    {
        Usuario Login(string email, string senha);
    }
}
