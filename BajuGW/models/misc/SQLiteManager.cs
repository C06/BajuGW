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
                "Data Source=./" + databaseName +";Version=3;foreign keys=true;");
            
            sqlCommand = new SQLiteCommand(sqlConnection);
            connect();
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
                    new System.IO.StreamWriter("db.log", true))
                {
                    file.WriteLine(exception.Message);
                }
                return false;
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
                    new System.IO.StreamWriter("db.log", true))
                {
                    file.WriteLine(exception.Message);
                }

                yield break;
            }
            while (reader.Read()) {
                yield return reader.GetValues();
            }
            reader.Dispose();
            yield break;
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
        

        //TODO: tambahkan tabel online store favorit
        /**
         * Buat tabel yang akan dipakai
         * 
         */
        public void initDatabase()
        {
            string query =
                "CREATE TABLE User (username VARCHAR(10) PRIMARY KEY,password VARCHAR(10) NOT NULL,email TEXT NOT NULL,cloth_width NUMERIC,cloth_height NUMERIC,picture_path TEXT,theme TEXT);" +
                "CREATE TABLE Store (id INTEGER PRIMARY KEY);" +
                "CREATE TABLE User_Activate_Store (username VARCHAR(10),store_id INTEGER,PRIMARY KEY (username, store_id),FOREIGN KEY (username) REFERENCES user(username),FOREIGN KEY (store_id) REFERENCES store(id));" +
                "CREATE TABLE Cloth (username VARCHAR(10),id INTEGER,name TEXT,brand TEXT,favorite BOOLEAN,color TEXT,cloth_width NUMERIC NOT NULL,cloth_height NUMERIC NOT NULL,picture_path TEXT NOT NULL,PRIMARY KEY (username, id),FOREIGN KEY (username) REFERENCES user(username));" +
                "CREATE TABLE Category (username VARCHAR(10),id TEXT,PRIMARY KEY (username, id),FOREIGN KEY (username) REFERENCES user(username));" +
                "CREATE TABLE Cloth_Has_Category (username VARCHAR(10),cloth_id INTEGER,creator VARCHAR(10),category_id TEXT,PRIMARY KEY (username, cloth_id, category_id),FOREIGN KEY (username, cloth_id) REFERENCES cloth(username, id),FOREIGN KEY (creator, category_id) REFERENCES cateogry(username, id));" +
                "create table favorite_online_cloth (username VARCHAR(10), store_id INTEGER, cloth_id INTEGER, PRIMARY KEY (username, store_id, cloth_id), FOREIGN KEY (username) REFERENCES user(username),FOREIGN KEY (store_id) REFERENCES store(id));"+
                "insert into store values (0), (1), (2);";

            queryWithoutReturn(query);
        }
    }
}