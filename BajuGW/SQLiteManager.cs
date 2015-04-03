using System;
using System.Collections.Specialized;
using System.Data.SQLite;

namespace BajuGW
{
    class SQLiteManager
    {
        private SQLiteConnection sqlConnection;
        private SQLiteCommand sqlCommand;

        public SQLiteManager(String dbName)
        {
            sqlConnection = new SQLiteConnection("Data Source=.\\" + dbName + ";Version=3;");
            sqlCommand = new SQLiteCommand(sqlConnection);
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
        
        public System.Collections.Generic.IEnumerable<NameValueCollection> queryWithReturn(String q)
        {
            SQLiteDataReader reader = null;
            try
            {
                sqlCommand.CommandText = q;
                reader = sqlCommand.ExecuteReader();
            }
            catch
            {
                
            }
            while (reader.Read()) {
                yield return reader.GetValues(); ;   
            }
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
        
        public void initDatabase()
        {
            string[] queries = System.IO.File.ReadAllLines(".\\query.txt");
            foreach (String query in queries)
            {
                queryWithoutReturn(query);
            }
        }
    }
}