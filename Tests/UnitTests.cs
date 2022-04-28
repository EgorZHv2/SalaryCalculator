using NUnit.Framework;
using SalaryCalculator.Services;
using SalaryCalculator.Views.Pages;
using SalaryCalculator.Models.DataBase;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NavigationTest1()
        {
            DelegateCommand command  =  new DelegateCommand((obj) =>
            {
                PositionsPage expectedpg = new PositionsPage();
                PositionsPage actualpg = Navigation.Navigate(expectedpg) as PositionsPage;
                Assert.AreEqual(expectedpg, actualpg);
            });

        }
        [Test]
        public void NavigationTest2()
        {
            DelegateCommand command = new DelegateCommand((obj) =>
            {
                WorkersPage expectedpg = new WorkersPage();
                WorkersPage actualpg = Navigation.Navigate(expectedpg) as WorkersPage;
                Assert.AreEqual(expectedpg, actualpg);
            });

        }
        [Test]
        public void NavigationTest3()
        {
            DelegateCommand command = new DelegateCommand((obj) =>
            {
               PaymentFormsPage expectedpg = new PaymentFormsPage();
                PaymentFormsPage actualpg = Navigation.Navigate(expectedpg) as PaymentFormsPage;
                Assert.AreEqual(expectedpg, actualpg);
            });

        }
        [Test]
        public void NavigationTest4()
        {
            DelegateCommand command = new DelegateCommand((obj) =>
            {
                WorkedUnitsPage expectedpg = new WorkedUnitsPage();
                WorkedUnitsPage actualpg = Navigation.Navigate(expectedpg) as WorkedUnitsPage;
                Assert.AreEqual(expectedpg, actualpg);
            });

        }
        [Test]
        public void NavigationTest5()
        {
            DelegateCommand command = new DelegateCommand((obj) =>
            {
                ResultsPage expectedpg = new ResultsPage();
                ResultsPage actualpg = Navigation.Navigate(expectedpg) as ResultsPage;
                Assert.AreEqual(expectedpg, actualpg);
            });

        }



    }
}