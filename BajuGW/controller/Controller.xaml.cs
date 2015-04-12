using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BajuGW
{
    /**
     * Controller untuk menghubungkan antara GUI (view) dengan data (model).
     * Selain itu, controller juga berfungsi untuk menyimpan resource yang
     * dipakai bersama-sama (Database manager, dll).
     * 
     */
    public partial class Controller : Application
    {
        private LoginScreen loginScreen;
        private MainScreen mainScreen;
        private Account account;
        public static SQLiteManager dbmanager;
        public static string DBNAME = "data.db";


        /**
         * Constructor utama
         * 
         */
        public Controller()
        {
            if (!File.Exists(Controller.DBNAME))
            {
                dbmanager = new SQLiteManager(DBNAME);
                dbmanager.initDatabase();
            }
            else
            {
                dbmanager = new SQLiteManager(DBNAME);
            }

            /*dbmanager.queryWithoutReturn("insert into user values ('rebby', 'rebby','rebby@gmail.com',0.0,0.0,'','');");
            dbmanager.queryWithoutReturn(
                "insert into cloth values" +
                "('rebby', 0, 'kaos', 'fasilkom', 0, 'blue', 0.0, 0.0, 'data/0.png')," +
                "('rebby', 1, 'kemeja', 'fasilkom', 0, 'blue', 0.0, 0.0, 'data/1.png')," +
                "('rebby', 2, 'polo', 'fasilkom', 0, 'blue', 0.0, 0.0, 'data/2.png')," +
                "('rebby', 3, 'jubah', 'fasilkom', 0, 'blue', 0.0, 0.0, 'data/3.png')," +
                "('rebby', 4, 'zirah', 'fasilkom', 0, 'blue', 0.0, 0.0, 'data/4.png')," +
                "('rebby', 5, 'ponco', 'fasilkom', 0, 'blue', 0.0, 0.0, 'data/5.png')," +
                "('rebby', 6, 'gamis', 'fasilkom', 0, 'blue', 0.0, 0.0, 'data/6.png')," +
                "('rebby', 7, 'koko', 'fasilkom', 0, 'blue', 0.0, 0.0, 'data/7.png')," +
                "('rebby', 8, 'seragam', 'fasilkom', 0, 'blue', 0.0, 0.0, 'data/8.png')," +
                "('rebby', 9, 'chainmail', 'fasilkom', 0, 'blue', 0.0, 0.0, 'data/9.png')," +
                "('rebby', 10, 'v-neck', 'fasilkom', 0, 'blue', 0.0, 0.0, 'data/10.png')," +
                "('rebby', 11, 'kutang', 'fasilkom', 0, 'blue', 0.0, 0.0, 'data/11.png');"
            );*/

            mainScreen = new MainScreen(this);
            loginScreen = new LoginScreen(this);
            this.MainWindow = loginScreen;
            this.MainWindow.Show();

            new OnlineStore("mahal", "http://ppl-c06.cs.ui.ac.id");
        }


        /**
         * Tampilkan halaman login
         * 
         */
        public void showLoginScreen(Window caller) {
            this.MainWindow = loginScreen;
            this.MainWindow.Show();
            caller.Hide();
        }


        /**
         * Tampilkan halaman utama
         * 
         */
        public void showMainScreen(Window caller) {
            this.MainWindow = mainScreen;
            mainScreen.refresh();
            this.MainWindow.Show();
            caller.Hide();
        }


        /**
         * Cek apakah sesi login valid atau tidak. Jika valid maka buat
         * sebuah instance dari kelas Account untuk menyimpan informasi
         * dari akun.
         * 
         */
        public bool login(String username, String password)
        {
            Account account = Account.login(username, password);
            if (account != null)
            {
                this.account = account;
                return true;
            }
            return false;
        }

        public void logout()
        {
            this.account = null;
        }


        /**
         * Simpan akun yang baru saja diregister ke dalam database
         * 
         */
        public bool register(string username, string password, string email)
        {
            if (Account.register(username, password, email))
            {
                return true;
            }
            return false;
        }


        /**
         * Memfavoritkan pakaian yang diinginkan
         * 
         */
        public bool setFavorite(int id)
        {
            return account.setFavorite(id);
        }


        /**
         * Kembalikan daftar pakaian yang disarankan oleh BajuGW
         * 
         */
        public List<Cloth> getSuggestion()
        {
            return account.getSuggestion();
        }


        /**
         * Kembalikan daftar kategori yang dimiliki pengguna
         * 
         */
        public List<string> getCategories()
        {
            return account.getCategories();
        }


        /**
         * Kembalikan daftar pakaian yang sesuai dengan kategori yang diinginkan
         * 
         */
        public List<Cloth> getClothes(String category)
        {
            return account.getClothes(category);
        }


        /**
         * Kembalikan daftar pakaian yang sesuai dengan kriteria pencarian
         * 
         */
        public List<Cloth> searchWardrobe(String query)
        {
            return account.search(query);
        }


        /**
         * Kembalikan daftar pakaian yang disarankan oleh BajuGW
         * 
         */
        public bool deleteCloth(int id)
        {
            return account.deleteCloth(id);
        }


        /**
         * Buat sebuah pakaian baru
         * 
         */
        public bool addCloth(Cloth cloth)
        {
            return account.addCloth(cloth);
        }


        /**
         * Buat sebuah kategori baru
         * 
         */
        public bool addCategory(String category)
        {
            return account.addCategory(category);
        }


        /**
         * Hapus kategori
         * 
         */
        public bool deleteCategory(String category)
        {
            return account.deleteCategory(category);
        }


        /**
         * Ubah nama kategori yang diinginkan
         * 
         */
        public bool editCategory(String category, String newName)
        {
            return account.editCategory(category, newName);
        }
    }
}
