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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;
        private readonly IHashService _hashService;

        public UsuarioService(IUsuarioRepository usuarioRepository, ITokenService tokenService, 
            IHashService hashService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _hashService = hashService;
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public Usuario Adicionar(Usuario usuario)
        {
            // Validando se o nome está em branco
            if (string.IsNullOrEmpty(usuario.Nome))
            {
                throw new Exception("É necessário fornecer o nome do Usuário");
            }

            // Validando se o e-mail está em branco
            if (string.IsNullOrEmpty(usuario.Email))
            {
                throw new Exception("É necessário fornecer o e-mail do Usuário");
            }

            // Validando se a senha está em branco
            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new Exception("É necessário fornecer a senha do Usuário");
            }

            // Obtendo todos os usuários do banco
            var usuarios = ObterTodos();

            // Validando se o e-mail já está cadastrado
            var emailCadatrado = usuarios.Any(u => u.Email == usuario.Email);

            // Caso esteja, retornar erro
            if (emailCadatrado)
            {
                throw new Exception("O e-mail do Usuário já está cadastrado");
            }

            // Criando MD5
            var md5Hash = MD5.Create();

            var senha = usuario.Senha;

            // Criando a cultura americana
            var cult = new CultureInfo("en-US");

            // Criando o Guid para o Usuário
            usuario.Id = Guid.NewGuid();

            // passando o MD5 e a senha para transformar em Hash
            usuario.Senha = _hashService.GetMd5Hash(md5Hash, senha);

            // Criando uma data com o formato americano
            usuario.DataCriacao = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", cult); 
            usuario.DataAtualizacao = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", cult);
            usuario.DataUltimoLogin = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", cult);

            // Criando token para o usuário
            usuario.Token = _tokenService.CriarToken(usuario.Nome,usuario.Email);

            // Adicionando o usuário e retornando
            return _usuarioRepository.Adicionar(usuario);

        }

        public Usuario ObterPorId(Guid id)
        {
            return _usuarioRepository.ObterPorId(id);
        }

        public IEnumerable<Usuario> ObterTodos()
        {
            return _usuarioRepository.ObterTodos();
        }

        public void Atualizar(Usuario usuario)
        {
            _usuarioRepository.Atualizar(usuario);
        }
    }
}
