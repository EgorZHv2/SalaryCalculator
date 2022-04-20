using System;
using System.Collections.Generic;

namespace SalaryCalculator.Data
{
    public partial class PaymentForm
    {
        public PaymentForm()
        {
            Positions = new HashSet<Position>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Position> Positions { get; set; }
    }
}
