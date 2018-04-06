using Concrete.Application.Interfaces;
using Concrete.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Concrete.Services.REST.UsuarioAPI.Controllers
{
    public class UsuariosController : ApiController
    {
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly ITelefoneAppService _telefoneAppService;

        public UsuariosController(IUsuarioAppService usuarioAppService, ITelefoneAppService telefoneAppService, ILoginAppService loginAppService, IProfilerAppService profilerAppService)
        {
            _usuarioAppService = usuarioAppService;
            _telefoneAppService = telefoneAppService;
        }

        // POST: SignUp
        [Route("SignUp")]
        [ResponseType(typeof(UsuarioViewModel))]
        public HttpResponseMessage Post(UsuarioViewModel usuarioViewModel)
        {
            var configuration = new HttpConfiguration();
            var request = new System.Net.Http.HttpRequestMessage();
            request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

            try
            {
                var usuario = _usuarioAppService.Adicionar(usuarioViewModel);

                var telefoneViewModel = new TelefoneViewModel();

                foreach (var telefone in usuario.Telefones)
                {
                    // Adicionando os dados do telefone

                    telefoneViewModel.Id = Guid.NewGuid();
                    telefoneViewModel.Numero = telefone.Numero;
                    telefoneViewModel.Ddd = telefone.Ddd;
                    telefoneViewModel.UsuarioId = usuario.Id;

                    // chamanda o serviço para adicionar o telefone

                    _telefoneAppService.AdicionarTelefone(telefoneViewModel);

                    // Adicionando dados do telefone para retorno

                    telefone.Id = telefoneViewModel.Id;
                    telefone.UsuarioId = telefoneViewModel.UsuarioId;
                }

                return request.CreateResponse(HttpStatusCode.OK, usuario);
            }
            catch (Exception e)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);

            }
            
        }
       
    }
}