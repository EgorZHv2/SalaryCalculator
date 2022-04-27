using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SalaryCalculator.ViewModels
{
    public class LSModalWindowVM:BaseVm
    {
        private LaborStandartsViewModel parentVm;
        private LaborStandart laborStandart;
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

        public string BtContent { get; set; }

        public LSModalWindowVM(LaborStandartsViewModel parentVm)
        {
            this.parentVm = parentVm;
            BtContent = "Добавить";
            Positions = ApplicationDbContext.GetContext().Positions.ToList();
        }

        public LSModalWindowVM(LaborStandartsViewModel parentVm, LaborStandart laborStandart)
        {
            this.parentVm = parentVm;
            this.laborStandart = laborStandart;
            BtContent = "Изменить";
            Positions = ApplicationDbContext.GetContext().Positions.ToList();
            _LaborStandart = laborStandart.StandartInUnits;
            Position = ApplicationDbContext.GetContext().Positions.FirstOrDefault(pos => pos.Id == laborStandart.PositionId);
           

        }

        private int _laborstandart;

        public int _LaborStandart
        {
            get { return _laborstandart; }
            set
            {
                _laborstandart = value;
                OnPropertyChanged();
            }
        }

        private Position position;

        public Position Position
        {
            get { return position; }
            set { position = value; OnPropertyChanged(); }
        }

        
        

        public ICommand Submit
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (laborStandart == null)
                    {

                        LaborStandart ls = new LaborStandart()
                        {
                            PositionId = Position.Id,
                            StandartInUnits = _LaborStandart
                        };
                        parentVm.AddLaborStandart(ls);
                    }
                    else
                    {
                        LaborStandart ls = new LaborStandart()
                        {
                            Id = laborStandart.Id,
                            PositionId = Position.Id,
                            StandartInUnits = _LaborStandart
                        };
                        parentVm.UpdateLaborStandart(ls);
                    }

                });
            }
        }
    }
}
