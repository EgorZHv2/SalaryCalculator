using System;
using System.Collections.Generic;

namespace SalaryCalculator.Data
{
    public partial class LaborStandart
    {
        public int Id { get; set; }
        public int StandartInUnits { get; set; }
        public int PositionId { get; set; }

        public virtual Position Position { get; set; } = null!;
    }
}
