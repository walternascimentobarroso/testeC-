using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace back_end.Models
{
    public class FuncionarioQuery
    {

        public readonly AppDb Db;
        public FuncionarioQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Funcionario> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `funcionario` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Funcionario>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `funcionario` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            var txn = await Db.Connection.BeginTransactionAsync();
            try
            {
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM `funcionario`";
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
            }
            catch
            {
                await txn.RollbackAsync();
                throw;
            }
        }

        private async Task<List<Funcionario>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Funcionario>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Funcionario(Db)
                    {
                        id = await reader.GetFieldValueAsync<int>(0),
                        nome = await reader.GetFieldValueAsync<string>(1),
                        maodeobraid = await reader.GetFieldValueAsync<string>(2)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
