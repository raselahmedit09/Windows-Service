using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWindowsService
{
    partial class DataBaseOperation
    {
        private static readonly object _convertToTextByHtmlLock = new object();
        private static string createdBy = ConfigurationManager.AppSettings["CreatedBy"];
        private static int createdById = Convert.ToInt32(ConfigurationManager.AppSettings["CreatedById"]);


        private static string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
        static SqlConnection conn = null;

        public static void OpenConnection()
        {
            conn = new SqlConnection(connectionString);
            conn.Open(); // open the connection
        }

        public static void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close(); // close the connction
                conn.Dispose(); // destroy the connection object
            }
        }

        // your method to insert, upagte , get data; 
    }
}
