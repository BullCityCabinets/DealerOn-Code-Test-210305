using MyPOS.Models;
using MyPOS.Services.ShoppingMethod;
using MyPOS.Services.Tax;
using System;
using System.Globalization;
using Xunit;

namespace MyPOS.Test
{
    public class PracticeTests
    {
        [Fact]
        public void BasketIsEmptyButNotNullOnCreation()
        {
            ShoppingBasket sut = new ShoppingBasket();
            Assert.True(sut.Manifest.Count == 0);
        }

        [Fact]
        public void StandardTaxIsCalculating()
        {
            var TaxService = new DefaultTaxService(salesTaxRate: 0.1, importDutyRate: 0.05, roundTo: .05);
            var p = new Product("Music CD", 14.99);
            var tax = TaxService.ComputeStandardTax(p.ListPrice);
            Assert.Equal(1.5, tax);
        }

        [Fact]
        public void ImportDutyIsCalculating()
        {
            var TaxService = new DefaultTaxService(salesTaxRate: 0.1, importDutyRate: 0.05, roundTo: .05);
            var p = new Product("imported box of chocolates", 10.00);
            var tax = TaxService.ComputeImportDuty(p.ListPrice);
            Assert.Equal(0.5, tax);
        }


        [Fact]
        public void ImportsAreIdentified()
        {
            var TaxService = new DefaultTaxService(salesTaxRate: 0.1, importDutyRate: 0.05, roundTo: .05);        
            string[] TaxExemptWords = new string[] { "book", "chocolate", "headache", "pills" };        
            var ShoppingMethod = new ManualTextEntry(ConsoleColor.Red, ConsoleColor.Red, TaxService, TaxExemptWords, "at", "import");

            var p = ShoppingMethod.ParsePurchase("1 imported box of chocolates at 10.00");

            Assert.Equal(.5, p.TaxCharged);


        }


    }
}
