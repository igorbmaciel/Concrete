using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Concrete.Application.Interfaces;
using Concrete.Application.ViewModels;
using Concrete.Data.Interfaces;
using Concrete.Domain.Interfaces;
using Concrete.Domain.Models;

namespace Concrete.Application.Services
{
    public class LoginAppService : ApplicationService, ILoginAppService
    {

        private readonly ILoginService _loginService;

        public LoginAppService(ILoginService loginService, IUnitOfWork uow) : base(uow)
        {
            _loginService = loginService;
        }

        public void Dispose()
        {
            _loginService.Dispose();
            GC.SuppressFinalize(this);
        }

        public UsuarioViewModel Login(string email, string senha)
        {
            BeginTransaction();
            var usuario = _loginService.Login(email, senha);
            Commit();

            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

            return usuarioViewModel;
        }
    }
}
