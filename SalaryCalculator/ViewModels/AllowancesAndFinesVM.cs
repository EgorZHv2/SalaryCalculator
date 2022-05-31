using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SalaryCalculator.Views.Windows;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SalaryCalculator.Interfaces;

namespace SalaryCalculator.ViewModels
{
    public class AllowancesAndFinesVM:BaseVm, IVMWithDataGrid
    {
        public AllowancesAndFinesVM()
        {
            GenerateList();
        }

        private List<AllowancesAndFinesModel> allowancesAndFines = new List<AllowancesAndFinesModel>();
        private AllowancesAndFinesModal ModalWindow;
        private AllowancesAndFinesModalVM ModalWindowModel;

        public List<AllowancesAndFinesModel> AllowancesAndFines
        {
            get { return allowancesAndFines; }
            set
            {
                allowancesAndFines = value;
                OnPropertyChanged();
            }
        }

        private AllowancesAndFinesModel selectedAAFModel;

        public AllowancesAndFinesModel SelectedAAFModel
        {
            get { return selectedAAFModel; }
            set
            {
                selectedAAFModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenAddDiag
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ModalWindow = new AllowancesAndFinesModal();
                    ModalWindowModel = new AllowancesAndFinesModalVM(this);
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
                    AllowancesAndFinesModel AaFModel = obj as AllowancesAndFinesModel;
                    ModalWindow = new AllowancesAndFinesModal();
                    AllowancesAndFine AaF = ApplicationDbContext.GetContext().AllowancesAndFines.Find(AaFModel.Id);
                    ModalWindowModel = new AllowancesAndFinesModalVM(this, AaF);
                    ModalWindow.DataContext = ModalWindowModel;
                    ModalWindow.Activate();
                    ModalWindow.Show();
                });
            }
        }

        public ICommand DeleteAaF
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                   
                        AllowancesAndFinesModel AaFModel = obj as AllowancesAndFinesModel;
                        ApplicationDbContext.GetContext().AllowancesAndFines.Remove(ApplicationDbContext.GetContext().AllowancesAndFines.Find(AaFModel.Id));
                        ApplicationDbContext.GetContext().SaveChanges();
                        GenerateList();
                   
                });
            }
        }

        public void AddAaF(AllowancesAndFine AaF)
        {
            ApplicationDbContext.GetContext().AllowancesAndFines.Add(AaF);
            ApplicationDbContext.GetContext().SaveChanges();
            ModalWindow.Hide();
            ModalWindow.Close();
            GenerateList();
        }

        public void UpdateAaF(AllowancesAndFine AaF)
        {
            var dbpos = ApplicationDbContext.GetContext().AllowancesAndFines.FirstOrDefault(s => s.Id == AaF.Id);
            dbpos.WorkerId = AaF.WorkerId;
            dbpos.Fine = AaF.Fine;
            dbpos.Bonus = AaF.Bonus;
            ApplicationDbContext.GetContext().SaveChanges();
            ModalWindow.Hide();
            ModalWindow.Close();
            GenerateList();
        }

        public void GenerateList()
        {
            List<AllowancesAndFine> bdlist = ApplicationDbContext.GetContext().AllowancesAndFines.ToList();
            List<AllowancesAndFinesModel> newlist = new List<AllowancesAndFinesModel>();
            foreach (AllowancesAndFine AaF in bdlist)
            {
                Worker wk = ApplicationDbContext.GetContext().Workers.Find(AaF.WorkerId);
                newlist.Add(new AllowancesAndFinesModel
                {
                    Id = AaF.Id, 
                    FIO = wk.FirstName + " " + wk.LastName + " " + wk.Patronimyc,
                    Bonus = AaF.Bonus,
                    Fine = AaF.Fine
                });
            }
            AllowancesAndFines = newlist;
        }
    }
}
