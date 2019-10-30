using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Facebook.DataAccess
{
    public class SqlClient
    {
        private static SqlConnection Connection { get; set; }
        private static SqlClient _sqlClient;

        private SqlClient(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public static SqlClient GetSqlClient(string connectionString)
        {
            if(Connection == null)
            {
                _sqlClient = new SqlClient(connectionString);
            }
            return _sqlClient;
        }

        public SqlConnection GetConnection()
        {
            return Connection;
        }

        public bool Open()
        {
            try
            {
                Connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void alert(string v)
        {
            throw new NotImplementedException();
        }

        public bool Close()
        {
            try
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
