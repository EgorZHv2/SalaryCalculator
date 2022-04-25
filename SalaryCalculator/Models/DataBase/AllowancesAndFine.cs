
using System;
using System.Collections.Generic;

namespace SalaryCalculator.Models.DataBase
{
    public partial class AllowancesAndFine

    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public decimal Bonus { get; set; }
        public decimal Fine { get; set; }

        public virtual Worker Worker { get; set; } = null!;
    }
}
