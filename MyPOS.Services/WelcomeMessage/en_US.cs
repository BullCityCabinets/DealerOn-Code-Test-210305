using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOS.Services.WelcomeMessage
{
    public class en_US : IGreeter
    {
        public string EscapeHelp { get; } = "help";
        public string EscapeCheckout { get; } = "checkout";
        public string[] TaxExemptWords { get; } = new string[] { "book", "chocolate", "headache", "pills" };
        public string Preposition { get; } = " at ";
        public string TermForImported { get; } = "imported";
        public string ThankYouMessage { get; } = "Thanks, come again soon!";
        public string AssistedShoppingRequestInput { get; } = "Sir or madame, please show me your standard inventory.";

        ConsoleColor _inputColor;
        ConsoleColor _outputColor;

        public en_US(ConsoleColor inputColor, ConsoleColor outputColor)
        {
            _inputColor = inputColor;
            _outputColor = outputColor;
        }

        public void Greet()
        {

            Console.ForegroundColor = _outputColor;
            Console.WriteLine
                ($"Welcome to the Text2Shop point-of-sale experience \n" +
                 "Please choose a shopping method:\n" +
                 "To use an automated system to browse stock, type:");
            Console.ForegroundColor = _inputColor;
            Console.WriteLine(AssistedShoppingRequestInput);
            Console.ForegroundColor = _outputColor;
            Console.Write
                ("or begin manually entering your items using cutting edge input syntax.\n" +
                "[Quantity] [Product Description] at [Price]\n" +
                "Example: ");
            Console.ForegroundColor = _inputColor;
            Console.WriteLine("2 Packs of cough drops at 2.98\n");


            Console.ForegroundColor = _outputColor;
            Console.Write("At any time you can ask for");
            Console.ForegroundColor = _inputColor;
            Console.WriteLine(" HELP");
            Console.ForegroundColor = _outputColor;
            Console.Write("Be sure to let us know when you are prepared to");
            Console.ForegroundColor = _inputColor;
            Console.WriteLine(" CHECKOUT");


            Console.ForegroundColor = _outputColor;
            Console.WriteLine("\nPlease specify your intent, or begin manual entry session with your first purchase.");

        }


        public string PrintAfterHoursMessage()
        {
            return "We're sorry, you've reached our shopper assistance program after hours.";
        }

        public string RespondToEscapeWords(string userInput)
        {
            string response = "";
            switch (userInput.ToLowerInvariant().Trim())
            {
                case "help":
                    Console.ForegroundColor = _outputColor;
                    Console.Write("Help: \nPlease begin manually entering your items using cutting edge input syntax.\n" +
                        "[Quantity] [Product Description] at [Price]\n" +
                         "Example: ");
                    Console.ForegroundColor = _inputColor;
                    Console.WriteLine("2 Packs of cough drops at 2.98\n");
                    response = "help";
                    break;

                case "checkout":
                    Console.ForegroundColor = _outputColor;
                    Console.WriteLine("We present you with a grand receipt:");                    
                    response = "checkout";
                    break;

            }
            return response;
        }

        public void PrintReadyforInput()
        {
            Console.ForegroundColor = _outputColor;
            Console.Write("Ready for input: ");
            Console.ForegroundColor = _inputColor;

        }



    }
}
