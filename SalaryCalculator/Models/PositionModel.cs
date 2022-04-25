using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Models
{
    public class PositionModel
    {
        public string? Name { get; set; }
        public string? PaymentForm { get; set; }
        public decimal? BasicSalarePerWorkUnit { get; set; }
        public decimal? SalarePerWorkUnitOverTheNorm { get; set; }
    }
}
