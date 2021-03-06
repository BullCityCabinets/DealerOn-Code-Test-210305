using MyPOS.Models;
using System;
using MyPOS.Services.Tax;

namespace MyPOS.Services.ShoppingMethod
{
    public class ManualTextEntry: IShoppingMethod
    {
        ConsoleColor _inputColor;
        ConsoleColor _outputColor;
        public string[] _exemptWords { get; set; }
        public string _preposition { get; set; }
        public string _termForImported { get; set; }
        
        public ITaxService _taxService { get; set; }


        public ManualTextEntry(ConsoleColor inputColor, ConsoleColor outputColor, ITaxService taxService, string[] exemptWords, string preposition, string termForImported)
        {            
            _inputColor = inputColor;
            _outputColor = outputColor;

            _taxService = taxService;
            _exemptWords = (string[])exemptWords.Clone();
            _preposition = preposition;
            _termForImported = termForImported;
        }

        public bool ValidateAsProduct(string userInput)
        {
            if (userInput.Contains(_preposition))
            {
                try // Is there enough info to be valid input?
                {
                    string[] breakout = userInput.Split(' ');
                    if (breakout.Length > 3)
                    {
                        try  //Is the last part a price?
                        {
                            if (GetPrice(breakout[breakout.Length - 1]) <= 0)
                            {
                                PrintSyntaxError("Valid input requires a price specified (nothing at this store is free).");
                                return false;
                            }
                            try  //Is the first part a quantity?
                            {
                                if (GetCount(breakout[0]) <= 0)
                                {
                                    PrintSyntaxError("All purchase require specified quanitities");
                                    return false;
                                }

                            }
                            catch
                            {
                                PrintSyntaxError("All purchase require specified quanitities");
                                return false;
                                throw;
                            }
                        }
                        catch
                        {
                            PrintSyntaxError("Valid input requires a price specified (nothing at this store is free).");
                            return false;
                            throw;
                        }
                    }
                    else //if (breakout.Length > 3)
                    {
                        PrintSyntaxError("Insufficient input to create purchase.");
                    }
                }
                catch
                {
                    PrintSyntaxError("Insufficient input to create purchase.");
                }
                return true;
            }
            else
            {
                PrintSyntaxError("Did you forget the preposition?");
                return false;
            }
        }





        public Purchase ParsePurchase(string userInput)
        {
            string[] breakout = userInput.Split(' ');

            var quanitity = (int)GetCount(breakout[0]);

            string name = "";
            for (int i = 1; i < breakout.Length - 2; i++)
            {
                name += breakout[i].Trim() + " ";
            }

            double price = GetPrice(breakout[breakout.Length - 1]);
            
            bool isImported = TestForImportedStatus(breakout[1].ToLowerInvariant());
            double importDuty = 0;
            if (isImported)
            {
                importDuty += _taxService.ComputeImportDuty(price);
            }

            bool isTaxable = TestForTaxableStatus(name);
            double salesTax = 0;
            if (isTaxable)
            {
                salesTax += _taxService.ComputeStandardTax(price);
            }
            Console.ForegroundColor = _outputColor;
            Console.WriteLine($"{userInput} added to your shopping basket.");

            double combinedTaxes = importDuty + salesTax;
            return new Purchase(new Product(name.Trim(), price), quanitity, combinedTaxes);

        }





        Double GetPrice(string input) =>
            Double.TryParse(input, out var price) == true
            ? Math.Round(price, 2) : 0;

        Double GetCount(string input) =>
            Double.TryParse(input, out var count) == true
            ? Math.Round(count, 0) : 0;

        public bool TestForTaxableStatus(string input)
        {
            int isExcempt = 0;
            foreach (var word in _exemptWords)
            {
                if (input.ToLowerInvariant().Contains(word))
                {
                    isExcempt++;
                }
            }
            return isExcempt > 0 ? false : true;
        }

        public bool TestForImportedStatus(string input)
        {
         
            return input.ToLowerInvariant() == "imported"
                 ? true
                : false;


        }


        void PrintSyntaxError(string circumstance)
        {
            Console.ForegroundColor = _outputColor;
            Console.Write($"Syntax Error.  {circumstance}  Please try again, or request ");
            Console.ForegroundColor = _inputColor;
            Console.WriteLine($"HELP");

        }

    }
}
