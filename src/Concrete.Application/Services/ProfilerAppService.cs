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
    public class ProfilerAppService : ApplicationService, IProfilerAppService
    {
        private readonly IProfileService _profileService;

        public ProfilerAppService(IProfileService profileService, IUnitOfWork uow) : base(uow)
        {
            _profileService = profileService;
        }

        public void Dispose()
        {
            _profileService.Dispose();
            GC.SuppressFinalize(this);
        }

        public UsuarioViewModel ValidarToken(string token, Guid id)
        {
            BeginTransaction();
            var usuario = _profileService.ValidarToken(token,id);
            Commit();

            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

            return usuarioViewModel;
        }
    }
}
