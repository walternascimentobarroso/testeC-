using System;
using MySql.Data.MySqlClient;

namespace back_end
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection;

        public AppDb()
        {
            Connection = new MySqlConnection("server=127.0.0.1;user id=root;password=123;port=3306;database=oficina;allow user variables=true;");
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
