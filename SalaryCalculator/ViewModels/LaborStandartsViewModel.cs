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
    public class LaborStandartsViewModel:BaseVm
    {
        public LaborStandartsViewModel()
        {

            GenerateList(ApplicationDbContext.GetContext().LaborStandarts.ToList());

        }
        private List<LaborStandartModel> laborstandarts = new List<LaborStandartModel>();
        private LaborStandartModalWindow ModalWindow;
        private LSModalWindowVM ModalWindowModel;
        public List<LaborStandartModel> LaborStandarts
        {
            get { return laborstandarts; }
            set
            {
                laborstandarts = value;
                OnPropertyChanged();
            }
        }
        private LaborStandartModel selectedlaborstandart;
        public LaborStandartModel SelectedLaborStandart
        {
            get { return selectedlaborstandart; }
            set
            {
                selectedlaborstandart = value;
                OnPropertyChanged();
            }
        }


        public ICommand OpenAddDiag
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ModalWindow = new LaborStandartModalWindow();
                    ModalWindowModel = new LSModalWindowVM(this);
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
                    LaborStandartModel lsm = obj as LaborStandartModel;
                    ModalWindow = new LaborStandartModalWindow();
                    LaborStandart ls = ApplicationDbContext.GetContext().LaborStandarts.Find(lsm.Id);
                    ModalWindowModel = new LSModalWindowVM(this, ls);
                    ModalWindow.DataContext = ModalWindowModel;
                    ModalWindow.Activate();
                    ModalWindow.Show();

                });
            }
        }
        public ICommand DeleteLaborStandart
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        LaborStandartModel ls = obj as LaborStandartModel;
                        ApplicationDbContext.GetContext().LaborStandarts.Remove(ApplicationDbContext.GetContext().LaborStandarts.Find(ls.Id));
                        ApplicationDbContext.GetContext().SaveChanges();
                        GenerateList(ApplicationDbContext.GetContext().LaborStandarts.ToList());
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Вы пытаетесь удалить используемую должность");
                    }
                });
            }
        }
        public void AddLaborStandart(LaborStandart ls)
        {
            ApplicationDbContext.GetContext().LaborStandarts.Add(ls);
            ApplicationDbContext.GetContext().SaveChanges();
            ModalWindow.Hide();
            ModalWindow.Close();
            GenerateList(ApplicationDbContext.GetContext().LaborStandarts.ToList());

        }
        public void UpdateLaborStandart(LaborStandart ls)
        {
            var dbpos = ApplicationDbContext.GetContext().LaborStandarts.FirstOrDefault(s => s.Id == ls.Id);
            dbpos.PositionId = ls.PositionId;
            dbpos.StandartInUnits = ls.StandartInUnits;
           
            ApplicationDbContext.GetContext().SaveChanges();
            ModalWindow.Hide();
            ModalWindow.Close();
            GenerateList(ApplicationDbContext.GetContext().LaborStandarts.ToList());
        }
        public void GenerateList(List<LaborStandart> list)
        {
            List<LaborStandartModel> newlist = new List<LaborStandartModel>();
            foreach (LaborStandart ls in list)
            {
                newlist.Add(new LaborStandartModel
                {
                    Id = ls.Id,
                    PositionName = ApplicationDbContext.GetContext().Positions.FirstOrDefault(pos => pos.Id == ls.PositionId).Name,
                    LaborStandart = ls.StandartInUnits
                });
            }
            LaborStandarts = newlist;

        }
    }
}
