using System;
using System.Collections.Generic;
using System.Text;

namespace WlochynskiKamil.Models
{
    class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Path { get; set; }
        public string Path2 { get; set; }

        public string Path3 { get; set; }

        public string Category { get; set; }

        public string Size { get; set; }

/*        public Product(string Name, int Price, string Path, string Category)
        {
            this.Name = Name;
            this.Price = Price;
            this.Path = Path;
        }*/
    }
}
