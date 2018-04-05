using System;

namespace InvoiceDemo.Models.Viewmodels.API
{
    public class UnpaidInvoiceVm
    {
        //public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int DueDate { get; set; }

        private string _paid;
        public string Paid
        {
            get => _paid;
            set
            {
                if (value == "True")
                {
                    _paid = "Paid";
                }
                else
                {
                    _paid = "Unpaid";
                }
            }
        }
        public decimal TotalPrice { get; set; }

    }
}