using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace back_end.Models
{
    public class Funcionario
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string maodeobraid { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public Funcionario(AppDb db=null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `funcionario` (`nome`, `maodeobraid`) VALUES (@nome, @maodeobraid);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            id = (int) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `funcionario` SET `nome` = @nome, `maodeobraid` = @maodeobraid WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `funcionario` WHERE `id` = @id;";
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
                ParameterName = "@nome",
                DbType = DbType.String,
                Value = nome,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@maodeobraid",
                DbType = DbType.String,
                Value = maodeobraid,
            });
        }

    }
}