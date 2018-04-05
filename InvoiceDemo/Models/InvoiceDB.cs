namespace InvoiceDemo.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class InvoiceDB : DbContext
    {
        public InvoiceDB()
            : base("name=InvoiceDB")
        {
        }

        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.InvoiceItem)
                .WithRequired(e => e.Invoice)
                .HasForeignKey(e => e.Invoice_id);

            modelBuilder.Entity<InvoiceItem>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);
        }
    }
}
