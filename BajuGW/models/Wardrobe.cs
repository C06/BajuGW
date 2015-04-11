using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace BajuGW
{
    /**
     * Kelas untuk menyimpan baju-baju yang dimiliki oleh pengguna
     * 
     */
    class Wardrobe
    {
        private List<Cloth> clothes;
        private List<string> categories;
        private string username;


        /**
         * Constructor utama dari kelas wardrobe
         * 
         */
        public Wardrobe(string username)
        {
            this.username = username;
            refresh();
        }


        /**
         * Muat ulang baju-baju di wardrobe
         * 
         */
        public void refresh()
        {
            this.clothes = new List<Cloth>();
            this.categories = new List<string>();

            SQLiteManager dbmanager = Controller.dbmanager;

            string query = "select id, name, brand, favorite, color, cloth_width, " +
                "cloth_height, picture_path from cloth where username='" +
                this.username + "'";
            foreach (NameValueCollection row in dbmanager.queryWithReturn(query))
            {
                int id = int.Parse(row["id"]);
                string name = row["name"];
                string brand = row["brand"];
                int isFavorite = int.Parse(row["favorite"]);
                string color = row["color"];
                double clothWidth = double.Parse(row["cloth_width"]);
                double clothHeight = double.Parse(row["cloth_height"]);
                string picture_path = row["picture_path"];
                clothes.Add(new Cloth(id, name, brand, isFavorite, color,
                    clothWidth, clothHeight, picture_path, new List<string>()));
            }

            foreach (Cloth cloth in clothes)
            {
                query = "select category_id from cloth_has_category where username='" +
                    this.username + "' and cloth_id='" + cloth.id + "'";
                foreach (NameValueCollection row in dbmanager.queryWithReturn(query))
                {
                    cloth.addCategory(row["category_id"]);
                }
            }

            query = "select id from category where username='" + this.username + "'";
            foreach (NameValueCollection row in dbmanager.queryWithReturn(query))
            {
                categories.Add(row["id"]);
            }
        }
		

        /**
         * Kembalikan daftar pakaian yang disarankan ke pengguna
         * 
         */
        public List<Cloth> getSuggestion()
        {
            List<Cloth> suggestion = new List<Cloth>();
            Random random = new Random();

            int i = 5;
            int n;
            List<int> blacklist = new List<int>();
            while (i > 0)
            {
                n = random.Next(clothes.Count);
                if (!blacklist.Contains(n))
                {
                    suggestion.Add(clothes[n]);
                    blacklist.Add(n);
                    n--;
                }
            }
            
            return suggestion;
		}
		

        /**
         * Kembalikan daftar kategori yang dimiliki oleh pengguna
         * 
         */
        public List<String> getCategories()
        {
            return categories;
		}
		

        /**
         * Kembalikan daftar pakaian yang termasuk ke dalam kategori yang dicari
         * 
         */
        public List<Cloth> getClothes(string category)
        {
            if (category.Equals(""))
                return clothes;

            List<Cloth> result = new List<Cloth>();

            foreach (Cloth cloth in clothes)
            {
                if (cloth.categories.Equals(category))
                {
                    result.Add(cloth);
                }
            }

            return result;
		}


        /**
         * Kembalikan pakaian yang dicari
         * 
         */
        public Cloth getCloth(int id)
        {
            Cloth result = null;
            foreach (Cloth cloth in clothes)
            {
                if (cloth.id == id)
                {
                    result = cloth;
                    break;
                }
            }
            return result;
        }


        /**
         * Tambahkan pakaian baru ke database
         * 
         */
        public bool addCloth(Cloth cloth)
        {
            SQLiteManager dbmanager = Controller.dbmanager;

            int max = 0;

            string query = "select max(id) from cloth where username='" + username + "'";
            foreach (NameValueCollection row in dbmanager.queryWithReturn(query))
            {
                if (("" + row[0]).Equals(""))
                    break;
                max = int.Parse(row[0]);
            }

            query = "insert into cloth values ('" + username + "', " + max+1 + ",'" +
                cloth.name + "','" + cloth.brand + "'," + cloth.isFavorite + ",'" +
                cloth.color + "'," + cloth.clothWidth + "," + cloth.clothHeight +
                ",'" + cloth.picture_path + "')";

            bool status = dbmanager.queryWithoutReturn(query);
            if (!status)
                return false;

            refresh();

            return true;
        }


        /**
         * Kembalikan daftar pakaian yang sesuai dengan kriteria pencarian
         * 
         */
        public List<Cloth> search(string query)
        {
            List<Cloth> result = new List<Cloth>();
            foreach (Cloth cloth in clothes)
            {
                if (cloth.name.Contains(query))
                    result.Add(cloth);
            }
            return result;
		}


        /**
         * Hapus pakaian dari database
         * 
         * 
         */
        public bool deleteCloth(int id)
        {
            SQLiteManager dbmanager = Controller.dbmanager;
            

            string query = "delete from cloth where username='" + username +
                "' and id=" + id;
            bool status = dbmanager.queryWithoutReturn(query);
            

            if (!status)
                return false;

            int i = 0;
            foreach (Cloth cloth in clothes)
            {
                if (cloth.id == id)
                    break;
                i++;
            }
            if (i < clothes.Count)
                clothes.RemoveAt(i);

            return true;
		}


        /**
         * Tambah kategori baru ke database
         * 
         */
        public bool addCategory(String category)
        {
            SQLiteManager dbmanager = Controller.dbmanager;
            
            
            string query = "insert into category values('" + username + "','" +
                category + "')";
            bool status = dbmanager.queryWithoutReturn(query);
            

            if (!status)
                return false;

            categories.Add(category);
            return true;
		}


        /**
         * Hapus kategori dari database
         * 
         */
        public bool deleteCategory(String category)
        {
            SQLiteManager dbmanager = Controller.dbmanager;
            

            string query = "delete from category where username='" + username +
                "' and id='" + category + "'";
            bool status = dbmanager.queryWithoutReturn(query);
            

            if (!status)
                return false;

            int i = 0;
            foreach (String id in categories)
            {
                if (id.Equals(category))
                    break;
                i++;
            }
            if (i < categories.Count)
                categories.RemoveAt(i);

            return true;
		}


        /**
         * Ubah kategori yang ada di database
         * 
         */
        public bool editCategory(String category, String newName)
        {
            SQLiteManager dbmanager = Controller.dbmanager;
            

            string query = "update category id='"+ newName + "' where username='" +
                username + "' and id='" + category + "'";
            bool status = dbmanager.queryWithoutReturn(query);
            

            if (!status)
                return false;

            int i = 0;
            foreach (String id in categories)
            {
                if (id.Equals(category))
                    break;
                i++;
            }
            if (i < categories.Count)
                categories[i] = newName;

            return true;
		}


        /**
         * Ubah pakaian menjadi pakaian favorit pengguna
         * 
         */
        public bool setFavorite(int id)
        {
            SQLiteManager dbmanager = Controller.dbmanager;
            

            string query = "update cloth favorite=" + 1 + " where username='" +
                username + "' and id=" + id;
            bool status = dbmanager.queryWithoutReturn(query);
            

            if (!status)
                return false;

            int i = 0;
            foreach (Cloth cloth in clothes)
            {
                if (cloth.id == id)
                    break;
                i++;
            }
            if (i < clothes.Count)
                clothes[i].isFavorite = 1;
            
            return true;
        }
    }
}
