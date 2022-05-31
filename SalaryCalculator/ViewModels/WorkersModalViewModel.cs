using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System;
using System.Windows;

namespace SalaryCalculator.ViewModels
{
    public class WorkersModalViewModel:BaseVm
    {
        private WorkersViewModel parentVm;
        private Worker _worker;
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

        public WorkersModalViewModel(WorkersViewModel parentVm)
        {
            this.parentVm = parentVm;
            BtContent = "Добавить";
            Positions = ApplicationDbContext.GetContext().Positions.ToList();
        }

        public WorkersModalViewModel(WorkersViewModel parentVm, Worker worker)
        {
            this.parentVm = parentVm;
            this._worker = worker;
            BtContent = "Изменить";
            Positions = ApplicationDbContext.GetContext().Positions.ToList();
            FirstName = worker.FirstName;
            LastName = worker.LastName;
            Patronymic = worker.Patronimyc;
            Position = ApplicationDbContext.GetContext().Positions.FirstOrDefault(pos => pos.Id == worker.PositionId);
           

        }

        private string firstname;

        public string FirstName
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged();
            }
        }

        private string lastname;

        public string LastName
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged();
            }
        }

        private string patronymic;

        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                patronymic = value;
                OnPropertyChanged();
            }
        }
       

        private Position position;

        public Position Position
        {
            get { return position; }
            set
            { 
                position = value; 
                OnPropertyChanged();
            }
        }

        
        public ICommand Submit
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (Position == null)
                    {
                        MessageBox.Show("Вы не выбрали должность");
                    }
                    else
                    {
                        if (_worker == null)
                        {
                            Worker worker = new Worker()
                            {
                                FirstName = FirstName,
                                LastName = LastName,
                                Patronimyc = Patronymic,
                                PositionId = Position.Id,

                            };

                            parentVm.AddWorker(worker);
                        }
                        else
                        {
                            Worker worker = new Worker()
                            {
                                Id = _worker.Id,
                                FirstName = FirstName,
                                LastName = LastName,
                                Patronimyc = Patronymic,
                                PositionId = Position.Id,

                            };


                            parentVm.UpdateWorker(worker);
                        }
                    }

                });
            }
        }
    }
}
