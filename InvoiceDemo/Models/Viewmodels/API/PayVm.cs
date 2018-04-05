namespace InvoiceDemo.Models.Viewmodels.API
{
    public class PayVm
    {
        public int Id { get; set; }
        public bool Paid => true;
    }
}