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

namespace SalaryCalculator.Services
{
    public class Navigation
    {
        public static List<Page> Pages = new List<Page>();

        public static Page Navigate(Page pg)
        {
            foreach(Page p in Pages)
            {
                if(p.GetType() == pg.GetType())
                {

                    return p;
                }
            }
            Pages.Add(pg);
            return pg;

        }
      
    }
    
}
