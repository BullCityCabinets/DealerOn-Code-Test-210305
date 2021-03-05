using MyPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOS.Services.Tax
{
    public class DefaultTaxService : ITaxService
    {

        public double SalesTaxRate { get; set; }
        public double ImportDutyRate { get; set; }
        public double RoundTo { get; set; }

        public DefaultTaxService(double salesTaxRate, double importDutyRate, double roundTo = .01)
        {
            SalesTaxRate = salesTaxRate;
            ImportDutyRate = importDutyRate;
            RoundTo = roundTo; 
        }


        public double ComputeStandardTax(double listprice) => ComputeWithRounding(listprice, SalesTaxRate);

        public double ComputeImportDuty(double listprice) => ComputeWithRounding(listprice, ImportDutyRate);


        double ComputeWithRounding(double listprice, double rate)
        {
            double actual = Math.Round(listprice * rate, 2);

            //if (actual % RoundTo > 0)
            //{
            double roundBy = 1 / RoundTo;
                double rounded = Math.Ceiling(actual * roundBy) / roundBy;
                return rounded;
            //}

            //return actual;
        }





    }
}
