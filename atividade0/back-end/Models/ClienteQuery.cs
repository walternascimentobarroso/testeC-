using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace back_end.Models
{
    public class ClienteQuery
    {

        public readonly AppDb Db;
        public ClienteQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Cliente> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `cliente` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Cliente>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `cliente` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            var txn = await Db.Connection.BeginTransactionAsync();
            try
            {
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM `cliente`";
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
            }
            catch
            {
                await txn.RollbackAsync();
                throw;
            }
        }

        private async Task<List<Cliente>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Cliente>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Cliente(Db)
                    {
                        id = await reader.GetFieldValueAsync<int>(0),
                        nome = await reader.GetFieldValueAsync<string>(1),
                        telefone = await reader.GetFieldValueAsync<string>(2),
                        celular = await reader.GetFieldValueAsync<string>(3)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
