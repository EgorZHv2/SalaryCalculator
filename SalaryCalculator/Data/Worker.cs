using System;
using System.Collections.Generic;

namespace SalaryCalculator.Data
{
    public partial class Worker
    {
        public Worker()
        {
            AllowancesAndFines = new HashSet<AllowancesAndFine>();
            WorkedUnitsOfLabors = new HashSet<WorkedUnitsOfLabor>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronimyc { get; set; }
        public int PositionId { get; set; }
        public DateTime HiringDate { get; set; }

        public virtual Position Position { get; set; } = null!;
        public virtual ICollection<AllowancesAndFine> AllowancesAndFines { get; set; }
        public virtual ICollection<WorkedUnitsOfLabor> WorkedUnitsOfLabors { get; set; }
    }
}
