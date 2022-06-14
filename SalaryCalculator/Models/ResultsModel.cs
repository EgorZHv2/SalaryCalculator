using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Models
{
    public class ResultsModel
    {
        public int Id { get; set; }
        public string? FIO { get; set; }
        public decimal? ResultBefore { get; set; }
        public decimal? ResultAfther { get; set; }
    }
}
