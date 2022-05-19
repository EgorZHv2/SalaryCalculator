using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SalaryCalculator.ViewModels
{
    public class PositionModalViewModel : BaseVm
    {
        private PositionsViewModel parentVm;
        private Position position;
        private List<PaymentForm> paymentForms = new List<PaymentForm>();

        public List<PaymentForm> PaymentForms
        {
            get { return paymentForms; }
            set
            {
                paymentForms = value;
                OnPropertyChanged();
            }
        }

        public string BtContent { get; set; }

        public PositionModalViewModel(PositionsViewModel parentVm)
        {
            this.parentVm = parentVm;
            BtContent = "Добавить";
            PaymentForms = ApplicationDbContext.GetContext().PaymentForms.ToList();
        }

        public PositionModalViewModel(PositionsViewModel parentVm, Position position)
        {
            this.parentVm = parentVm;
            this.position = position;
            BtContent = "Изменить";
            PaymentForms = ApplicationDbContext.GetContext().PaymentForms.ToList();
            Name = position.Name;
            PayementForm = ApplicationDbContext.GetContext().PaymentForms.Find(position.PaymentFormId);
            StandartInUnits = position.StandartInUnits;
            Salary = position.BasicSalarePerWorkUnit;
            OverSalary = position.SalarePerWorkUnitOverTheNorm;
           
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        private int standartInUnits;

        public int StandartInUnits
        {
            get { return standartInUnits; }
            set
            {
                standartInUnits = value;
                OnPropertyChanged();
            }
        }

        private PaymentForm payementform;

        public PaymentForm PayementForm
        {
            get { return payementform; }
            set { payementform = value; OnPropertyChanged(); }
        }

        private decimal? salary;

        public decimal? Salary
        {
            get { return salary; }
            set { salary = value; OnPropertyChanged(); }
        }

        private decimal? oversalary;

        public decimal? OverSalary
        {
            get { return oversalary; }
            set { oversalary = value; OnPropertyChanged(); }
        }

        public ICommand Submit
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (position == null)
                    {
                        Position pos = new Position()
                        {
                            Name = Name,
                            PaymentFormId = PayementForm.Id,
                            StandartInUnits = StandartInUnits,
                            BasicSalarePerWorkUnit = Salary,
                            SalarePerWorkUnitOverTheNorm = OverSalary
                        };

                        parentVm.AddPosition(pos);
                    }
                    else
                    {
                        Position pos = new Position()
                        {
                            Id = position.Id,
                            Name = Name,
                            PaymentFormId = PayementForm.Id,
                            StandartInUnits = StandartInUnits,
                            BasicSalarePerWorkUnit = Salary,
                            SalarePerWorkUnitOverTheNorm = OverSalary
                        };
                        parentVm.UpdatePosition(pos);
                    }

                });
            }
        }
    }
}