using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Concrete.Domain.Interfaces;

namespace Concrete.Domain.Services
{
    public class HashService : IHashService
    {
        public string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convertendo a cadeia de entrada para uma matriz de bytes e computa o hash.
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Criando um novo Stringbuilder para coletar os bytes
            // e criando uma string.
            var sBuilder = new StringBuilder();

            // Loop através de cada byte dos dados hash
            // e formata cada um como uma seqüência hexadecimal.
            foreach (var b in data)
            {
                sBuilder.Append(b.ToString("x2"));
            }

            // Retornando a string hexadecimal.
            return sBuilder.ToString();
        }
    }
}
