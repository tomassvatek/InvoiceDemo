using System;
using System.Net;
using System.Web.Mvc;
using InvoiceDemo.Models;
using InvoiceDemo.Models.Viewmodels;
using InvoiceDemo.Repositories;

namespace InvoiceDemo.Controllers
{
    public class InvoiceItemsController : Controller
    {
        private readonly IInvoiceItemRepository _repository;

        public InvoiceItemsController(IInvoiceItemRepository repository)
        {
            _repository = repository;
        }

        // GET: InvoiceItems/Create
        public ActionResult Create(int? invoice)
        {
            if (invoice == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: InvoiceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateInvoiceItemVm invoiceItem, int? invoice)
        {
            if (ModelState.IsValid)
            {
                InvoiceItem item = new InvoiceItem()
                {
                    Name = invoiceItem.Name,
                    Price = invoiceItem.Price,
                    Quantity = invoiceItem.Quantity
                };

                try
                {
                    _repository.CreateInvoiceItem(item, invoice);
                    return RedirectToAction("Details", "Invoices", new {id = invoice});
                }
                catch (ArgumentException e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return View(invoiceItem);
        }

        // GET: InvoiceItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invoiceItem = _repository.GetInvoiceItem(id);

            if (invoiceItem == null)
            {
                return HttpNotFound();
            }

            DeleteInvoiceItemVm invoiceItemVm = new DeleteInvoiceItemVm()
            {
                Name = invoiceItem.Name,
                Price = invoiceItem.Price,
                Quantity = invoiceItem.Quantity
            };

            return View(invoiceItemVm);
        }

        // POST: InvoiceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteInvoiceItem(id);
            return RedirectToAction("Index", "Invoices");
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
