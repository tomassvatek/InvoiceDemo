using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using InvoiceDemo.Models;

namespace InvoiceDemo.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDB _db;
        private bool _disposed = false;


        public InvoiceRepository()
        {
            _db = new InvoiceDB();
        }

        public IEnumerable<Invoice> GetInvoices()
        {
            return _db.Invoice.ToList();
        }

        public IEnumerable<Invoice> GetUnpaidInvoices()
        {
            return _db.Invoice.Where(x => x.Paid == false).ToList();
        }

        public Invoice GetInvoice(int? id)
        {
            return _db.Invoice.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }


        public void CreateInvoice(Invoice invoice)
        {
            _db.Invoice.Add(invoice);
            _db.SaveChanges();
        }

        public void EditInvoice(Invoice invoice)
        {
            var exists = _db.Invoice.Where(x => x.Id == invoice.Id);

            if (exists == null)
            {
                throw new ArgumentException("Invoice does not exist.");
            }

            _db.Entry(invoice).State = EntityState.Modified;
            _db.SaveChanges();
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