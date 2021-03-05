using MyPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOS.Services.Tax
{
    public interface ITaxService
    {
        public double SalesTaxRate { get; set; }
        public double ImportDutyRate { get; set; }
        public double RoundTo { get; set; }

        public double ComputeStandardTax(double listprice);
        public double ComputeImportDuty(double listprice);
    }
}
