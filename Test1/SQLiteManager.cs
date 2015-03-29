using System;
using System.Data.SQLite;

namespace Test1
{
    class SQLiteManager
    {
        private SQLiteConnection sqlConnection;
        private SQLiteCommand sqlCommand;

        public SQLiteManager(String dbName)
        {
            sqlConnection = new SQLiteConnection("Data Source=.\\" + dbName + ";Version=3;");
            sqlCommand = new SQLiteCommand(sqlConnection);
            
            connect();
        }

        /// <summary>
        /// Executing query in parameter for non-result query.
        /// </summary>
        /// <param name="q">the sql query</param>
        /// <returns>number of infected rows.</returns>
        public void queryWithoutReturn(String q)
        {
            try
            {
                sqlCommand.CommandText = q;
                sqlCommand.ExecuteNonQuery();
            }
            catch
            {

            }
            
        }

        /// <summary>
        /// Executing query in parameter for returning-result query.
        /// </summary>
        /// <param name="q">the sql query</param>
        /// <returns>number of infected rows.</returns>
        public SQLiteDataReader queryWithReturn(String q)
        {
            SQLiteDataReader reader;
            try
            {
                sqlCommand.CommandText = q;
                reader = sqlCommand.ExecuteReader();
            }
            catch
            {
                reader = null;
            }
            return reader;
            
        }

        /// <summary>
        /// Method connect ini return type void. (Awalnya int)
        /// </summary>
        public void connect()
        {
            sqlConnection.Open();
        }

        /// <summary>
        /// Method disconnect ini return type void. (Awalnya int)
        /// </summary>
        public void disconnect()
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
    }
}