using MyPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPOS.Services.Tax;


namespace MyPOS.Services.ShoppingMethod
{
    public interface IShoppingMethod
    {
        public string[] _exemptWords { get; set; }
        public string _preposition { get; set; }
        public string _termForImported { set; }

        public ITaxService _taxService { get; set; }

        public bool ValidateAsProduct(string userInput);
        public Purchase ParsePurchase(string userInput);

        public bool TestForTaxableStatus(string input);
        public bool TestForImportedStatus(string input);
    }
}
