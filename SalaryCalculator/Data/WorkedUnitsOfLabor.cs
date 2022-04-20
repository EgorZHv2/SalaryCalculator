using System;
using System.Collections.Generic;

namespace SalaryCalculator.Data
{
    public partial class WorkedUnitsOfLabor
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int WorkedUnits { get; set; }

        public virtual Worker Worker { get; set; } = null!;
    }
}
