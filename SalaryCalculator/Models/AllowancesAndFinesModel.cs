using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Models
{
    public class AllowancesAndFinesModel
    {
        public int Id { get; set; }
        public string? FIO { get; set; }

        public decimal? Bonus { get; set; }

        public decimal? Fine { get; set; }
    }
}
