using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Concrete.Domain.Interfaces
{
    public interface IHashService
    {
        string GetMd5Hash(MD5 md5Hash, string input);
    }
}
