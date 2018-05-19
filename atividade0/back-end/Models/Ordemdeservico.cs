using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace back_end.Models
{
    public class Ordemdeservico
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string clienteid { get; set; }
        public string funcionarioid { get; set; }
        public string subservicoid { get; set; }
        public string descontoid { get; set; }
        public string observacao { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public Ordemdeservico(AppDb db=null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `ordemdeservico` (`descricao`, `clienteid`, `funcionarioid`, `subservicoid`, `descontoid`, `observacao`) VALUES (@descricao, @clienteid, @funcionarioid, @subservicoid, @descontoid, @observacao);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id = (int) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `ordemdeservico` SET `descricao` = @descricao, `clienteid` = @clienteid, `funcionarioid` = @funcionarioid, `subservicoid` = @subservicoid, `descontoid` = @descontoid, `observacao` = @observacao WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `ordemdeservico` WHERE `id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@descricao",
                DbType = DbType.String,
                Value = descricao,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@clienteid",
                DbType = DbType.String,
                Value = clienteid,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@funcionarioid",
                DbType = DbType.String,
                Value = funcionarioid,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@subservicoid",
                DbType = DbType.String,
                Value = subservicoid,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@descontoid",
                DbType = DbType.String,
                Value = descontoid,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@observacao",
                DbType = DbType.String,
                Value = observacao,
            });
        }

    }
}