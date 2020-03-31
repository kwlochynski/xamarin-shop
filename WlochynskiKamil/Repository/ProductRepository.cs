using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WlochynskiKamil.Models;

namespace WlochynskiKamil.Repository
{
    class ProductRepository
    {
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "myDb2.db3");
        //List<Product> products = new List<Product>();
        public void addProduct(Product product)
        {
            var db = new SQLiteConnection(dbPath);



            bool canAdd = true;
            foreach(Product p in getAllProducts())
            {
                if(p.Name == product.Name)
                {
                    canAdd = false;
                }
            }
           if (canAdd==true)
            {
                db.CreateTable<Product>();
                db.Insert(product);
            }

        }

        public List<Product> getAllProducts()
        {
            var db = new SQLiteConnection(dbPath);
            List<Product> products = new List<Product>();
            db.CreateTable<Product>();
            products = db.Table<Product>().ToList();
            return products;
        }

        public List<Product> getAllProductsByCategory(string category)
        {
            var db = new SQLiteConnection(dbPath);
            List<Product> products = new List<Product>();
            db.CreateTable<Product>();
            products = db.Table<Product>().Where(r => r.Category == category).ToList();
            return products;
        }
    }
}
