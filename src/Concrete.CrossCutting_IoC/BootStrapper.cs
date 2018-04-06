using Concrete.Application.Interfaces;
using Concrete.Application.Services;
using Concrete.Data.Context;
using Concrete.Data.Interfaces;
using Concrete.Data.Repositories;
using Concrete.Data.UoW;
using Concrete.Domain.Interfaces;
using Concrete.Domain.Services;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete.CrossCutting_IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            // Lifestyle.Transient => Uma instancia para cada solicitacao;
            // Lifestyle.Singleton => Uma instancia unica para a classe
            // Lifestyle.Scoped => Uma instancia unica para o request

            // App
            container.Register<IUsuarioAppService, UsuarioAppService>(Lifestyle.Singleton);
            container.Register<ITelefoneAppService,TelefoneAppService>(Lifestyle.Singleton);
            container.Register<ILoginAppService, LoginAppService>(Lifestyle.Singleton);
            container.Register<IProfilerAppService, ProfilerAppService>(Lifestyle.Singleton);

            // Domain
            container.Register<IUsuarioService, UsuarioService>(Lifestyle.Singleton);
            container.Register<ITelefoneService, TelefoneService>(Lifestyle.Singleton);
            container.Register<ILoginService, LoginService>(Lifestyle.Singleton);
            container.Register<IProfileService, ProfileService>(Lifestyle.Singleton);
            container.Register<ITokenService, TokenService>(Lifestyle.Singleton);
            container.Register<IHashService, HashService>(Lifestyle.Singleton);

            // Infra Dados
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Singleton);
            container.Register<ITelefoneRepository, TelefoneRepository>(Lifestyle.Singleton);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Singleton);
            container.Register<ConcreteContext>(Lifestyle.Singleton);
           
        }
    }
}
