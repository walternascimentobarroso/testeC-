using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace back_end.Models
{
    public class Maodeobra
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descricao { get; set; }
        public string precohora { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public Maodeobra(AppDb db=null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `maodeobra` (`codigo`, `descricao`, `precohora`) VALUES (@codigo, @descricao, @precohora);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id = (int) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `maodeobra` SET `codigo` = @codigo, `descricao` = @descricao, `precohora` = @precohora WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `maodeobra` WHERE `id` = @id;";
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
                ParameterName = "@codigo",
                DbType = DbType.String,
                Value = codigo,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@descricao",
                DbType = DbType.String,
                Value = descricao,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@precohora",
                DbType = DbType.String,
                Value = precohora,
            });
        }

    }
}