using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            TaxSubtotal += Math.Round(purchase.TaxCharged,2);
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

                    toPrint.Add($"{nameToPrint}: {extendedTotal.ToString("F2")} ({p.Quantity} @ {(p.ListPrice + p.TaxCharged).ToString("F2")} )");
                    //toPrint.Add($"{nameToPrint}: {extendedTotal.ToString()} ({p.Quantity} @ {Math.Round((p.ListPrice + p.TaxCharged),2).ToString()} )");
                    grandTotal += extendedTotal;
                }
                else
                {
                    toPrint.Add($"{nameToPrint}: {(p.ListPrice + p.TaxCharged).ToString("F2")} ");
                    grandTotal += (p.ListPrice + p.TaxCharged);
                }
            }

            toPrint.Add($"Sales Taxes: {TaxSubtotal.ToString("F2")}");
            //toPrint.Add($"Sales Taxes: {Math.Round(TaxSubtotal,2).ToString()}");


            //toPrint.Add($"Total: {grandTotal.ToString("F2")}");
            toPrint.Add($"Total: {Math.Round(grandTotal, 2)}");
            //toPrint.Add($"Total: {Math.Round(grandTotal,2).ToString()}");

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
