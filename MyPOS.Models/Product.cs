using System;

namespace MyPOS.Models
{
    public class Product
    {
        public string Name;
        public double ListPrice;

        public Product(string name, double listPrice)
        {
            Name = name;
            ListPrice = Math.Round(listPrice, 2);
        }
    }
}
