using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace back_end.Models
{
    public class OrdemdeservicoQuery
    {

        public readonly AppDb Db;
        public OrdemdeservicoQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Ordemdeservico> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `ordemdeservico` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Ordemdeservico>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `ordemdeservico` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            var txn = await Db.Connection.BeginTransactionAsync();
            try
            {
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM `ordemdeservico`";
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
            }
            catch
            {
                await txn.RollbackAsync();
                throw;
            }
        }

        private async Task<List<Ordemdeservico>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Ordemdeservico>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Ordemdeservico(Db)
                    {
                        id = await reader.GetFieldValueAsync<int>(0),
                        descricao = await reader.GetFieldValueAsync<string>(1),
                        clienteid = await reader.GetFieldValueAsync<string>(2),
                        funcionarioid = await reader.GetFieldValueAsync<string>(3),
                        subservicoid = await reader.GetFieldValueAsync<string>(4),
                        descontoid = await reader.GetFieldValueAsync<string>(5),
                        observacao = await reader.GetFieldValueAsync<string>(6)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
