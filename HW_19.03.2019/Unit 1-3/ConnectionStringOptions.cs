using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.SMN_182.Unit_1_3
{
    public class ConnectionStringInCodeDemo
    {
        public string ConnectionString = "Provider=SQLOLEDB.1;" +
            "Integrated Security=SSPI;Persist Security Info=False;" +
            "Initial Catalog=KoreanMetroShopDb;" +
            "Data Source=DESKTOP-O9KC10I";

        public void OpenConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
        }
    }

    public class ConnectionStringInAppConfigDemo
    {
        public string GetConnectionString()
        {
            var connectionString = ConfigurationManager
                .ConnectionStrings["DefaultConnectionString"]
                .ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("No connection string provided!");

            return connectionString;
        }

        public void OpenConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            sqlConnection.Open();
        }
    }
}
