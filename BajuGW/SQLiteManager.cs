using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace BajuGW
{
    class SQLiteManager
    {
        private SQLiteCommand sqlCommand;
        private SQLiteConnection sqlConnection;
        public SQLiteManager(String dbName)
        {
            sqlCommand = new SQLiteCommand();
            ///<summary>
            ///Checking wether the database exist
            ///</summary>
            try
            {
                sqlConnection = new SQLiteConnection("Data source=dbBajuGW.sqlite3;Version=3;FailIfMissing=True;");
                connect();
                sqlCommand.Connection = sqlConnection;
            }
            catch (Exception e)
            {
                sqlConnection = new SQLiteConnection("Data source=dbBajuGW.sqlite3;Version=3;");
                connect();
                sqlCommand.Connection = sqlConnection;
                initialize();
            }            
        }

        /// <summary>
        /// Executing query in parameter for non-result query.
        /// </summary>
        /// <param name="q">the sql query</param>
        /// <returns>number of infected rows.</returns>
        int queryWithoutReturn(String q)
        {
            sqlCommand.CommandText = q;
            return sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Executing query in parameter for returning-result query.
        /// </summary>
        /// <param name="q">the sql query</param>
        /// <returns>SQLiteDataReader</returns>
        SQLiteDataReader queryWithReturn(String q)
        {
            sqlCommand.CommandText = q;
            return sqlCommand.ExecuteReader();
        }

        /// <summary>
        /// Method connect ini return type void. (Awalnya int)
        /// </summary>
        private void connect()
        {
            sqlConnection.Open();
        }

        /// <summary>
        /// Method disconnect ini return type void. (Awalnya int)
        /// </summary>
        private void disconnect()
        {
            sqlConnection.Close();
        }

        /// <summary>
        /// Method checkConnection ini return type bool. (Awalnya int)
        /// </summary>
        public bool checkConnection()
        {
            if (sqlConnection.ConnectionString != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Initializing the database for first use.
        /// </summary>
        private void initialize()
        {
            String sql = "CREATE TABLE user (user_name [tipe-data], password_hashed [tipe-data], salt [tipe-data], nama [tipe-data], email [tipe-data], )";
            queryWithoutReturn(sql);

            sql = "CREATE TABLE pakaian (user_name [tipe-data], item_id [tipe-data], favorit [tipe-data], warna [tipe-data], merek [tipe-data], )";
            queryWithoutReturn(sql);

            sql = "CREATE TABLE kategori (user_name [tipe-data], item_id [tipe-data], tag_name [tipe-data])";
            queryWithoutReturn(sql);
        }
    }
}