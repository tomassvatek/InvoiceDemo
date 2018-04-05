using System.Linq;

namespace InvoiceDemo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Invoice")]
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            InvoiceItem = new HashSet<InvoiceItem>();
        }

        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Invoice_date { get; set; }

        public bool Paid { get; set; }

        private int _dueDate;

        public int Due_date
        {
            get => _dueDate;
            set => _dueDate = value > 0 ? value : 15;
        }

        [NotMapped]
        public decimal TotalPrice
        {
            get
            {
                var sum = InvoiceItem.Where(x => x.Invoice_id == Id).Sum(x => x.Price * x.Quantity);
                return sum;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceItem> InvoiceItem { get; set; }
    }
}
