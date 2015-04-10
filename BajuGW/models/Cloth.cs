using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

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
        public BitmapSource picture;
        public string picture_path;
        public List<string> categories;


        /**
         * Constructor utama
         * 
         */
        public Cloth(int id, string name, string brand, int isFavorite, string color,
            double clothWidth, double clothHeight, string picture_path, List<string> categories)
        {
            this.id = id;
            this.name = name;
            this.brand = brand;
            this.isFavorite = isFavorite;
            this.color = color;
            this.clothWidth = clothWidth;
            this.clothHeight = clothHeight;
            this.picture_path = picture_path;
            this.picture = new BitmapImage(new Uri(picture_path));
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
            string color, double clothWidth, double clothHeight, string picture_path,
            List<string> categories, int store, int price) :
            base (id, name, brand, isFavorite, color, clothWidth, clothHeight,
            picture_path, categories)
        {
            this.store = store;
            this.price = price;
        }
    }
}
