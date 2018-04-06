using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete.Domain.Interfaces
{
    public interface ITokenService
    {
        string CriarToken(string nome, string email);
    }
}
