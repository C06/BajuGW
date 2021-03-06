﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace BajuGW
{
    /**
     * Representasi data dari baju yang dimiliki pengguna
     * 
     */
    public class Cloth
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
        public List<string> category;


        /**
         * Constructor utama
         * 
         */
        public Cloth(int id, string name, string brand, int isFavorite, string color,
            double clothWidth, double clothHeight, string picture_path, List<string> category)
        {
            this.id = id;
            this.name = name;
            this.brand = brand;
            this.isFavorite = isFavorite;
            this.color = color;
            this.clothWidth = clothWidth;
            this.clothHeight = clothHeight;
            this.picture_path = picture_path;
            this.picture = new BitmapImage(new Uri(picture_path, UriKind.Relative));
            this.category = category;
        }


        public Cloth(OnlineCloth cloth)
        {
            this.id = cloth.id;
            this.name = cloth.name;
            this.brand = cloth.brand;
            this.isFavorite = 0;
            this.color = cloth.color;
            this.clothWidth = cloth.clothWidth;
            this.clothHeight = cloth.clothHeight;

            string newPath = cloth.picture_path.Replace("./temp/", "./data/");
            File.Copy(cloth.picture_path, newPath);
            this.picture_path = newPath;
            this.picture = new BitmapImage(new Uri(this.picture_path, UriKind.Relative));

            this.category = cloth.category;
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
            this.category.Add(category);
        }


        /**
         * Menghapus salah satu kategori baju
         * 
         */
        public void removeCategory(string category)
        {
            this.category.Remove(category);
        }
    }


    /**
     * Representasi data dari baju yang ada di toko online
     * 
     */
    public class OnlineCloth : Cloth
    { 
	    public int store;
        public int price;


        /**
         * Constructor utama
         * 
         */
        public OnlineCloth(int id, string name, string brand, int isFavorite,
            string color, double clothWidth, double clothHeight, string picture_path,
            List<string> category, int store, int price) :
            base (id, name, brand, isFavorite, color, clothWidth, clothHeight,
            picture_path, category)
        {
            this.store = store;
            this.price = price;
        }
    }
}
