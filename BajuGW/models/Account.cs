using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

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
        private BitmapSource picture = null;
        private string theme;
        
        private Wardrobe wardrobe;
        private List<int> connectedStore;


        /**
         * Constructor utama
         * 
         */
        public Account(String username, String email, double clothWidth,
            double clothHeight, string picture_path, string theme,
            List<int> connectedStore)
        {
            this.username = username;
            this.email = email;
            this.clothWidth = clothWidth;
            this.clothHeight = clothHeight;

            if (!picture_path.Equals(""))
                this.picture = new BitmapImage(new Uri(picture_path));
            
            this.theme = theme;
            this.wardrobe = new Wardrobe(username);
            this.connectedStore = connectedStore;
        }

        
        /**
         * Daftarkan akun yang baru saja melakukan register
         * 
         */
        public static bool register(string username, string password, string email)
        {
            SQLiteManager manager = Controller.dbmanager;
            
            string query = "insert into User values ('" + username + "','" +
                password + "','" + email + "'," + 0.0 + "," +
                0.0 + ", '', '')";
            
            bool result = manager.queryWithoutReturn(query);
            
            if (result)
                return true;
            return false;
        }


        /**
         * Cek apakah sesi login valid atau tidak
         * 
         */
        public static Account login(string username, string password)
        {
            SQLiteManager manager = Controller.dbmanager;
            
            string
                email = "",
                theme = "",
                realPassword = "",
                picture_path = "";
            double
                clothWidth = 0.0,
                clothHeight = 0.0;
            List<int> connectedStore = new List<int>();

            string query = "select * from user where username='" + username + "'";
            foreach (NameValueCollection row in manager.queryWithReturn(query))
            {
                realPassword = row["password"];
                email = row["email"];
                clothWidth = double.Parse(row["cloth_width"]);
                clothHeight = double.Parse(row["cloth_height"]);
                picture_path = row["picture_path"];
                theme = row["theme"];
            }

            if (!realPassword.Equals(password))
                return null;
            
            query = "select store_id from User_Activate_Store where username='" + username + "'";
            foreach (NameValueCollection row in manager.queryWithReturn(query))
            {
                connectedStore.Add(int.Parse(row["store_id"]));
            }

            return new Account(username, email, clothWidth, clothHeight, picture_path, theme, connectedStore);
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
