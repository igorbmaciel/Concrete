using Concrete.CrossCutting_IoC;
using Concrete.Domain.Interfaces;
using Concrete.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using System;

namespace Concrete.CrossCutting.Test
{
    [TestClass]
    public class UsuarioTest
    {
        // boneco
        public Usuario Usuario { get; set; }
        public Telefone Telefone { get; set; }

        private Container _container;

        // Criando Instância do Container e retornando
        public Container Initialize()
        {
            _container = new Container();

            InitializeContainer(_container);

            _container.Verify();

            return _container;
        }

        // Inicializando o Container
        private void InitializeContainer(Container container)
        {
            BootStrapper.RegisterServices(_container);
        }

        [TestMethod]
        public void CadastrarUsuarioCorretamenteTest()
        {
            Initialize();

            // arrange 

            Usuario = new Usuario()
            {
                Nome = "Teste de Nome",
                Email = "testedeemail@teste.com",
                Senha = "testedesenha"
            };


            var usuarioService = _container.GetInstance<IUsuarioService>();

            // act  
            var usuario = usuarioService.Adicionar(Usuario);

            // assert  
            Assert.IsNotNull(usuario);

            // arrange 
            Telefone = new Telefone()
            {
                Numero = "1234-5678",
                Ddd = "21",
                Id = Guid.NewGuid(),
                UsuarioId = usuario.Id
            };

            var telefoneService = _container.GetInstance<ITelefoneService>();

            // act 
            telefoneService.AdicionarTelefone(Telefone);

            // assert 
            Assert.IsNotNull(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "É necessário fornecer o nome do Usuário")]
        public void CadastrarUsuarioSemNomeTest()
        {
            Initialize();

            // arrange 
            Usuario = new Usuario()
            {
                Nome = "",
                Email = "testedeemail@teste..com",
                Senha = "testedesenha"
            };

            var usuarioService = _container.GetInstance<IUsuarioService>();

            // act 
            var usuario = usuarioService.Adicionar(Usuario);

            // assert 
            Assert.IsNull(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "É necessário fornecer o e-mail do Usuário")]
        public void CadastrarUsuarioSemEmailTest()
        {
            Initialize();

            // arrange 
            Usuario = new Usuario()
            {
                Nome = "teste",
                Email = "",
                Senha = "testedesenha"
            };

            var usuarioService = _container.GetInstance<IUsuarioService>();

            // act 
            var usuario = usuarioService.Adicionar(Usuario);

            // assert
            Assert.IsNull(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "É necessário fornecer a senha do Usuário")]
        public void CadastrarUsuarioSemSenhaTest()
        {
            Initialize();

            // arrange 
            Usuario = new Usuario()
            {
                Nome = "teste",
                Email = "testedeemail@teste.com.",
                Senha = ""
            };

            var usuarioService = _container.GetInstance<IUsuarioService>();

            // act 
            var usuario = usuarioService.Adicionar(Usuario);

            // assert
            Assert.IsNull(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "O e-mail do Usuário já está cadastrado")]
        public void CadastrarUsuarioComEmailExistenteTest()
        {
            Initialize();

            // arrange 
            Usuario = new Usuario()
            {
                Nome = "teste",
                Email = "testedeemail@teste.com",
                Senha = "testedesenha"
            };

            var usuarioService = _container.GetInstance<IUsuarioService>();

            // act 
            var usuario = usuarioService.Adicionar(Usuario);

            // assert
            Assert.IsNull(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "É necessário fornecer o Número do Telefone")]
        public void CadastrarUsuarioSemNumeroTelefoneTest()
        {
            Initialize();

            // arrange
            Usuario = new Usuario()
            {
                Nome = "teste",
                Email = "teste@teste.com.br",
                Senha = "testedesenha"
            };

            var usuarioService = _container.GetInstance<IUsuarioService>();

            var usuario = usuarioService.Adicionar(Usuario);

            Telefone = new Telefone()
            {
                Numero = "",
                Ddd = "21",
                Id = Guid.NewGuid(),
                UsuarioId = usuario.Id
            };

            var telefoneService = _container.GetInstance<ITelefoneService>();

            // act 
            telefoneService.AdicionarTelefone(Telefone);

            // assert
            Assert.IsNull(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "É necessário fornecer o DDD do Telefone")]
        public void CadastrarUsuarioSemDddTelefoneTest()
        {
            Initialize();

            // arrange
            Usuario = new Usuario()
            {
                Nome = "teste",
                Email = "teste@testando.com.br",
                Senha = "testedesenha"
            };

            var usuarioService = _container.GetInstance<IUsuarioService>();

            var usuario = usuarioService.Adicionar(Usuario);

            Telefone = new Telefone()
            {
                Numero = "1234-5678",
                Ddd = "",
                Id = Guid.NewGuid(),
                UsuarioId = usuario.Id
            };

            var telefoneService = _container.GetInstance<ITelefoneService>();

            // act 
            telefoneService.AdicionarTelefone(Telefone);

            // assert
            Assert.IsNull(usuario);
        }
    }
}
