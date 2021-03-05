using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOS.Services.WelcomeMessage
{
    public interface IGreeter
    {
        public string EscapeHelp { get; }
        public string EscapeCheckout { get; }
        public string Preposition { get; }
        public string TermForImported { get; }
        public string[] TaxExemptWords { get; }
        public string ThankYouMessage { get; }
        public string AssistedShoppingRequestInput { get; }


        public void Greet();

        public string RespondToEscapeWords(string userInput);

        public void PrintReadyforInput();

        public string PrintAfterHoursMessage();

    }
}
