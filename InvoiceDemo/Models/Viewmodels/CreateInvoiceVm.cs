using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InvoiceDemo.Models.Viewmodels
{
    public class CreateInvoiceVm
    {
        [DisplayName("Invoice date")]
        [DisplayFormat(DataFormatString = "{0:d.M. yyyy}")]
        public DateTime InvoiceDate { get; set; }

        [DisplayName("Due date")]
        [Range(1, 30, ErrorMessage = "Due date must be between 1 and 30 interval")]
        public int DueDate { get; set; }       

        [DisplayName("Payment status")]
        public bool Paid { get; set; }
    }
}