using Concrete.CrossCutting_IoC;
using Concrete.Domain.Interfaces;
using Concrete.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using System;
using System.Linq;

namespace Concrete.CrossCutting.Test
{
    [TestClass]
    public class LoginTest
    {
        // boneco
        public Usuario Usuario { get; set; }

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
        public void ValidarLoginCorretamenteTest()
        {
            Initialize();

            // arrange 
            var usuarioService = _container.GetInstance<IUsuarioService>();

            // Pegando todos os usuários do banco
            var usuarios = usuarioService.ObterTodos();

            // Pegando o primeiro e-mail do banco
            var email = usuarios.First().Email;

            // Pegando a primeira senha do banco
            var senha = usuarios.First().Senha;

            var loginService = _container.GetInstance<ILoginService>();

            // act  
            var usuario = loginService.Login(email, senha);

            // assert 
            Assert.IsNotNull(usuario);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Usuário e / ou senha inválidos")]
        public void ValidarLoginSemEmailTest()
        {
            Initialize();

            // arrange 
            var usuarioAppService = _container.GetInstance<IUsuarioService>();

            // Pegando todos os usuários do banco
            var usuarios = usuarioAppService.ObterTodos();

            var email = "";

            // Pegando a primeira senha do banco
            var senha = usuarios.First().Senha;

            var loginAppService = _container.GetInstance<ILoginService>();

            // act  
            var usuario = loginAppService.Login(email, senha);

            // assert 
            Assert.IsNull(usuario);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Usuário e / ou senha inválidos")]
        public void ValidarLoginSenhaInvalidaTest()
        {
            Initialize();

            // arrange 
            var usuarioAppService = _container.GetInstance<IUsuarioService>();

            // Pegando todos os usuários do banco
            var usuarios = usuarioAppService.ObterTodos();

            // Pegando o primeiro e-mail do banco
            var email = usuarios.First().Email;
            var senha = "aaa";

            var loginAppService = _container.GetInstance<ILoginService>();

            // act  
            var usuario = loginAppService.Login(email, senha);

            // assert 
            Assert.IsNull(usuario);

        }
    }
}
