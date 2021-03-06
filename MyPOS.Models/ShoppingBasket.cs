using System;
using System.Collections.Generic;

namespace MyPOS.Models
{
    public class ShoppingBasket
    {
        public List<Purchase> Manifest;
        public double TaxSubtotal;

        public ShoppingBasket()
        {
            Manifest = new List<Purchase>();
            TaxSubtotal = 0;
        }

        public void Buy(Purchase purchase)
        {
            bool contains = false;
            foreach (var p in Manifest)
            {
                if (p.Name == purchase.Name) // .ToLowerInvariant())
                {
                    if (p.ListPrice == purchase.ListPrice)
                    {
                        contains = true;
                        p.Quantity += purchase.Quantity;
                    }
                }
            }
            if (!contains)
            {
                Manifest.Add(purchase);
            }
            TaxSubtotal += purchase.TaxCharged;
            PrintReceipt();
        }

        public void PrintReceipt()
        {
            double grandTotal = 0;

            var toPrint = new List<string>();
            foreach (var p in Manifest)
            {
                string nameToPrint = char.ToUpper(p.Name[0]) + p.Name.Substring(1);

                if (p.Quantity > 1)
                {
                    double extendedTotal = (p.ListPrice + p.TaxCharged) * p.Quantity;

                    toPrint.Add($"{nameToPrint}: {extendedTotal.ToString("F2")} ({p.Quantity} @ {(p.ListPrice + p.TaxCharged).ToString("F2")})");
                    grandTotal += extendedTotal;
                }
                else
                {
                    toPrint.Add($"{nameToPrint}: {(p.ListPrice + p.TaxCharged).ToString("F2")} ");
                    grandTotal += (p.ListPrice + p.TaxCharged);
                }
            }

            toPrint.Add($"Sales Taxes: {TaxSubtotal.ToString("F2")}");
            toPrint.Add($"Total: {grandTotal.ToString("F2")}");


            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            foreach (var item in toPrint)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }


    }
}
