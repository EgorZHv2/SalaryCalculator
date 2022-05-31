using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SalaryCalculator.ViewModels
{
    public class WorkedUnitsModalVM:BaseVm
    {
        private WorkedUnitsViewModel parentVm;
        private WorkedUnitsOfLabor _wuol;
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

        public WorkedUnitsModalVM(WorkedUnitsViewModel parentVm)
        {
            this.parentVm = parentVm;
            BtContent = "Добавить";
            Workers = ApplicationDbContext.GetContext().Workers.ToList();
        }

        public WorkedUnitsModalVM(WorkedUnitsViewModel parentVm, WorkedUnitsOfLabor wuol)
        {
            this.parentVm = parentVm;
            this._wuol = wuol;
            BtContent = "Изменить";
            Workers = ApplicationDbContext.GetContext().Workers.ToList();
            Worker = ApplicationDbContext.GetContext().Workers.FirstOrDefault(s => s.Id == _wuol.WorkerId);
            WorkedUnits = wuol.WorkedUnits;
            

        }

        
        private Worker worker;

        public Worker Worker
        {
            get { return worker; }
            set { worker = value; OnPropertyChanged(); }
        }

       

        private int workedunits;

        public int WorkedUnits
        {
            get { return workedunits; }
            set { workedunits = value; OnPropertyChanged(); }
        }

        public ICommand Submit
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (Worker == null)
                    {
                        MessageBox.Show("Вы не выбрали сотрудника");
                    }
                    else
                    {
                        if (_wuol == null)
                        {
                            WorkedUnitsOfLabor wuol = new WorkedUnitsOfLabor()
                            {
                                WorkerId = Worker.Id,
                                WorkedUnits = WorkedUnits
                            };

                            parentVm.AddWorkedUnits(wuol);
                        }
                        else
                        {
                            WorkedUnitsOfLabor wuol = new WorkedUnitsOfLabor()
                            {
                                Id = _wuol.Id,
                                WorkerId = Worker.Id,
                                WorkedUnits = WorkedUnits
                            };

                            parentVm.UpdatePosition(wuol);
                        }
                    }

                });
            }
        }
    }
}
