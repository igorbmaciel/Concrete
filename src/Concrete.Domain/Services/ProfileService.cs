using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.Domain.Interfaces;
using Concrete.Domain.Models;

namespace Concrete.Domain.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public ProfileService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
            GC.SuppressFinalize(this);
        }


        public Usuario ValidarToken(string token, Guid id)
        {

            // Validando se o token existe
            var validarToken = _usuarioRepository.ObterTodos().Any(u => u.Token == token);


            // Caso não exista, retornar erro
            if (!validarToken)
            {
                throw new Exception("Não autorizado");
            }

            // Buscando o usuário pelo ID
            var usuario = _usuarioRepository.ObterPorId(id);

            var validarUsuario = false;

            // Validando se o token passado é do usuário
            if (usuario != null)
                validarUsuario = usuario.Token == token;


            // Caso não seja, retornar erro
            if (!validarUsuario)
            {
                throw new Exception("Não autorizado");
            }

            // Validando se a data do último login é menor que 30 minutos
            var validarTempo = DateTime.Now.AddMinutes(-30) < Convert.ToDateTime(usuario.DataUltimoLogin);

            // Caso não seja, retornar erro
            if (!validarTempo)
            {
                throw new Exception("Sessão inválida");
            }

            // Retornando usuário
            return usuario;
        }
    }
}
