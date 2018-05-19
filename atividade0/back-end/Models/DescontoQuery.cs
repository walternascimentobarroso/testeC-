using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace back_end.Models
{
    public class DescontoQuery
    {

        public readonly AppDb Db;
        public DescontoQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Desconto> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `desconto` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Desconto>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `desconto` ORDER BY `id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            var txn = await Db.Connection.BeginTransactionAsync();
            try
            {
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM `desconto`";
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
            }
            catch
            {
                await txn.RollbackAsync();
                throw;
            }
        }

        private async Task<List<Desconto>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Desconto>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Desconto(Db)
                    {
                        id = await reader.GetFieldValueAsync<int>(0),
                        precototal = await reader.GetFieldValueAsync<string>(1),
                        desconto = await reader.GetFieldValueAsync<string>(2)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
