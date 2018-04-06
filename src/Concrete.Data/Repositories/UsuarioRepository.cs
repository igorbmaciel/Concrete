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
    public class UsuarioRepository : IUsuarioRepository
    {
        protected ConcreteContext Db = new ConcreteContext();

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public Usuario Adicionar(Usuario usuario)
        {
            var cn = Db.Database.Connection;
            var sql = @"INSERT INTO Usuario (id,nome,email,senha,datacriacao,dataatualizacao,dataultimologin,token)" + $"Values ('{usuario.Id}','{usuario.Nome}','{usuario.Email}','{usuario.Senha}','{usuario.DataCriacao}'," +
                      $"'{usuario.DataAtualizacao}','{usuario.DataUltimoLogin}','{usuario.Token}')";

            cn.Execute(sql);

            return usuario;

        }

        public Usuario ObterPorId(Guid id)
        {
            var cn = Db.Database.Connection;
            var sql = @"SELECT * FROM Usuario u " +
                      "LEFT JOIN Telefone t " +
                      "ON t.UsuarioId = u.Id " +
                      "WHERE u.Id = @sid";

            var usuario = new List<Usuario>();
            cn.Query<Usuario, Telefone, Usuario>(sql,
                (c, e) =>
                {
                    usuario.Add(c);
                    if (e != null)
                        usuario[0].Telefones.Add(e);

                    return usuario.FirstOrDefault();
                }, new { sid = id }, splitOn: "Id, Id");

            return usuario.FirstOrDefault();
        }

        public IEnumerable<Usuario> ObterTodos()
        {
            var cn = Db.Database.Connection;

            var sql = @"SELECT * FROM Usuario";

            return cn.Query<Usuario>(sql);
        }

        public void Atualizar(Usuario usuario)
        {
            var cn = Db.Database.Connection;
            var sql = @"UPDATE Usuario " + $"SET DataUltimoLogin = '{usuario.DataUltimoLogin}'" +
                      $"WHERE Id = '{usuario.Id}'";

            cn.Execute(sql);
        }
    }
}
