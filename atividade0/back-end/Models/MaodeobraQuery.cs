using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace back_end.Models
{
    public class MaodeobraQuery
    {

        public readonly AppDb Db;
        public MaodeobraQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Maodeobra> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `maodeobra` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Maodeobra>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `maodeobra` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            var txn = await Db.Connection.BeginTransactionAsync();
            try
            {
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM `maodeobra`";
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
            }
            catch
            {
                await txn.RollbackAsync();
                throw;
            }
        }

        private async Task<List<Maodeobra>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Maodeobra>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Maodeobra(Db)
                    {
                        id = await reader.GetFieldValueAsync<int>(0),
                        codigo = await reader.GetFieldValueAsync<string>(1),
                        descricao = await reader.GetFieldValueAsync<string>(2),
                        precohora = await reader.GetFieldValueAsync<string>(3)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
