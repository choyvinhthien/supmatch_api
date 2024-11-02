namespace eStore.Helpers
{
    public class frmUpdateProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int UnitsInstock { get; set; }
    }
}
