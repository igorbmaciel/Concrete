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
    public class LoginsController : ApiController
    {
        private readonly ILoginAppService _loginAppService;

        public LoginsController(ILoginAppService loginAppService)
        {
            _loginAppService = loginAppService;
        }


        // GET: Login
        [Route("Login/{email}/{senha}")]
        [ResponseType(typeof(UsuarioViewModel))]
        public HttpResponseMessage Get(string email, string senha)
        {
            var configuration = new HttpConfiguration();
            var request = new System.Net.Http.HttpRequestMessage();
            request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

            try
            {
                var usuario = _loginAppService.Login(email, senha);

                return request.CreateResponse(HttpStatusCode.OK, usuario);
            }
            catch (Exception e)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}