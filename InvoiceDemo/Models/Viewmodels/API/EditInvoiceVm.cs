using System;

namespace InvoiceDemo.Models.Viewmodels.API
{
    public class EditInvoiceVm
    {
        public DateTime InvoiceDate { get; set; }
        public int DueDate { get; set; }    
        public bool Paid { get; set; }
    }
}