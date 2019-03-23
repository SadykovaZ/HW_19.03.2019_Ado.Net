using AdoNet.SMN_182.Unit_1_3;
using HW_19._03._2019.Entities;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;
using System.Drawing.Imaging;

namespace HW_19._03._2019.Repository
{
    public class QrCodeRepository
    {
        private string GetTableName()
        {
            return $"[dbo].[qrCodes]";
        }

        private string GetTableNameGeo()
        {
            return $"[dbo].[qrCodeGeo]";
        }

        public void Add(QRCodeEntity entity)
        {
            string sqlCommand = $"INSERT INTO {GetTableName()}(UserId, Content, QrCodeType) " +
                $"VALUES (@userId, @content, @qrCodeType)";

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter userIdParam = new SqlParameter("@userId", entity.UserId);
                    SqlParameter contentParam = new SqlParameter("@content", entity.Content);
                    SqlParameter qrCodeTypeParam = new SqlParameter("@qrCodeType", (int)entity.QrCodeType);

                    command.Parameters.Add(userIdParam);
                    command.Parameters.Add(contentParam);
                    command.Parameters.Add(qrCodeTypeParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddGeo(QRCodeEntity entity)
        {
            string sqlCommand = $"INSERT INTO {GetTableNameGeo()}(UserId, Geolocation, QrCodeType) " +
                $"VALUES (@userId, @content, @qrCodeType)";

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter userIdParam = new SqlParameter("@userId", entity.UserId);
                    SqlParameter contentParam = new SqlParameter("@content", entity.Content);
                    SqlParameter qrCodeTypeParam = new SqlParameter("@qrCodeType", (int)entity.QrCodeType);

                    command.Parameters.Add(userIdParam);
                    command.Parameters.Add(contentParam);
                    command.Parameters.Add(qrCodeTypeParam);

                    command.ExecuteNonQuery();
                }
            }
        }

       

        public QRCodeEntity Read(int id)
        {
            QRCodeEntity entity = new QRCodeEntity();
            string sql = $"SELECT * FROM {GetTableName()} WHERE ID= @id";
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                SqlParameter IdParam = new SqlParameter("@id", id);

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(IdParam);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            entity.Id = Int32.Parse(reader["Id"].ToString());
                            entity.Content = (byte[])reader["Content"];
                            entity.QrCodeType = (QrCodeType)reader["QrCodeType"];
                        }
                    }
                    else throw new Exception("No data found!");
                }
                return entity;
            }
        }

        public QRCodeEntity ReadGeo(int id)
        {
            QRCodeEntity entity = new QRCodeEntity();
            string sql = $"SELECT * FROM {GetTableNameGeo()} WHERE ID= @id";
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                SqlParameter IdParam = new SqlParameter("@id", id);

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(IdParam);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            entity.Id = Int32.Parse(reader["Id"].ToString());
                            entity.Content = (byte[])reader["Geolocation"];
                            entity.QrCodeType = (QrCodeType)reader["QrCodeType"];
                        }
                    }
                    else throw new Exception("No data found!");
                }
                return entity;
            }
        }



        private string GetConnectionString()
        {
            ConnectionStringInAppConfigDemo appConfig =
                new ConnectionStringInAppConfigDemo();
            return appConfig.GetConnectionString();
        }
    }
}
