using MyPOS.Models;
using MyPOS.Services.ShoppingMethod;
using MyPOS.Services.Tax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Xunit;

namespace MyPOS.Test
{
    public class TestDealerOnInputs
    {
        [Fact]
        public void Input1()
        {
            var TaxService = new DefaultTaxService(salesTaxRate: 0.1, importDutyRate: 0.05, roundTo: .05);
            string[] TaxExemptWords = new string[] { "book", "chocolate", "headache", "pills" };
            var ShoppingMethod = new ManualTextEntry(ConsoleColor.Red, ConsoleColor.Red, TaxService, TaxExemptWords, "at", "import");

            var inputs = new List<string>() 
            {
                "1 Book at 12.49",
                "1 Book at 12.49",
                "1 Music CD at 14.99",
                "1 Chocolate bar at 0.85"
            };

            double sut = 0;
            foreach (var input in inputs)
            {
                var p = ShoppingMethod.ParsePurchase(input);
                sut += p.ListPrice + p.TaxCharged;
            }
            Assert.Equal(42.32, sut);
        }

        [Fact]
        public void Input2()
        {
            var TaxService = new DefaultTaxService(salesTaxRate: 0.1, importDutyRate: 0.05, roundTo: .05);
            string[] TaxExemptWords = new string[] { "book", "chocolate", "headache", "pills" };
            var ShoppingMethod = new ManualTextEntry(ConsoleColor.Red, ConsoleColor.Red, TaxService, TaxExemptWords, "at", "import");

            var inputs = new List<string>()
            {
                "1 Imported box of chocolates at 10.00",
                "1 Imported bottle of perfume at 47.50"
            };

            double sut = 0;
            foreach (var input in inputs)
            {
                var p = ShoppingMethod.ParsePurchase(input);
                sut += p.ListPrice + p.TaxCharged;
            }
            Assert.Equal(65.15, sut);
        }

        [Fact]
        public void Input3()
        {
            var TaxService = new DefaultTaxService(salesTaxRate: 0.1, importDutyRate: 0.05, roundTo: .05);
            string[] TaxExemptWords = new string[] { "book", "chocolate", "headache", "pills" };
            var ShoppingMethod = new ManualTextEntry(ConsoleColor.Red, ConsoleColor.Red, TaxService, TaxExemptWords, "at", "import");

            var inputs = new List<string>()
            {
                "1 Imported bottle of perfume at 27.99",
                "1 Bottle of perfume at 18.99",
                "1 Packet of headache pills at 9.75",
                "1 Imported box of chocolates at 11.25",
                "1 Imported box of chocolates at 11.25"
            };

            var purchases = new List<Purchase>();
            foreach (var input in inputs)
            {
                purchases.Add(ShoppingMethod.ParsePurchase(input));                
            }

            ShoppingBasket shoppingBasket = new ShoppingBasket();
            foreach (var p in purchases)
            {
                shoppingBasket.Buy(p);
                //if(p.ListPrice % 0.01 != 0 || p.TaxCharged % TaxService.RoundTo != 0)
                //{
                //    Debug.WriteLine("What's going on, here?");
                //}
            }

            double sut = 0;
            foreach (var p in shoppingBasket.Manifest)
            {
                sut += (p.ListPrice + p.TaxCharged) * p.Quantity;
                if (p.ListPrice % 0.01 != 0 || p.TaxCharged % TaxService.RoundTo != 0)
                {
                    Debug.WriteLine("What's going on, here?");
                }
            }



            
            Assert.Equal(86.53, sut);
        }






    }
}
