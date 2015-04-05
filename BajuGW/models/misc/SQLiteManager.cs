using System;
using System.Collections.Specialized;
using System.Data.SQLite;

namespace BajuGW
{
    /**
     * Kelas manager untuk mengatur transaksi SQL
     * 
     */
    public class SQLiteManager
    {
        private SQLiteConnection sqlConnection;
        private SQLiteCommand sqlCommand;

        
        /**
         * Construktor utama kelas ini
         * 
         */
        public SQLiteManager(string databaseName)
        {
            sqlConnection = new SQLiteConnection(
                "Data Source=.\\" + databaseName +";Version=3;foreign keys=true;");
            
            sqlCommand = new SQLiteCommand(sqlConnection);
        }

        
        /**
         * Jalankan query SQL tanpa mengembalikan hasil
         * 
         */
        public bool queryWithoutReturn(string query)
        {
            try
            {
                sqlCommand.CommandText = query;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter("log.txt", true))
                {
                    file.WriteLine(exception.Message);
                }
            }
            return true;
        }
        
        
        /**
         * Kembalikan hasil dari eksekusi perintah SQL
         * 
         */
        public System.Collections.Generic.IEnumerable<NameValueCollection>
            queryWithReturn(string query)
        {
            SQLiteDataReader reader = null;
            try
            {
                sqlCommand.CommandText = query;
                reader = sqlCommand.ExecuteReader();
            }
            catch (Exception exception)
            {
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter("log.txt", true))
                {
                    file.WriteLine(exception.Message);
                }

                yield break;
            }
            while (reader.Read()) {
                yield return reader.GetValues(); ;   
            }
        }

        
        /**
         * Nyalakan koneksi
         * 
         */
        public void connect()
        {
            sqlConnection.Open();
        }

        
        /**
         * Matikan koneksi
         * 
         */
        public void disconnect()
        {
            sqlConnection.Close();
        }

        
        /**
         * Periksa apakah koneksi ke DBMS masih terhubung atau tidak
         * 
         */
        public bool checkConnection()
        {
            if (sqlConnection.ConnectionString != null)
                return true;
            return false;
        }
        

        /**
         * Buat tabel yang akan dipakai
         * 
         */
        public void initDatabase()
        {
            string query =
                "CREATE TABLE User (username VARCHAR(10) PRIMARY KEY,password VARCHAR(10) NOT NULL,salt TEXT UNIQUE NOT NULL,email TEXT NOT NULL,cloth_width NUMERIC,cloth_height NUMERIC,face BLOB,theme TEXT);" +
                "CREATE TABLE Store (id INTEGER PRIMARY KEY ON CONFLICT ROLLBACK AUTOINCREMENT);" +
                "CREATE TABLE User_Activate_Store (username VARCHAR(10) PRIMARY KEY REFERENCES User(username) ON DELETE CASCADE ON UPDATE CASCADE,store_id INTEGER PRIMARY KEY REFERENCES Store(id) ON DELETE NO ACTION ON UPDATE NO ACTION);" +
                "CREATE TABLE Cloth (username VARCHAR(10) PRIMARY KEY REFERENCES User(username) ON DELETE CASCADE ON UPDATE CASCADE,id INTEGER PRIMARY KEY ON CONFLICT ROLLBACK AUTOINCREMENT,name TEXT,brand TEXT,favorite BOOLEAN,color TEXT,cloth_width NUMERIC NOT NULL,cloth_height NUMERIC NOT NULL,picture BLOB NOT NULL);" +
                "CREATE TABLE Category (username VARCHAR(10) PRIMARY KEY REFERENCES User(username) ON DELETE CASCADE ON UPDATE CASCADE,id TEXT PRIMARY KEY);" +
                "CREATE TABLE Cloth_Has_Category (username VARCHAR(10) PRIMARY KEY REFERENCES Cloth(username) ON DELETE CASCADE ON UPDATE CASCADE,cloth_id INTEGER PRIMARY KEY REFERENCES Cloth(id) ON DELETE CASCADE ON UPDATE CASCADE,category_id TEXT PRIMARY KEY REFERENCES Category(id) ON DELETE CASCADE ON UPDATE CASCADE";

            queryWithoutReturn(query);
        }
    }
}