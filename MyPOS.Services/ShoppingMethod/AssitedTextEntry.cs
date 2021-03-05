using MyPOS.Models;
using MyPOS.Services.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOS.Services.ShoppingMethod
{
    public class AssitedTextEntry : IShoppingMethod
    {
        public string[] _exemptWords { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string _preposition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string _termForImported { set => throw new NotImplementedException(); }
        public ITaxService _taxService { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Buy(Purchase purchase, ShoppingBasket basket) 
        {
            throw new NotImplementedException();
        }

        public Purchase ParsePurchase(string userInput)
        {
            throw new NotImplementedException();
        }

        public bool TestForImportedStatus(string input)
        {
            throw new NotImplementedException();
        }

        public bool TestForTaxableStatus(string input)
        {
            throw new NotImplementedException();
        }

        public bool ValidateAsProduct(string userInput)
        {
            throw new NotImplementedException();
        }
    }
}
