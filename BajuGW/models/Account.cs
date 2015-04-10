using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace BajuGW
{
    /**
     * Kelas untuk menyimpan semua data dari akun yang sedang terhubung (login)
     * ke BajuGW
     * 
     */
    class Account
    {
        private string username;
        private string email;
        private double clothWidth;
        private double clothHeight;
        private Object face; //TODO: ubah ke kelas "Bitmap"
        private string theme;
        
        private Wardrobe wardrobe;
        private List<int> connectedStore;


        /**
         * Constructor utama
         * 
         */
        public Account(String username, String email, double clothWidth,
            double clothHeight, Object face, //TODO: ubah ke kelas "Bitmap"
            string theme, List<int> connectedStore)
        {
            this.username = username;
            this.email = email;
            this.clothWidth = clothWidth;
            this.clothHeight = clothHeight;
            this.face = face;
            this.theme = theme;
            this.wardrobe = new Wardrobe(username);
            this.connectedStore = connectedStore;
        }


        //TODO: ubah return type
        /**
         * Daftarkan akun yang baru saja melakukan register
         * 
         */
        public static bool register(string username, string password, string email)//, Object face)
        {
            Tuple<string, string> hashed = encrypt(password);
            if (hashed == null)
                return false;

            SQLiteManager manager = Controller.dbmanager;
            Console.WriteLine(manager);
            manager.connect();

            string query = "insert into User values (" + username + "," +
                hashed.Item1 + "," + hashed.Item2 + "," + email + "," + 0.0 + "," +
                0.0 + ", null, null";
            manager.queryWithoutReturn(query);
            manager.disconnect();

            return true;
        }


        /**
         * Cek apakah sesi login valid atau tidak
         * 
         */
        public static Account login(string username, string password)
        {
            SQLiteManager manager = Controller.dbmanager;
            manager.connect();

            string
                email = "",
                theme = "",
                salt = "",
                hashedPassword = "";
            double
                clothWidth = 0.0,
                clothHeight = 0.0;
            Object face = null; //TODO: ubah menjadi kelas "Bitmap"
            List<int> connectedStore = new List<int>();

            string query = "select * from user where username='" + username + "'";
            foreach (NameValueCollection row in manager.queryWithReturn(query))
            {
                salt = row["salt"];
                hashedPassword = row["password"];
                email = row["email"];
                clothWidth = double.Parse(row["cloth_width"]);
                clothHeight = double.Parse(row["cloth_height"]);
                face = row["face"]; //TODO: ubah menjadi kelas "Bitmap"
                theme = row["theme"];
            }

            if (!decrypt(hashedPassword, salt).Equals(password))
                return null;

            query = "select store_id from User_Activate_Store where username='" + username + "'";
            foreach (NameValueCollection row in manager.queryWithReturn(query))
            {
                connectedStore.Add(int.Parse(row["store_id"]));
            }

            manager.disconnect();

            return new Account(username, email, clothWidth, clothHeight, face, theme, connectedStore);
        }


        //TODO: Implementasi method untuk mendekripsi password
        /**
         * Dekripsi string berdasarkan password dan salt
         * 
         */
        public static string decrypt(string password, string salt)
        {
            return password + salt;
        }

        
        //TODO: Implementasi method untuk mengenkripsi password
        public static Tuple<string, string> encrypt(string password)
        {
            if (password.Length < 6)
                return null;

            string hashed = password.Substring(0, password.Length - 3);
            string salt = password.Substring(3);
            Tuple<string, string> result = new Tuple<string, string>(hashed, salt);

            return result;
        }

        /**
         * Memfavoritkan pakaian yang diinginkan
         * 
         */
		public bool setfavorite(int id)
        {
            return wardrobe.setFavorite(id);
		}
		

        /**
         * Kembalikan daftar pakaian yang disarankan oleh BajuGW
         * 
         */
        public List<Cloth> getSuggestion()
        {
            return wardrobe.getSuggestion();
		}


        /**
         * Kembalikan daftar kategori yang dimiliki pengguna
         * 
         */
        public List<string> getCategories()
        {
            return wardrobe.getCategories();
		}


        /**
         * Kembalikan daftar pakaian yang sesuai dengan kategori yang diinginkan
         * 
         */
        public List<Cloth> getClothes(string category)
        {
            return wardrobe.getClothes(category);
		}


        /**
         * Kembalikan daftar pakaian yang sesuai dengan kriteria pencarian
         * 
         */
        public List<Cloth> searchWardrobe(string query)
        {
            return wardrobe.search(query);
		}


        /**
         * Kembalikan daftar pakaian yang disarankan oleh BajuGW
         * 
         */
		public bool deleteCloth(int id) {
            return wardrobe.deleteCloth(id);
		}


        /**
         * Buat sebuah pakaian baru
         * 
         */
        public bool addCloth(Cloth cloth)
        {
            return wardrobe.addCloth(cloth);
		}


        /**
         * Buat sebuah kategori baru
         * 
         */
		public bool addCategory(string category) {
            return wardrobe.addCategory(category);
		}
		

        /**
         * Hapus kategori
         * 
         */
		public bool deleteCategory(string category) {
            return wardrobe.deleteCategory(category);
		}
		

        /**
         * Ubah nama kategori yang diinginkan
         * 
         */
		public bool editCategory(string category, string newName) {
            return wardrobe.editCategory(category, newName);
		}


        /**
         * Buat pakaian yang dipilih menjadi pakaian favorit pengguna
         * 
         */
        public bool setFavorite(int id)
        {
            return wardrobe.setFavorite(id);
        }


        /**
         * Kembalikan daftar pakaian yang sesuai dengan kriteria pencarian
         * 
         */
        public List<Cloth> search(string query)
        {
            return wardrobe.search(query);
        }
    }
}
