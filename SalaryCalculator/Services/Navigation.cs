using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SalaryCalculator.Views.Pages;
using SalaryCalculator.ViewModels;
using SalaryCalculator.Interfaces;

namespace SalaryCalculator.Services
{
    public class Navigation
    {
        public static List<Page> Pages = new List<Page>();
        
        public static Page Navigate(Page pg)
        {
            IVMWithDataGrid pageVM;
            foreach (Page p in Pages)
            {
                if(p.GetType() == pg.GetType())
                {
                    pageVM = p.DataContext as IVMWithDataGrid;
                    pageVM.GenerateList();
                    return p;
                }
            }
            Pages.Add(pg);
            pageVM = pg.DataContext as IVMWithDataGrid;
            pageVM.GenerateList();
            return pg;

        }
      
    }
    
}
