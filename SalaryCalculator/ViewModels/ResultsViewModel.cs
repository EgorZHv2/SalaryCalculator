using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SalaryCalculator.Interfaces;
using Microsoft.Win32;
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace SalaryCalculator.ViewModels
{
    public class ResultsViewModel : BaseVm,IVMWithDataGrid
    {
        List<ResultsModel> resultslist = new List<ResultsModel>();
        public ResultsViewModel()
        {
            GenerateList();
        }

        private List<ResultsModel> resultsModels = new List<ResultsModel>();

        public List<ResultsModel> ResultsModels
        {
            get { return resultsModels; }
            set 
            { 
                resultsModels = value;
                OnPropertyChanged();
            }
        }

        public ICommand Refresh 
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    GenerateList();
                    MessageBox.Show("Я сработало");
                });
            }
        }

        public  ICommand ExportToCsv
        {
            get
            {
                return new DelegateCommand(async obj =>
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = "Results.csv";
                    saveFileDialog.ShowDialog();
                    string path = saveFileDialog.FileName;
                    CsvConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";"
                    };
                    using(StreamWriter streamWriter = new StreamWriter(path,false,System.Text.Encoding.UTF8))
                        using(CsvWriter csvWriter = new CsvWriter(streamWriter, configuration))
                    {
                      await csvWriter.WriteRecordsAsync(resultslist);
                    }
                });
            }
        }
        public void GenerateList()
        {
            List<Worker> workers = ApplicationDbContext.GetContext().Workers.ToList();
            resultslist = new List<ResultsModel>();
            foreach (Worker worker in workers)
            {
                decimal? basesalary = ApplicationDbContext.GetContext().Positions.FirstOrDefault(s => s.Id == worker.PositionId).BasicSalarePerWorkUnit;
                decimal? oversalary = ApplicationDbContext.GetContext().Positions.FirstOrDefault(s => s.Id == worker.PositionId).SalarePerWorkUnitOverTheNorm;
                int? standartinunits = ApplicationDbContext.GetContext().Positions.FirstOrDefault(s => s.Id == worker.PositionId).StandartInUnits;
                int? workedunits = ApplicationDbContext.GetContext().WorkedUnitsOfLabors.FirstOrDefault(s => s.WorkerId == worker.Id)?.WorkedUnits;
                AllowancesAndFine? AaF = ApplicationDbContext.GetContext().AllowancesAndFines.FirstOrDefault(s => s.WorkerId == worker.Id);


                resultslist.Add(new ResultsModel
                {
                    FIO = (worker.FirstName + " " + worker.LastName + " " + worker.Patronimyc).Trim(),
                    Result = (workedunits <= standartinunits ? basesalary * workedunits : basesalary * standartinunits + oversalary * (workedunits - standartinunits)) + (AaF != null ? AaF.Bonus: 0) - (AaF != null ? AaF.Fine : 0)

                });

            }
            ResultsModels = resultslist;
            
        }
    }
}