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
    public class WorkersViewModel:BaseVm,IVMWithDataGrid
    {
        public WorkersViewModel()
        {

            GenerateList();

        }
        private List<WorkersModel> workers = new List<WorkersModel>();
        private WorkersModalWindow ModalWindow;
        private WorkersModalViewModel ModalWindowModel;
        public List<WorkersModel> Workers
        {
            get { return workers; }
            set
            {
                workers = value;
                OnPropertyChanged();
            }
        }
        private WorkersModel selectedworker;
        public WorkersModel SelectedWorker
        {
            get { return selectedworker; }
            set
            {
                selectedworker = value;
                OnPropertyChanged();
            }
        }


        public ICommand OpenAddDiag
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ModalWindow = new WorkersModalWindow();
                    ModalWindowModel = new WorkersModalViewModel(this);
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
                    WorkersModel wm = obj as WorkersModel;
                    ModalWindow = new WorkersModalWindow();
                    Worker worker = ApplicationDbContext.GetContext().Workers.Find(wm.Id);
                    ModalWindowModel = new WorkersModalViewModel(this, worker);
                    ModalWindow.DataContext = ModalWindowModel;
                    ModalWindow.Activate();
                    ModalWindow.Show();

                });
            }
        }
        public ICommand DeleteWorker
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        WorkersModel wm = obj as WorkersModel;
                        ApplicationDbContext.GetContext().Workers.Remove(ApplicationDbContext.GetContext().Workers.Find(wm.Id));
                        ApplicationDbContext.GetContext().SaveChanges();
                        GenerateList();
                    }
                    catch 
                    {
                        MessageBox.Show("Вы пытаетесь удалить используемого сотрудника");
                    }
                });
            }
        }
        public void AddWorker(Worker worker)
        {
            ApplicationDbContext.GetContext().Workers.Add(worker);
            ApplicationDbContext.GetContext().SaveChanges();
            ModalWindow.Hide();
            ModalWindow.Close();
            GenerateList();

        }
        public void UpdateWorker(Worker worker)
        {
            var dbpos = ApplicationDbContext.GetContext().Workers.FirstOrDefault(s => s.Id == worker.Id);
            dbpos.FirstName = worker.FirstName;
            dbpos.LastName = worker.LastName;
            dbpos.Patronimyc = worker.Patronimyc;
            dbpos.PositionId = worker.PositionId;
            
            ApplicationDbContext.GetContext().SaveChanges();
            ModalWindow.Hide();
            ModalWindow.Close();
            GenerateList();
        }
        public void GenerateList()
        {
            List<Worker> bdlist = ApplicationDbContext.GetContext().Workers.ToList();
            List<WorkersModel> newlist = new List<WorkersModel>();
            foreach (Worker worker in bdlist)
            {
                newlist.Add(new WorkersModel
                {
                    Id = worker.Id,
                    FirstName = worker.FirstName,
                    LastName = worker.LastName,
                    Patronymic = worker.Patronimyc,
                    Position = ApplicationDbContext.GetContext().Positions.Find(worker.PositionId).Name,
                    
                });
            }
            Workers = newlist;

        }
    }
}
