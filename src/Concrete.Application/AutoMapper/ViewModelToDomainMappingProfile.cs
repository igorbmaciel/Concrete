using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Concrete.Application.ViewModels;
using Concrete.Domain.Models;

namespace Concrete.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<UsuarioViewModel, Usuario>();
            CreateMap<TelefoneViewModel, Telefone>();
        }
    }
}
