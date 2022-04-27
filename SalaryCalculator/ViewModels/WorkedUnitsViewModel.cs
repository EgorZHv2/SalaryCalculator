using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SalaryCalculator.Views.Windows;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace SalaryCalculator.ViewModels
{
    public class WorkedUnitsViewModel : BaseVm
    {
        public WorkedUnitsViewModel()
        {
            GenerateList(ApplicationDbContext.GetContext().WorkedUnitsOfLabors.ToList());
        }

        private List<WorkedUnitsModel> workedUnits = new List<WorkedUnitsModel>();
        private WorkedUnitsModalWindow ModalWindow;
        private WorkedUnitsModalVM ModalWindowModel;

        public List<WorkedUnitsModel> WorkedUnits
        {
            get { return workedUnits; }
            set
            {
                workedUnits = value;
                OnPropertyChanged();
            }
        }

        private WorkedUnitsModel selectedworkedunit;

        public WorkedUnitsModel SelectedWorkedUnit
        {
            get { return selectedworkedunit; }
            set
            {
               selectedworkedunit = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenAddDiag
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ModalWindow = new WorkedUnitsModalWindow();
                    ModalWindowModel = new WorkedUnitsModalVM(this);
                    ModalWindow.DataContext = ModalWindowModel;
                    ModalWindow.Activate();
                    ModalWindow.Show();
                });
            }
        }

        public ICommand OpenUpdateDiag
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    WorkedUnitsModel pm = obj as WorkedUnitsModel;
                    ModalWindow = new WorkedUnitsModalWindow();
                    WorkedUnitsOfLabor wuol = ApplicationDbContext.GetContext().WorkedUnitsOfLabors.Find(pm.Id);
                    ModalWindowModel = new WorkedUnitsModalVM(this, wuol);
                    ModalWindow.DataContext = ModalWindowModel;
                    ModalWindow.Activate();
                    ModalWindow.Show();
                });
            }
        }

        public ICommand DeleteWorkedUnits
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        WorkedUnitsModel wum = obj as WorkedUnitsModel;
                        ApplicationDbContext.GetContext().WorkedUnitsOfLabors.Remove(ApplicationDbContext.GetContext().WorkedUnitsOfLabors.Find(wum.Id));
                        ApplicationDbContext.GetContext().SaveChanges();
                        GenerateList(ApplicationDbContext.GetContext().WorkedUnitsOfLabors.ToList());
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Вы пытаетесь удалить используемую должность");
                    }
                });
            }
        }

        public void AddWorkedUnits(WorkedUnitsOfLabor wuos)
        {
            ApplicationDbContext.GetContext().WorkedUnitsOfLabors.Add(wuos);
            ApplicationDbContext.GetContext().SaveChanges();
            ModalWindow.Hide();
            ModalWindow.Close();
            GenerateList(ApplicationDbContext.GetContext().WorkedUnitsOfLabors.ToList());
        }

        public void UpdatePosition(WorkedUnitsOfLabor wuos)
        {
            var dbpos = ApplicationDbContext.GetContext().WorkedUnitsOfLabors.FirstOrDefault(s => s.Id == wuos.Id);
            dbpos.WorkerId = wuos.WorkerId;
            dbpos.WorkedUnits = wuos.WorkedUnits;
            ApplicationDbContext.GetContext().SaveChanges();
            ModalWindow.Hide();
            ModalWindow.Close();
            GenerateList(ApplicationDbContext.GetContext().WorkedUnitsOfLabors.ToList());
        }

        public void GenerateList(List<WorkedUnitsOfLabor> list)
        {
            List<WorkedUnitsModel> newlist = new List<WorkedUnitsModel>();
            foreach (WorkedUnitsOfLabor wuol in list)
            {
                Worker wk = ApplicationDbContext.GetContext().Workers.Find(wuol.WorkerId);
                newlist.Add(new WorkedUnitsModel
                {
                    Id = wuol.Id,
                    FIO = wk.FirstName + " " + wk.LastName + " " + wk.Patronimyc,
                    UnitsOfLabor = wuol.WorkedUnits
                });
            }
            WorkedUnits = newlist;
        }
    }
}