using System;
using System.Collections.Generic;
using System.Linq;
using InvoiceDemo.Models;

namespace InvoiceDemo.Repositories
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly InvoiceDB _db;
        private bool _disposed = false;

        public InvoiceItemRepository()
        {
            _db = new InvoiceDB();
        }

        public IEnumerable<InvoiceItem> GetInvoiceItems()
        {
            return _db.InvoiceItem.ToList();
        }

        public InvoiceItem GetInvoiceItem(int? id)
        {
            return _db.InvoiceItem.Find(id);
        }

        public void CreateInvoiceItem(InvoiceItem invoiceItem, int? invoiceId)
        {
            var invoiceExits = _db.Invoice.Find(invoiceId); 
            if (invoiceExits == null)
            {
                throw new ArgumentException();
            }

            invoiceItem.Invoice_id = invoiceId.Value;
            _db.InvoiceItem.Add(invoiceItem);
            _db.SaveChanges();
        }

        public void DeleteInvoiceItem(int? id)
        {
            var item = _db.InvoiceItem.Find(id);
            if (item != null)
            {
                _db.InvoiceItem.Remove(item);
                _db.SaveChanges();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}