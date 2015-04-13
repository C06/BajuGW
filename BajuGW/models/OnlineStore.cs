using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Specialized;

namespace BajuGW
{
    public class OnlineStore
    {
        public int id;
        public string address;
        private List<OnlineCloth> clothes;
        public static List<string> categories = new List<string>(new string[] { "all" });
        private string username;

        /**
         * Constructor utama
         * 
         */
        public OnlineStore(int id, string address, string username)
        {
            this.id = id;
            this.address = address;
            this.username = username;
            refresh();
        }

        public void refresh()
        {
            this.clothes = new List<OnlineCloth>();

            bool exists = System.IO.Directory.Exists("./temp");

            if (!exists)
                System.IO.Directory.CreateDirectory("./temp");

            

            Directory.SetCurrentDirectory("./temp");
            using (WebClient Client = new WebClient())
            {
                Client.DownloadFile(this.address + "/json1.php", this.id + "_clothes.json");
                using (System.IO.StreamReader r = new System.IO.StreamReader(this.id + "_clothes.json"))
                {
                    string json = r.ReadToEnd();
                    clothes = JsonConvert.DeserializeObject<List<OnlineCloth>>(json);
                }

                foreach (OnlineCloth cloth in clothes)
                {
                    string filename = cloth.picture_path.Replace("/", this.id + "_");

                    if (!File.Exists(filename))
                    {
                        Client.DownloadFile(this.address + cloth.picture_path, filename);

                    }
                    cloth.picture = new BitmapImage(new Uri("./temp/" + filename, UriKind.Relative));
                    cloth.picture_path = "./temp/" + filename;

                    foreach (string cat in cloth.category)
                    {
                        if (!OnlineStore.categories.Contains(cat))
                            OnlineStore.categories.Add(cat);
                    }
                }
            }
            Directory.SetCurrentDirectory("./..");

            SQLiteManager dbmanager = Controller.dbmanager;

            foreach (OnlineCloth cloth in clothes)
            {
                string query = "select cloth_id from favorite_online_cloth where username='" +
                        this.username + "' and store_id=" + this.id + " and cloth_id=" + cloth.id+";";
                foreach (NameValueCollection row in dbmanager.queryWithReturn(query))
                {
                    cloth.isFavorite = 1;
                }
            }
            
            
        }


        /**
         * Kembalikan pakaian yang dicari
         * 
         */
        OnlineCloth getOnlineCloth(int id)
        {
            int i = 0;
            foreach (OnlineCloth cloth in clothes)
            {
                if (cloth.id == id)
                    break;
                i++;
            }
            if (i < clothes.Count)
                return clothes[i];
            return null;
        }


        /**
         * Kembalikan daftar pakaian yang sesuai dengan kategori yang dicari
         * 
         */
        List<OnlineCloth> getOnlineClothes(String category)
        {
            if (category.Equals(""))
                return clothes;

            List<OnlineCloth> result = new List<OnlineCloth>();

            foreach (OnlineCloth cloth in clothes)
            {
                if (cloth.category.Contains(category))
                {
                    result.Add(cloth);
                }
            }

            return result;
		}


        /**
         * Menandakan pakaian yang diinginkan sebagai pakaian favorit
         * 
         */
		public bool setFavorite(Account account, int id)
        {
            OnlineCloth result = getOnlineCloth(id);
            if (result == null)
                return false;

            result.setFavorite();
            return true;
		}
		

        /**
         * Membeli sebuah pakaian yang tersedia di toko online tujuan
         * 
         */
		public bool buy(Account account, int id)
        {
            Cloth newCloth = new Cloth(getOnlineCloth(id));
            return account.addCloth(newCloth);
		}


        /**
         * Mencari baju yang tersedia di suatu toko online berdasarkan id
         * 
         */
        List<OnlineCloth> search(String query)
        {
            List<OnlineCloth> result = new List<OnlineCloth>();
            foreach (OnlineCloth cloth in clothes)
            {
                if (cloth.name.Contains(query))
                    result.Add(cloth);
            }
            return result;
		}


        public static bool checkConnection(string url)
        {
            var ping = new System.Net.NetworkInformation.Ping();
            var result = ping.Send(url);
            if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
                return false;
            return true;
        }

        internal List<OnlineCloth> getClothes(string query, string category)
        {
            List<OnlineCloth> clothes = getOnlineClothes(category);
            List<OnlineCloth> result = new List<OnlineCloth>();
            foreach (OnlineCloth cloth in clothes)
            {
                if (cloth.name.Contains(query))
                    result.Add(cloth);
            }
            return result;
        }

        public void setOnlineFavorite(int cloth)
        {
            SQLiteManager dbmanager = Controller.dbmanager;
            string query = "insert into favorite_online_cloth values ('"+username+"'," +id +", " + cloth+");";
            dbmanager.queryWithoutReturn(query);
        }

        public void setOnlineUnfavorite(int cloth)
        {
            SQLiteManager dbmanager = Controller.dbmanager;
            string query = "delete from favorite_online_cloth where username='"+
                username+"' and store_id=" + id + " and cloth_id="+cloth+";";
            dbmanager.queryWithoutReturn(query);
        }

    }
}
