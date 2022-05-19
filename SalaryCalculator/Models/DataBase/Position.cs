using System;
using System.Collections.Generic;

namespace SalaryCalculator.Models.DataBase
{
    public partial class Position
    {
        public Position()
        {

            Workers = new HashSet<Worker>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int PaymentFormId { get; set; }

        public int StandartInUnits { get; set; }
        public decimal? BasicSalarePerWorkUnit { get; set; }
        public decimal? SalarePerWorkUnitOverTheNorm { get; set; }

        public virtual PaymentForm PaymentForm { get; set; } = null!;
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
