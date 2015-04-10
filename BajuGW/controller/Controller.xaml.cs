using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;
using System.Collections.Generic;

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
            dbmanager = new SQLiteManager(DBNAME);
            dbmanager.initDatabase();

            mainScreen = new MainScreen(this);
            loginScreen = new LoginScreen(this);
            this.MainWindow = loginScreen;
            this.MainWindow.Show();
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


        /**
         * Simpan akun yang baru saja diregister ke dalam database
         * 
         */
        public bool register(string username, string password, string email)//, Object face)
        {
            if (Account.register(username, password, email))//, face))
            {
                return true;
            }
            return false;
        }


        /**
         * Memfavoritkan pakaian yang diinginkan
         * 
         */
        bool setfavorite(int id)
        {
            return account.setFavorite(id);
        }


        /**
         * Kembalikan daftar pakaian yang disarankan oleh BajuGW
         * 
         */
        List<Cloth> getSuggestion()
        {
            return account.getSuggestion();
        }


        /**
         * Kembalikan daftar kategori yang dimiliki pengguna
         * 
         */
        List<string> getCategories()
        {
            return account.getCategories();
        }


        /**
         * Kembalikan daftar pakaian yang sesuai dengan kategori yang diinginkan
         * 
         */
        List<Cloth> getClothes(String category)
        {
            return account.getClothes(category);
        }


        /**
         * Kembalikan daftar pakaian yang sesuai dengan kriteria pencarian
         * 
         */
        List<Cloth> searchWardrobe(String query)
        {
            return account.search(query);
        }


        /**
         * Kembalikan daftar pakaian yang disarankan oleh BajuGW
         * 
         */
        bool deleteCloth(int id)
        {
            return account.deleteCloth(id);
        }


        /**
         * Buat sebuah pakaian baru
         * 
         */
        bool addCloth(Cloth cloth)
        {
            return account.addCloth(cloth);
        }


        /**
         * Buat sebuah kategori baru
         * 
         */
        bool addCategory(String category)
        {
            return account.addCategory(category);
        }


        /**
         * Hapus kategori
         * 
         */
        bool deleteCategory(String category)
        {
            return account.deleteCategory(category);
        }


        /**
         * Ubah nama kategori yang diinginkan
         * 
         */
        bool editCategory(String category, String newName)
        {
            return account.editCategory(category, newName);
        }
    }
}
