using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using InvoiceDemo.Models;
using InvoiceDemo.Models.Viewmodels;
using InvoiceDemo.Models.Viewmodels.API;
using InvoiceDemo.Repositories;
using JsonPatch;
using EditInvoiceVm = InvoiceDemo.Models.Viewmodels.EditInvoiceVm;

namespace InvoiceDemo.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceRepository _repository;

        public InvoicesController(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = from e in _repository.GetInvoices()
                           select new InvoiceDetailVm
                           {
                               Id = e.Id,
                               InvoiceDate = e.Invoice_date,
                               DueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, e.Due_date),
                               Paid = e.Paid.ToString(),
                               TotalPrice = e.TotalPrice
                           };

            return View(invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invoice = _repository.GetInvoice(id);

            if (invoice == null)
            {
                return HttpNotFound();
            }

            InvoiceDetailVm detailVm = new InvoiceDetailVm()
            {
                Id = invoice.Id,
                InvoiceDate = invoice.Invoice_date,
                DueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, invoice.Due_date),
                Paid = invoice.Paid.ToString(),
                TotalPrice = invoice.TotalPrice,
                InvoiceItem = invoice.InvoiceItem
            };

            return View(detailVm);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateInvoiceVm invoiceVm)
        {
            if (ModelState.IsValid)
            {
                Invoice invoice = new Invoice()
                {
                    Invoice_date = invoiceVm.InvoiceDate,
                    Paid = invoiceVm.Paid,
                    Due_date = invoiceVm.DueDate
                };

                _repository.CreateInvoice(invoice);
                return RedirectToAction("Details", new { id = invoice.Id });
            }

            return View(invoiceVm);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invoice = _repository.GetInvoice(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            EditInvoiceVm vm = new EditInvoiceVm
            {
                Id = invoice.Id,
                InvoiceDate = invoice.Invoice_date,
                DueDate = invoice.Due_date,
                Paid = invoice.Paid,
                InvoiceItem = from e in invoice.InvoiceItem
                              select new InvoiceItemVm
                              {
                                  Id = e.Id,
                                  Name = e.Name,
                                  Price = e.Price,
                                  Quantity = e.Quantity
                              }
            };
            return View(vm);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditInvoiceVm invoiceVm)
        {
            if (ModelState.IsValid)
            {
                Invoice invoice = new Invoice()
                {
                    Id = invoiceVm.Id,
                    Invoice_date = invoiceVm.InvoiceDate,
                    Due_date = invoiceVm.DueDate,
                    Paid = invoiceVm.Paid
                };

                _repository.EditInvoice(invoice);
                return RedirectToAction("Index");
            }
            return View(invoiceVm);
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}

namespace InvoiceDemo.Controllers.Api
{
    public class InvoicesController : ApiController
    {
        private readonly InvoiceRepository _repository = new InvoiceRepository();

        public IHttpActionResult GetUnpaidInvoices()
        {
            var result = from e in _repository.GetUnpaidInvoices()
                         select new UnpaidInvoiceVm
                         {
                             InvoiceDate = e.Invoice_date,
                             DueDate = e.Due_date,
                             Paid = e.Paid.ToString(),
                             TotalPrice = e.TotalPrice
                         };

            return Ok(result);
        }

        // PUT: api/invoices
        public IHttpActionResult Put([FromBody] PayVm payViewmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Invoice invoice = new Invoice()
            {
                Id = payViewmodel.Id,
                Paid = payViewmodel.Paid
            };

            try
            {
                _repository.EditInvoice(invoice);
                return Ok();

            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [System.Web.Http.HttpPatch]
        public IHttpActionResult EditInvoice(int id, [FromBody]JsonPatchDocument<Models.Viewmodels.API.EditInvoiceVm> patchData)
        {
            var invoice = _repository.GetInvoice(id);
            if (invoice == null)
            {
                return BadRequest("Invoice does not exist.");
            }

            if (patchData == null)
            {
                return BadRequest("Request body is empty");
            }

            Models.Viewmodels.API.EditInvoiceVm vm = new Models.Viewmodels.API.EditInvoiceVm
            {
                InvoiceDate = invoice.Invoice_date,
                DueDate = invoice.Due_date,
                Paid = invoice.Paid
            };

            patchData.ApplyUpdatesTo(vm);

            Invoice inv = new Invoice()
            {
                Id = id,
                Invoice_date = vm.InvoiceDate,
                Due_date = vm.DueDate,
                Paid = vm.Paid
            };

            _repository.EditInvoice(inv);
            return Ok();
        }
    }
}