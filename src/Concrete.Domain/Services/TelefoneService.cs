using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.Domain.Interfaces;
using Concrete.Domain.Models;

namespace Concrete.Domain.Services
{
    public class TelefoneService : ITelefoneService
    {

        private readonly ITelefoneRepository _telefoneRepository;

        public TelefoneService(ITelefoneRepository telefoneRepository)
        {
            _telefoneRepository = telefoneRepository;
        }

        public void Dispose()
        {
            _telefoneRepository.Dispose();
            GC.SuppressFinalize(this);
        }
       
        public void AdicionarTelefone(Telefone telefone)
        {
            // Validando se o número está vazio
            if (string.IsNullOrEmpty(telefone.Numero))
            {
                throw new Exception("É necessário fornecer o Número do Telefone");
            }

            // Validando se o ddd está vazio
            if (string.IsNullOrEmpty(telefone.Ddd))
            {
                throw new Exception("É necessário fornecer o DDD do Telefone");
            }

            // Atribuindo o Id para o telefone
            telefone.Id = Guid.NewGuid();


            // Adicionando o telefone
            _telefoneRepository.Adicionar(telefone);
        }
        
    }
}
