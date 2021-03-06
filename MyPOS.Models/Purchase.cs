using System;

namespace MyPOS.Models
{
    public class Purchase : Product
    {
        public int Quantity;
        public double TaxCharged;

        public Purchase(Product product, int quantity, double taxCharged) :base(name:product.Name, listPrice: product.ListPrice)
        {
            Quantity = quantity;
            TaxCharged = taxCharged;

        }
    }
}
