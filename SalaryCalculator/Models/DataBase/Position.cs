using System;
using System.Collections.Generic;

namespace SalaryCalculator.Models.DataBase
{
    public partial class Position
    {
        public Position()
        {
            LaborStandarts = new HashSet<LaborStandart>();
            Workers = new HashSet<Worker>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int PaymentFormId { get; set; }
        public decimal? BasicSalarePerWorkUnit { get; set; }
        public decimal? SalarePerWorkUnitOverTheNorm { get; set; }

        public virtual PaymentForm PaymentForm { get; set; } = null!;
        public virtual ICollection<LaborStandart> LaborStandarts { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
