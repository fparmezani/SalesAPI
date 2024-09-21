namespace Sales.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; } = string.Empty;
        public List<SaleItem> Items { get; set; }
        public bool IsCancelled { get; set; }
    }

}
