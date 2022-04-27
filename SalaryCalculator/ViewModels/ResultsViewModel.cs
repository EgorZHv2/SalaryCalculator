using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using System.Collections.Generic;
using System.Linq;

namespace SalaryCalculator.ViewModels
{
    public class ResultsViewModel : BaseVm
    {
        public ResultsViewModel()
        {
            GenerateList();
        }

        private List<ResultsModel> resultsModels = new List<ResultsModel>();

        public List<ResultsModel> ResultsModels
        {
            get { return resultsModels; }
            set { resultsModels = value; }
        }

        public void GenerateList()
        {
            List<Worker> workers = ApplicationDbContext.GetContext().Workers.ToList();

            List<ResultsModel> newlist = new List<ResultsModel>();
            foreach (Worker worker in workers)
            {
                decimal? basesalary = ApplicationDbContext.GetContext().Positions.FirstOrDefault(s => s.Id == worker.PositionId).BasicSalarePerWorkUnit;
                decimal? oversalary = ApplicationDbContext.GetContext().Positions.FirstOrDefault(s => s.Id == worker.PositionId).SalarePerWorkUnitOverTheNorm;
                int standartinunits = ApplicationDbContext.GetContext().LaborStandarts.FirstOrDefault(s => s.PositionId == worker.PositionId).StandartInUnits;
                int? workedunits = ApplicationDbContext.GetContext().WorkedUnitsOfLabors.FirstOrDefault(s => s.WorkerId == worker.Id)?.WorkedUnits;

                
                    newlist.Add(new ResultsModel
                    {
                        FIO = worker.FirstName + " " + worker.LastName + " " + worker.Patronimyc,
                        Result = workedunits <= standartinunits ? basesalary * workedunits : basesalary * standartinunits + oversalary * (workedunits - standartinunits)
                                              
                    });
                
            }

            resultsModels = newlist;
        }
    }
}