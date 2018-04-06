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
    public class TelefoneAppService : ApplicationService, ITelefoneAppService
    {
        private readonly ITelefoneService _telefoneService;

        public TelefoneAppService(ITelefoneService telefoneService, IUnitOfWork uow) : base(uow)
        {
            _telefoneService = telefoneService;
        }

        public void Dispose()
        {
            _telefoneService.Dispose();
            GC.SuppressFinalize(this);
        }

        public TelefoneViewModel AdicionarTelefone(TelefoneViewModel telefoneViewModel)
        {
            var telefone = Mapper.Map<TelefoneViewModel, Telefone>(telefoneViewModel);

            BeginTransaction();
            _telefoneService.AdicionarTelefone(telefone);
            Commit();

            return telefoneViewModel;
        }
    }
}
