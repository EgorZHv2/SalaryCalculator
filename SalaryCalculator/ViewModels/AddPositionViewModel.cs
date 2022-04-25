using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;

namespace SalaryCalculator.ViewModels
{
    public class AddPositionViewModel:BaseVm
    {
        private PositionsViewModel _d;
        public AddPositionViewModel(PositionsViewModel d)
        {
            _d = d;
        }

        private string name;
        public string _Name
        {
            get { return name; }
            set { name = value; }
        }
        private int payementformid;
        public int _PayementFormId
        {
            get { return payementformid; }
            set { payementformid = value; }
        }
        private decimal salary;
        public decimal _Salary
        {
            get { return salary; }
            set { salary = value; }
        }
        private decimal oversalary;
        public decimal _OverSalary
        {
            get { return oversalary; }
            set { oversalary = value; }
        }

        public ICommand AddPosition
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Position pos = new Position()
                    {
                        Name = _Name,
                        PaymentFormId = _PayementFormId,
                        BasicSalarePerWorkUnit = _Salary,
                        SalarePerWorkUnitOverTheNorm = _OverSalary
                    };
                    _d.AddPosition(pos);

                });
            }
        }

    }
}
