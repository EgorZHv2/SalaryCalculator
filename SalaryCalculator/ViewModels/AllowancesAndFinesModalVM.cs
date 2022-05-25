using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SalaryCalculator.ViewModels
{
    public class AllowancesAndFinesModalVM:BaseVm
    {
        private AllowancesAndFinesVM parentVm;
        private AllowancesAndFine _AaF;
        private List<Worker> workers = new List<Worker>();

        public List<Worker> Workers
        {
            get { return workers; }
            set
            {
                workers = value;
                OnPropertyChanged();
            }
        }

        public string BtContent { get; set; }

        public AllowancesAndFinesModalVM(AllowancesAndFinesVM parentVm)
        {
            this.parentVm = parentVm;
            BtContent = "Добавить";
            Workers = ApplicationDbContext.GetContext().Workers.ToList();
        }

        public AllowancesAndFinesModalVM(AllowancesAndFinesVM parentVm, AllowancesAndFine AaF)
        {
            this.parentVm = parentVm;
            this._AaF = AaF;
            BtContent = "Изменить";
            Workers = ApplicationDbContext.GetContext().Workers.ToList();
            Worker = ApplicationDbContext.GetContext().Workers.FirstOrDefault(s => s.Id == _AaF.WorkerId);
            Bonus = AaF.Bonus;
            Fine = AaF.Fine;


        }


        private Worker worker;

        public Worker Worker
        {
            get { return worker; }
            set { worker = value; OnPropertyChanged(); }
        }



        private decimal? bonus;

        public decimal? Bonus
        {
            get { return bonus; }
            set { bonus = value; OnPropertyChanged(); }
        }

        private decimal? fine;

        public decimal? Fine
        {
            get { return fine; }
            set { fine = value; OnPropertyChanged(); }
        }

        public ICommand Submit
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (_AaF == null)
                    {
                       
                            AllowancesAndFine AllAndFine = new AllowancesAndFine
                            {
                                WorkerId = Worker.Id,
                                Bonus = Bonus == null ? 0: Bonus,
                                Fine = Fine == null ? 0 : Fine
                            };
                            parentVm.AddAaF(AllAndFine);
                        
                        

                       
                    }
                    else
                    {
                        AllowancesAndFine AllAndFine = new AllowancesAndFine
                        {
                            Id = _AaF.Id,
                            WorkerId = Worker.Id,
                            Bonus = Bonus,
                            Fine = Fine
                        };

                        parentVm.UpdateAaF(AllAndFine);
                    }

                });
            }
        }
    }
}
