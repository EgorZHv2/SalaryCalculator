using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Windows.Input;
using SalaryCalculator.Services;
using SalaryCalculator.Data;


namespace SalaryCalculator.ViewModels
{
    public class MainViewModel:BaseVm
    {
        public MainViewModel()
        {
            DbInitializer.Initialize(ApplicationDbContext.GetContext());

        }
    }
}
