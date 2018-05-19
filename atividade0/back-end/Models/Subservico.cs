using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace back_end.Models
{
    public class Subservico
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string especialidadeid { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public Subservico(AppDb db=null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `subservico` (`descricao`, `especialidadeid`) VALUES (@descricao, @especialidadeid);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id = (int) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `subservico` SET `descricao` = @descricao, `especialidadeid` = @especialidadeid WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `subservico` WHERE `id` = @id;";
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
                ParameterName = "@especialidadeid",
                DbType = DbType.String,
                Value = especialidadeid,
            });
        }

    }
}