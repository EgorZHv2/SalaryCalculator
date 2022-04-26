using SalaryCalculator.Models;
using SalaryCalculator.Models.DataBase;
using SalaryCalculator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows;

namespace SalaryCalculator.ViewModels
{
    public class PFModalWindowVM:BaseVm
    {
        private PaymentFormsViewModel parentVm;
        private PaymentForm paymentform;
        

        public string BtContent { get; set; }

        public PFModalWindowVM(PaymentFormsViewModel parentVm)
        {
            this.parentVm = parentVm;
            BtContent = "Добавить";
            
        }

        public PFModalWindowVM(PaymentFormsViewModel parentVm, PaymentForm paymentForm)
        {
            this.parentVm = parentVm;
            this.paymentform = paymentForm;
            BtContent = "Изменить";
            Name = paymentForm.Name;
           
           

        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

       
        public ICommand Submit
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (paymentform == null)
                    {
                        PaymentForm pf = new PaymentForm()
                        {
                            Name = Name,
                            
                        };

                        parentVm.AddPaymentForm(pf);
                    }
                    else
                    {
                        PaymentForm pf = new PaymentForm()
                        {
                            Id = paymentform.Id,
                            Name = Name,
                            
                        };
                        parentVm.UpdatePaymentForm(pf);
                    }

                });
            }
        }
    }
}
