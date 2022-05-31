using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SalaryCalculator.Views.Windows;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SalaryCalculator.Interfaces;

namespace SalaryCalculator.ViewModels
{
    public class PaymentFormsViewModel : BaseVm, IVMWithDataGrid
    {

        public PaymentFormsViewModel()
        {
                
            GenerateList();

        }


        private List<PaymentForm> paymentforms = new List<PaymentForm>();
        private PaymentFormsModalWindow ModalWindow;
        private PFModalWindowVM ModalWindowModel;
        public List<PaymentForm> PaymentForms
        {
            get { return paymentforms; }
            set
            {
                paymentforms = value;
                OnPropertyChanged();
            }
        }
        private PaymentForm selectedform;
        public PaymentForm SelectedForm
        {
            get { return selectedform; }
            set
            {
                selectedform = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenAddDiag
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ModalWindow = new PaymentFormsModalWindow();
                    ModalWindowModel = new PFModalWindowVM(this);
                    ModalWindow.DataContext = ModalWindowModel;
                    ModalWindow.Activate();
                    ModalWindow.Show();
                });
            }
        }
        public ICommand OpenUpdateDiag
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    PaymentForm pf = obj as PaymentForm;
                    ModalWindow = new PaymentFormsModalWindow();
                    ModalWindowModel = new PFModalWindowVM(this, pf);
                    ModalWindow.DataContext = ModalWindowModel;
                    ModalWindow.Activate();
                    ModalWindow.Show();
                });
            }
        }
        public ICommand DeletePaymentForm
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        PaymentForm pf = obj as PaymentForm;
                        ApplicationDbContext.GetContext().PaymentForms.Remove(ApplicationDbContext.GetContext().PaymentForms.Find(pf.Id));
                        ApplicationDbContext.GetContext().SaveChanges();
                        GenerateList();
                    }
                    catch 
                    {
                        MessageBox.Show("Вы пытаетесь удалить используемую форму оплаты труда");
                    }
                });
            }
        }
        public void AddPaymentForm(PaymentForm paymentForm)
        {
            ApplicationDbContext.GetContext().PaymentForms.Add(paymentForm);
            ApplicationDbContext.GetContext().SaveChanges();
            ModalWindow.Hide();
            ModalWindow.Close();
            GenerateList();
        }
        public void UpdatePaymentForm(PaymentForm paymentForm)
        {
            var dbpaymentform = ApplicationDbContext.GetContext().PaymentForms.FirstOrDefault(s => s.Id == paymentForm.Id);
            dbpaymentform.Name = paymentForm.Name;
            ApplicationDbContext.GetContext().SaveChanges();
            ModalWindow.Hide();
            ModalWindow.Close();
            GenerateList();
        }
        public void GenerateList()
        {
            List<PaymentForm> bdlist = ApplicationDbContext.GetContext().PaymentForms.ToList();
            PaymentForms = bdlist;
        }
    }
}