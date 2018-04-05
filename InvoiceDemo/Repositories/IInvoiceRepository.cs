using System;
using System.Collections.Generic;
using InvoiceDemo.Models;

namespace InvoiceDemo.Repositories
{
    public interface IInvoiceRepository : IDisposable
    {
        IEnumerable<Invoice> GetInvoices();
        IEnumerable<Invoice> GetUnpaidInvoices();
        Invoice GetInvoice(int? id);
        void CreateInvoice(Invoice invoice);
        void EditInvoice(Invoice invoice);
    }
}
