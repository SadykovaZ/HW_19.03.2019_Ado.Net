using AdoNet.SMN_182.Unit_1_3;
using HW_19._03._2019.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_19._03._2019.Repository
{
    public class ProductRepository
    {
        private string GetTableName()
        {
            return $"[dbo].[products]";
        }

        public Product Read(int id)
        {
            Product product = new Product();
            string sql = $"SELECT * FROM {GetTableName()} WHERE ID = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            product.Id = Int32.Parse(reader["Id"].ToString());
                            product.ProductName = reader["ProductName"].ToString();
                            product.Cost = Decimal.Parse(reader["Cost"].ToString());
                        }
                    }
                    else throw new Exception("No data found!");
                }
            }
            return product;
        }

        private string GetConnectionString()
        {
            ConnectionStringInAppConfigDemo appConfig =
                new ConnectionStringInAppConfigDemo();
            return appConfig.GetConnectionString();
        }
    }
}
