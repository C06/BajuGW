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
                sqlConnection = new SQLiteConnection("Data source=" + dbName + ";Version=3;FailIfMissing=True;");
                connect();
            }
            catch (SQLiteException e)
            {
                sqlConnection = new SQLiteConnection("Data source=" + dbName + ";Version=3;");
                /*
                 *  Create tabel-tabel yang ada di dalam database menurut ERD
                 *  menggunakan method query.
                 */
                connect();                   

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
        /// <returns>number of infected rows.</returns>
        int queryWithReturn(String q)
        {
            sqlCommand.CommandText = q;
            return sqlCommand.ExecuteNonQuery();
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
    }
}
