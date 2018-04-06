using Concrete.CrossCutting_IoC;
using Concrete.Domain.Interfaces;
using Concrete.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using System;
using System.Globalization;
using System.Linq;

namespace Concrete.CrossCutting.Test
{
    [TestClass]
    public class ProfileTest
    {
        // boneco
        public Usuario Usuario { get; set; }

        private Container _container;

        public Container Initialize()
        {
            _container = new Container();

            InitializeContainer(_container);

            _container.Verify();

            return _container;
        }

        private void InitializeContainer(Container container)
        {
            BootStrapper.RegisterServices(_container);
        }

        [TestMethod]
        public void ValidarTokenCorretamenteTest()
        {
            Initialize();

            // arrange 
            var usuarioAppService = _container.GetInstance<IUsuarioService>();

            // Pegando todos os usuários do banco
            var usuarios = usuarioAppService.ObterTodos();

            // Pegando o primeiro token do banco
            var token = usuarios.First().Token;

            // Pegando o primeiro id do banco
            var id = usuarios.First().Id;

            var profileAppService = _container.GetInstance<IProfileService>();

            // act  
            var usuario = profileAppService.ValidarToken(token,id);

            // assert  
            Assert.IsNotNull(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Não autorizado")]
        public void ValidarTokenSemTokenTest()
        {
            Initialize();

            // arrange 
            var usuarioAppService = _container.GetInstance<IUsuarioService>();

            // Pegando todos os usuários do banco
            var usuarios = usuarioAppService.ObterTodos();

            var token = "";

            // Pegando o primeiro id do banco
            var id = usuarios.First().Id;

            var profileAppService = _container.GetInstance<IProfileService>();

            // act  
            var usuario = profileAppService.ValidarToken(token, id);

            // assert  
            Assert.IsNull(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Não autorizado")]
        public void ValidarTokenIdInvalidoTest()
        {
            Initialize();

            // arrange 
            var usuarioAppService = _container.GetInstance<IUsuarioService>();

            // Pegando todos os usuários do banco
            var usuarios = usuarioAppService.ObterTodos();

            // Pegando o primeiro token do banco
            var token = usuarios.First().Token;

            // Criando um novo Guid para ser diferente do Guid cadastrado para o token acima
            var id = Guid.NewGuid();

            var profileAppService = _container.GetInstance<IProfileService>();

            // act  
            var usuario = profileAppService.ValidarToken(token, id);

            // assert  
            Assert.IsNull(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Sessão inválida")]
        public void ValidarTokenSessaoInvalidaTest()
        {
            Initialize();

            // arrange 
            var usuarioAppService = _container.GetInstance<IUsuarioService>();

            // Pegando todos os usuários do banco
            var usuarios = usuarioAppService.ObterTodos().First();

            // Criando a cultura americana
            var cult = new CultureInfo("en-US");

            // Criando uma data com o formato americano
            usuarios.DataUltimoLogin = DateTime.Now.AddMinutes(-31).ToString("yyyy/MM/dd HH:mm:ss", cult);

            // Atualizando os dados do usuário
            usuarioAppService.Atualizar(usuarios);


            // Pegando o primeiro token do banco onde a data de ultimo login seja maior que 30 minutos
            var token = usuarios.Token;

            // Pegando o primeiro id do banco onde a data de ultimo login seja maior que 30 minutos
            var id = usuarios.Id;

            var profileAppService = _container.GetInstance<IProfileService>();

            // act  
            var usuario = profileAppService.ValidarToken(token, id);

            // assert  
            Assert.IsNull(usuario);
        }
    }
}
