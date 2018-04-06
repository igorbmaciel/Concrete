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
    public class UsuarioAppService : ApplicationService, IUsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioAppService(IUsuarioService usuarioService, IUnitOfWork uow) : base(uow)
        {
            _usuarioService = usuarioService;
        }

        public void Dispose()
        {
            _usuarioService.Dispose();
            GC.SuppressFinalize(this);
        }

        public UsuarioViewModel Adicionar(UsuarioViewModel usuarioViewModel)
        {
            var usuario = Mapper.Map<UsuarioViewModel, Usuario>(usuarioViewModel);

            BeginTransaction();

            var usuarioReturn = _usuarioService.Adicionar(usuario);
            usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuarioReturn);

            Commit();

            return usuarioViewModel;
        }

        public UsuarioViewModel ObterPorId(Guid id)
        {
            return Mapper.Map<Usuario, UsuarioViewModel>(_usuarioService.ObterPorId(id));
        }

        public IEnumerable<UsuarioViewModel> ObterTodos()
        {
            return Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_usuarioService.ObterTodos());
        }

        public UsuarioViewModel Atualizar(UsuarioViewModel usuarioViewModel)
        {
            BeginTransaction();
            _usuarioService.Atualizar(Mapper.Map<UsuarioViewModel, Usuario>(usuarioViewModel));
            Commit();
            return usuarioViewModel;
        }
    }
}
