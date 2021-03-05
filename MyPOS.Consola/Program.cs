using System;
using System.Globalization;
using System.Linq;

using MyPOS.Services.WelcomeMessage;
using MyPOS.Services.ShoppingMethod;
using MyPOS.Services.Tax;
using MyPOS.Models;

namespace MyPOS.Consola
{
    class Program
    {
        static ITaxService TaxService;
        static IGreeter Greeter;
        static IShoppingMethod ShoppingMethod;
        static ShoppingBasket MyShoppingBasket;

        static ConsoleColor InputColor;
        static ConsoleColor OutputColor;

        static void Main(string[] args)
        {
            SetCulture();

            bool isShopping = true;
            bool isInitialInput = true;
            MyShoppingBasket = new ShoppingBasket();

            var userInput = "";

            while (isShopping)
            {
                Greeter.PrintReadyforInput();
                Console.ForegroundColor = InputColor;
                userInput = Console.ReadLine();

                if (userInput.ToLowerInvariant() != Greeter.EscapeHelp && userInput.ToLowerInvariant() != Greeter.EscapeCheckout) 
                {
                    if (!String.IsNullOrWhiteSpace(userInput) && userInput != Greeter.AssistedShoppingRequestInput)
                    {
                        if (isInitialInput)
                        {
#if DEBUG
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nINITIALIZING MANUAL TEXT ENTRY...");
                            Console.ForegroundColor = OutputColor;
#endif
                            ShoppingMethod ??= new ManualTextEntry(InputColor, OutputColor,TaxService, Greeter.TaxExemptWords, Greeter.Preposition, Greeter.TermForImported);
                            isInitialInput = false;
                        }

                        if (ShoppingMethod.ValidateAsProduct(userInput.ToLowerInvariant()))
                        {
#if DEBUG
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nINPUT VALIDATED.");
                            Console.ForegroundColor = OutputColor;
#endif
                            MyShoppingBasket ??= new ShoppingBasket();
                            MyShoppingBasket.Buy(ShoppingMethod.ParsePurchase(userInput));
                        }
                    }

                    else // User requested assisted shopping
                    {
                        //ShoppingMethod = new AssitedTextEntry(); // TODO!
                        
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Greeter.PrintAfterHoursMessage()); 
                    }
                }

                else
                {                    
                    isShopping = false; // user has requested HELP or CHEKCOUT...
                }
            }
            Console.WriteLine("\nShopping ended...\n");
            var escapedTo = Greeter.RespondToEscapeWords(userInput);
            if(escapedTo == "checkout")
            {
                MyShoppingBasket.PrintReceipt();
                
            }
            Console.ForegroundColor = InputColor;
            Console.WriteLine(Greeter.ThankYouMessage);
            Console.ForegroundColor = ConsoleColor.White;

        }





        static void SetCulture()
        {
            var culture = CultureInfo.CurrentCulture; //Console.WriteLine(d.ToString(culture));
            string language = new string(culture.ToString().ToLowerInvariant().Take(2).ToArray());

            switch (language)
            {
                case "en":
                    TaxService = new DefaultTaxService(salesTaxRate: 0.1, importDutyRate: 0.05, roundTo: .05);
                    InputColor = ConsoleColor.Yellow;
                    OutputColor = ConsoleColor.Green;
                    Greeter = new en_US(InputColor, OutputColor);
                    Greeter.Greet();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Our apologies for only offering this service in English.  We will strive to be more inclusive in future releases.\n");

                    TaxService = new DefaultTaxService(salesTaxRate: 0.1, importDutyRate: 0.05, roundTo: .05);
                    InputColor = ConsoleColor.Yellow;
                    OutputColor = ConsoleColor.Green;
                    Greeter = new en_US(InputColor, OutputColor);
                    Greeter.Greet();
                    break;
            };
        }
    }
}
