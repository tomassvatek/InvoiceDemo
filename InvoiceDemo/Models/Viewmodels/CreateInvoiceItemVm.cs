using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceDemo.Models.Viewmodels
{
    public class CreateInvoiceItemVm    
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Quantity cannot be negative number")]
        public int Quantity { get; set; }
    }
}