using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InvoiceDemo.Models.Viewmodels
{
    public class InvoiceDetailVm
    {
        public int Id { get; set; }

        [DisplayName("Invoice date")]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}")]
        public DateTime InvoiceDate { get; set; }

        private DateTime _dueDate;

        [DisplayName("Due date")]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}")]
        public DateTime DueDate
        {
            get => _dueDate;
            set => _dueDate = InvoiceDate.AddDays(value.Day);
        }   

        [DisplayName("Total price")]
        public decimal TotalPrice { get; set; }

        public string CustomPrice => String.Format("{0:C}", TotalPrice);

        private string _paid;

        [DisplayName("Payment status")]
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

        public virtual ICollection<InvoiceItem> InvoiceItem { get; set; }
    }
}