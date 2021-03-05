using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
