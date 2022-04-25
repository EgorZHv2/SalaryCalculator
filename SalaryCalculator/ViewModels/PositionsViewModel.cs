using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SalaryCalculator.Views.Windows;
using System.Windows;

namespace SalaryCalculator.ViewModels
{
    public class PositionsViewModel : BaseVm
    {
        private List<Position> positions = new List<Position>();

        public List<Position> Positions
        {
            get { return positions; }
            set
            {
                positions = value;
                OnPropertyChanged();
            }
        }
        private Position selpos;
        public Position SelPos
        {
            get { return selpos; }
            set
            {
                selpos = value;
                OnPropertyChanged();
            }
        }

        private PositionsViewModel()
        {
           
            Positions = ApplicationDbContext.GetContext().Positions.ToList();
           
        }
        public ICommand OpenAddDiag
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    AddPosition add = new AddPosition();
                    AddPositionViewModel addmodel = new AddPositionViewModel(this);
                    add.DataContext = addmodel;
                    add.Activate();
                    add.Show();
                    
                });
            }
        }
        public ICommand DeletePosition
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Position ps = obj as Position;
                    ApplicationDbContext.GetContext().Remove(ps);
                    ApplicationDbContext.GetContext().SaveChanges();
                    Positions = ApplicationDbContext.GetContext().Positions.ToList();
                });
            }
        }
        public void AddPosition(Position pos)
        {
            ApplicationDbContext.GetContext().Positions.Add(pos);
            ApplicationDbContext.GetContext().SaveChanges();
            Positions = ApplicationDbContext.GetContext().Positions.ToList();

        }

    }
}