using System.Data;
using System.Data.SqlClient;

namespace symmons.com._Classes.Symmons.Custom.Migration
{
    public class ProductMigrationData
    {
        // ********************************************************************************************************************

        public static SqlConnection SqlConnection
        {
            get
            {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["redirect"].ConnectionString;
                var connection = new SqlConnection(connectionString);
                return connection;
            }
        }

        // ********************************************************************************************************************

        public bool LogUpdate(string itemId, string productName,string oldProductPath,string status, string error)
        {
            try
            {
                var logQuery = "INSERT INTO [dbo].[LoggingTable]([itemId],[productName],[oldProductPath],[insertStatus],[error]) VALUES ('"
                                + itemId + "','" + productName + "','" + oldProductPath + "','" + status + "','" + error + "')";
                var connecion = SqlConnection;
                var logCommand = new SqlCommand(logQuery, connecion);
                connecion.Open();
                logCommand.ExecuteNonQuery();
                connecion.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ********************************************************************************************************************

        public bool LogProductForRollBack(string itemId, string productName,string oldProductPath,string newProductPath,string productType, string error)
        {
            try
            {
                var logQuery = "INSERT INTO [dbo].[ProductRollBack]([itemId],[productName],[oldProductPath],[newProductPath],[productType],[error]) VALUES ('"
                                + itemId + "','" + productName + "','" + oldProductPath + "','" + newProductPath + "','" + productType + "','" + error + "')";
                var connecion = SqlConnection;
                var logCommand = new SqlCommand(logQuery, connecion);
                connecion.Open();
                logCommand.ExecuteNonQuery();
                connecion.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ********************************************************************************************************************

        public DataTable GetProducts()
        {
            try
            {
                var dataAdapter =
                 new SqlDataAdapter("SELECT * FROM [dbo].[ProductRollBack]",
                     SqlConnection) { SelectCommand = { CommandType = CommandType.Text } };
                return DbConnect(dataAdapter);
            }
            catch
            {
                return null;
            }
        }


        public DataTable GetProductsFailed()
        {
            try
            {
                var dataAdapter =
                 new SqlDataAdapter("SELECT * FROM [dbo].[LoggingTable]",
                     SqlConnection) { SelectCommand = { CommandType = CommandType.Text } };
                return DbConnect(dataAdapter);
            }
            catch
            {
                return null;
            }
        }

        // ********************************************************************************************************************

        private DataTable DbConnect(SqlDataAdapter dataAdapter)
        {
            var resultTable = new DataTable();
            dataAdapter.Fill(resultTable);
            return resultTable;
        }

        // ********************************************************************************************************************
        // ********************************************************************************************************************
    }
}