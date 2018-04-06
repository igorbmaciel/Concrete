using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.Domain.Models;

namespace Concrete.Domain.Interfaces
{
    public interface IProfileService : IDisposable
    {
        Usuario ValidarToken(string token, Guid id);
    }
}
