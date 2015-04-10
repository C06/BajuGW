using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Net;
using Newtonsoft.Json;

namespace BajuGW
{
    class OnlineStore
    {
        private string name;
        private string address;
        private List<OnlineCloth> clothes;


        /**
         * Constructor utama
         * 
         */
        public OnlineStore(string name, string address)
        {
            this.name = name;
            this.address = address;
            this.clothes = new List<OnlineCloth>();

            using (WebClient Client = new WebClient())
            {
                Client.DownloadFile(address, "clothes.json");
                using (System.IO.StreamReader r = new System.IO.StreamReader("file.json"))
                {
                    string json = r.ReadToEnd();
                    clothes = JsonConvert.DeserializeObject<List<OnlineCloth>>(json);
                }
                
                foreach (OnlineCloth cloth in clothes) {
                    string filename = this.name + "_" +cloth.id+".jpg";
                    Client.DownloadFile(address+cloth.picture_path, filename);
                    cloth.picture = new BitmapImage(new Uri(filename));
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
                if (cloth.categories.Contains(category))
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
		bool setFavorite(int id)
        {
            OnlineCloth result = getOnlineCloth(id);
            if (result == null)
                return false;

            result.setFavorite();
            return true;
		}
		

        //TODO: selesaikan method ini
        /**
         * Membeli sebuah pakaian yang tersedia di toko online tujuan
         * 
         * 
         */
		bool buy(Account account, int id)
        {
            return false;
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


        bool connect()
        {
            return false;
        }


        bool disconnect()
        {
            return false;
        }


        bool checkConnection()
        {
            return false;
        }
    }
}
