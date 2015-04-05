using System;
using System.Collections;
using System.Collections.Generic;

namespace BajuGW
{
    /**
     * Representasi data dari baju yang dimiliki pengguna
     * 
     */
    class Cloth
    {
        public int id;
        public string name;
        public int isFavorite;
        public double clothWidth;
        public double clothHeight;
        public string color;
        public string brand;
        public object picture; //TODO: Ubah menjadi kelas "Bitmap"
        public List<string> categories;


        /**
         * Constructor utama
         * 
         */
        public Cloth(int id, string name, string brand, int isFavorite, string color,
            double clothWidth, double clothHeight, object picture, List<string> categories)
        {
            this.id = id;
            this.name = name;
            this.brand = brand;
            this.isFavorite = isFavorite;
            this.color = color;
            this.clothWidth = clothWidth;
            this.clothHeight = clothHeight;
            this.picture = picture;
            this.categories = categories;
        }


        /**
         * Kembalikan detail dari baju
         * 
         */
		public Cloth getDetails()
		{
            return this;
		}
		

        /**
         * Membuat baju yang dipilih menjadi baju favorit pengguna
         * 
         */
		public void setFavorite()
        {
            this.isFavorite = 1;
		}


        /**
         * Membuat baju yang dipilih menjadi bukan baju favorit pengguna
         * 
         */
        public void unsetFavorite()
        {
            this.isFavorite = 0;
        }


        /**
         * Tambahkan kategori baju
         * 
         */
        public void addCategory(string category)
        {
            categories.Add(category);
        }


        /**
         * Menghapus salah satu kategori baju
         * 
         */
        public void removeCategory(string category)
        {
            categories.Remove(category);
        }
    }


    /**
     * Representasi data dari baju yang ada di toko online
     * 
     */
    class OnlineCloth : Cloth
    { 
	    private int store;
        private int price;


        /**
         * Constructor utama
         * 
         */
        public OnlineCloth(int id, string name, string brand, int isFavorite,
            string color, double clothWidth, double clothHeight, object picture,
            List<string> categories, int store, int price) :
            base (id, name, brand, isFavorite, color, clothWidth, clothHeight,
            picture, categories)
        {
            this.store = store;
            this.price = price;
        }
    }
}
