using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concrete.Data.Context;
using Concrete.Domain.Interfaces;
using Concrete.Domain.Models;
using Dapper;

namespace Concrete.Data.Repositories
{
    public class TelefoneRepository : ITelefoneRepository
    {

        protected ConcreteContext Db = new ConcreteContext();

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Adicionar(Telefone telefone)
        {
            var cn = Db.Database.Connection;
            var sql = @"INSERT INTO Telefone (id,numero,ddd,usuarioid)" + $"Values ('{telefone.Id}','{telefone.Numero}','{telefone.Ddd}','{telefone.UsuarioId}')";

            cn.Execute(sql);
           
        }
    }
}
