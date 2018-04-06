using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.Domain.Interfaces;

namespace Concrete.Domain.Services
{
    public class TokenService : ITokenService
    {
        public string CriarToken(string nome, string email)
        {
            // Definindo uma Const Key que deve ser uma chave secreta privada armazenada em algum lugar seguro
            string key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b372742           9090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

            // Criando a chave de segurança usando a chave privada acima:
            // não a versão mais recente do JWT usando o namespace Microsoft em vez do sistema
                        var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // Observe também que o comprimento da segurança deve ser > 256b
            // então você deve ter certeza de que sua chave privada tenha um comprimento adequado
            //
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256Signature);

            //  Finalmente criando um Token
            var header = new JwtHeader(credentials);

            // Alguns PayLoad que contêm informações sobre o usuário
            var payload = new JwtPayload
           {
               { "Nome ", nome},
               { "Email", email},
           };

            //
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            // Token em String para que você possa usá-lo em seu usuário
            var tokenString = handler.WriteToken(secToken);

            // Retornando token
            return tokenString;
        }
    }
}
