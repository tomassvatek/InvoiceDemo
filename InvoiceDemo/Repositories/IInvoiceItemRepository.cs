using System;
using System.Collections.Generic;
using InvoiceDemo.Models;

namespace InvoiceDemo.Repositories
{
    public interface IInvoiceItemRepository : IDisposable
    {
        IEnumerable<InvoiceItem> GetInvoiceItems();
        InvoiceItem GetInvoiceItem(int? id);
        void CreateInvoiceItem(InvoiceItem invoiceItem, int? invoiceId);
        void DeleteInvoiceItem(int? id);
    }
}
