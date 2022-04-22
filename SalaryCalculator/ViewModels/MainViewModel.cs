using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Windows.Input;
using System.Windows.Controls;
using SalaryCalculator.Views.Pages;
using SalaryCalculator.Services;
using SalaryCalculator.Data;


namespace SalaryCalculator.ViewModels
{
    public class MainViewModel:BaseVm
    {
        private Page _page;

        public Page Page
        {
            get { return _page; }
            set { _page = value; }
        }

        public MainViewModel()
        {
            DbInitializer.Initialize(ApplicationDbContext.GetContext());
            PositionsPage pg = new PositionsPage();
            Page = pg;
        }
    }
}
