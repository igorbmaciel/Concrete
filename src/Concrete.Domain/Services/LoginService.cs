using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Concrete.Domain.Interfaces;
using Concrete.Domain.Models;

namespace Concrete.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IHashService _hashService;

        public LoginService(IUsuarioRepository usuarioRepository, IHashService hashService)
        {
            _usuarioRepository = usuarioRepository;
            _hashService = hashService;
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public Usuario Login(string email, string senha)
        {

            // Obtendo os usuários do banco
            var usuarios = _usuarioRepository.ObterTodos();

            // Validando se o e-mail está cadastrado
            var validaEmail = usuarios.Any(u => u.Email == email);


            // Caso não esteja, retornar erro
            if (!validaEmail)
            {
                throw new Exception("Usuário e / ou senha inválidos");
            }

            // Validando se a senha está correta
            var validaSenha = usuarios.FirstOrDefault(u => u.Email == email).Senha == senha;


            // Caso não esteja, retornar erro
            if (!validaSenha)
            {
                throw new Exception("Usuário e / ou senha inválidos");
            }


            // Buscando o usuário pelo e-mail
            var usuario = usuarios.FirstOrDefault(u => u.Email == email);

            // Criando a cultura americana
            var cult = new CultureInfo("en-US");
            
            // Criando uma data com o formato americano
            usuario.DataUltimoLogin = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", cult);

            // Atualizando os dados do usuário
            _usuarioRepository.Atualizar(usuario);

            // Retornando o usuário
            return _usuarioRepository.ObterPorId(usuario.Id); ;
        }
    }
}
