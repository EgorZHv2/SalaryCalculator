using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;
using SalaryCalculator.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SalaryCalculator.Views.Windows;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace SalaryCalculator.ViewModels
{
    public class PositionsViewModel : BaseVm, IVMWithDataGrid
    {
        public PositionsViewModel()
        {

            GenerateList();

        }
        private List<PositionModel> positions = new List<PositionModel>();
        private PositionModalWindow ModalWindow;
        private PositionModalViewModel ModalWindowModel;
        public List<PositionModel> Positions
        {
            get { return positions; }
            set
            {
                positions = value;
                OnPropertyChanged();
            }
        }
        private PositionModel selectedposition;
        public PositionModel SelectedPosition
        {
            get { return selectedposition; }
            set
            {
                selectedposition = value;
                OnPropertyChanged();
            }
        }

     
        public ICommand OpenAddDiag
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ModalWindow = new PositionModalWindow();
                    ModalWindowModel = new PositionModalViewModel(this);
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
                    PositionModel pm = obj as PositionModel;
                    ModalWindow = new PositionModalWindow();
                    Position position = ApplicationDbContext.GetContext().Positions.Find(pm.Id);
                    ModalWindowModel = new PositionModalViewModel(this,position);
                    ModalWindow.DataContext = ModalWindowModel;
                    ModalWindow.Activate();
                    ModalWindow.Show();

                });
            }
        }
        public ICommand DeletePosition
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        PositionModel pm = obj as PositionModel;
                        ApplicationDbContext.GetContext().Positions.Remove(ApplicationDbContext.GetContext().Positions.Find(pm.Id));
                        ApplicationDbContext.GetContext().SaveChanges();
                        GenerateList();
                    }
                    catch(DbUpdateException)
                    {
                        MessageBox.Show("Вы пытаетесь удалить используемую должность");
                    }
                });
            }
        }
        public void AddPosition(Position pos)
        {
            ApplicationDbContext.GetContext().Positions.Add(pos);
            ApplicationDbContext.GetContext().SaveChanges();
            ModalWindow.Hide();
            ModalWindow.Close();
            GenerateList();

        }
        public void UpdatePosition(Position pos)
        {
            var dbpos = ApplicationDbContext.GetContext().Positions.FirstOrDefault(s => s.Id == pos.Id);
            dbpos.Name = pos.Name;
            dbpos.PaymentFormId = pos.PaymentFormId;
            dbpos.StandartInUnits = pos.StandartInUnits;
            dbpos.BasicSalarePerWorkUnit = pos.BasicSalarePerWorkUnit;
            dbpos.SalarePerWorkUnitOverTheNorm = pos.SalarePerWorkUnitOverTheNorm;
            ApplicationDbContext.GetContext().SaveChanges();
            ModalWindow.Hide();
            ModalWindow.Close();
            GenerateList();
        }
        public void GenerateList()
        {
            List<Position> bdlist = ApplicationDbContext.GetContext().Positions.ToList();
            List<PositionModel> newlist = new List<PositionModel>();
            foreach(Position pos in bdlist)
            {
                newlist.Add(new PositionModel
                {
                    Id = pos.Id,
                    Name = pos.Name,
                    PaymentForm = ApplicationDbContext.GetContext().PaymentForms.Find(pos.PaymentFormId).Name,
                    StandartInUnits = pos.StandartInUnits,
                    BasicSalarePerWorkUnit = pos.BasicSalarePerWorkUnit,
                    SalarePerWorkUnitOverTheNorm = pos.SalarePerWorkUnitOverTheNorm
                });
            }
            Positions = newlist;
            
        }

    }
}