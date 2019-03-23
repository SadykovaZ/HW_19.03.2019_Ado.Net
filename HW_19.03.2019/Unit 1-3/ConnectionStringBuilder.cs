using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.SMN_182.Unit_1_3
{
    public class ConnectionStringBuilder
    {
        public string SqlConnectionStringBuilderDemo()
        {
            ConnectionStringInAppConfigDemo connectionStringContainer = new ConnectionStringInAppConfigDemo();
            string connectionString = connectionStringContainer.GetConnectionString();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.MultipleActiveResultSets = true;
            builder.ConnectTimeout = 5;

            return builder.ConnectionString;
        }

        public void OpenConnection()
        {
            using (HttpClient httpClient = new HttpClient())
            {

            }

            using(MemoryStream stream = new MemoryStream())
            {

            }

            using (SqlConnection sqlConnection =
                new SqlConnection(SqlConnectionStringBuilderDemo()))
            {
                try
                {
                    sqlConnection.Open();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Connection error");
                }
            }
        }
    }
}
