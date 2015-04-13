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
        public static List<OnlineStore> stores;
        public static string DBNAME = "data.db";
        public static string[] supportedStore = { "http://ppl-c06.cs.ui.ac.id" };

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
            
            mainScreen = new MainScreen(this);
            loginScreen = new LoginScreen(this);
            this.MainWindow = loginScreen;
            this.MainWindow.Show();
        }

        public string getUsername()
        {
            return account.username;
        }

        public bool buyCloth(int store, int cloth)
        {
            return stores[store].buy(account, cloth);
        }

        public List<string> getOnlineCategories()
        {
            return OnlineStore.categories;
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
            mainScreen.refreshStore();
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

        public bool login(String username)
        {
            Account account = Account.login(username);
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
            mainScreen = new MainScreen(this);
            loginScreen = new LoginScreen(this);
        }


        /**
         * Simpan akun yang baru saja diregister ke dalam database
         * 
         */
        public bool register(string username, string password, string email, int uid)
        {
            if (Account.register(username, password, email, uid))
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


        public bool setUnfavorite(int id)
        {
            return account.setUnfavorite(id);
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


        public void addConnectedStore(int id)
        {
            account.addConnectedStore(id);
        }


        public void removeConnectedStore(int id)
        {
            account.removeConnectedStore(id);
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


        public void refresh()
        {
            account.refresh();
        }


        public List<Cloth> getFavorites()
        {
            return account.getFavorites();
        }


        public List<Cloth> getClothesFromWardrobe(string query, string category)
        {
            return account.getClothes(query, category);
        }


        public List<OnlineCloth> getClothesFromOnlineStore(int id, string query, string category)
        {
            return stores[id].getClothes(query, category);
        }

        public bool setClothCategory(int id, string category)
        {
            return account.setClothCategory(id, category);
        }

        public void setOnlineFavorite(int store, int cloth)
        {
            stores[store].setOnlineFavorite(cloth);
        }

        public void setOnlineUnfavorite(int store, int cloth)
        {
            stores[store].setOnlineUnfavorite(cloth);
        }

        internal void refreshStore()
        {
            foreach (OnlineStore store in stores)
            {
                if (store != null)
                    store.refresh();
            }
        }
    }
}
