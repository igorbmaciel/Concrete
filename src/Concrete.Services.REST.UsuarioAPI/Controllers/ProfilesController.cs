using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Concrete.Application.Interfaces;
using Concrete.Application.ViewModels;

namespace Concrete.Services.REST.UsuarioAPI.Controllers
{
    public class ProfilesController : ApiController
    {
        private readonly IProfilerAppService _profilerAppService;

        public ProfilesController(IProfilerAppService profilerAppService)
        {
            _profilerAppService = profilerAppService;
        }

        // GET: Profile
        [Route("Profile/{id}")]
        [ResponseType(typeof(UsuarioViewModel))]
        public HttpResponseMessage Get(Guid id)
        {
            var configuration = new HttpConfiguration();
            var request = new System.Net.Http.HttpRequestMessage();
            request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

            // Buscando o Header de Autorização

            var authorization = Request.Headers.Authorization;

            var token = authorization.ToString();

            try
            {
                var usuario = _profilerAppService.ValidarToken(token, id);

                return request.CreateResponse(HttpStatusCode.OK, usuario);
            }
            catch (Exception e)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}