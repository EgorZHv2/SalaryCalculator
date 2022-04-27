using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Windows.Input;
using System.Windows.Controls;
using SalaryCalculator.Views.Pages;
using SalaryCalculator.Services;
using SalaryCalculator.Models.DataBase;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace SalaryCalculator.ViewModels
{
    public class MainViewModel : BaseVm
    {


        private Page _pageinframe;

        public Page PageInFrame
        {
            get { return _pageinframe; }
            set 
            { 
                _pageinframe = value;
                OnPropertyChanged();
            }
        }
       
        public MainViewModel()
        {
            DbInitializer.Initialize(ApplicationDbContext.GetContext());                
        }
        public ICommand GoToPositionsPage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                   PositionsPage pg = new PositionsPage();
                   PageInFrame = Navigation.Navigate(pg);
                });
            }
        }
        public ICommand GoToPaymentFormsPage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    PaymentFormsPage pg = new PaymentFormsPage();
                    PageInFrame = Navigation.Navigate(pg);
                });
            }
        }
        public ICommand GoToLaborStandartsPage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    LaborStandartsPage pg = new LaborStandartsPage();
                    PageInFrame = Navigation.Navigate(pg);
                });
            }
        }
        public ICommand GoToWorkersPage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    WorkersPage pg = new WorkersPage();
                    PageInFrame = Navigation.Navigate(pg);
                });
            }
        }

        public ICommand GoToWorkedUnitsPage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    WorkedUnitsPage pg = new WorkedUnitsPage();
                    PageInFrame = Navigation.Navigate(pg);
                });
            }
        }
        public ICommand GoToAllowancesAndFinesPage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    AllowancesAndFinesPage pg = new AllowancesAndFinesPage();
                    PageInFrame = Navigation.Navigate(pg);
                });
            }
        }
        public ICommand GoToResults
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ResultsPage pg = new ResultsPage();
                    PageInFrame = Navigation.Navigate(pg);
                });
            }
        }
        public ICommand Exit
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Environment.Exit(1);



                });
            }
        }
    }
}
