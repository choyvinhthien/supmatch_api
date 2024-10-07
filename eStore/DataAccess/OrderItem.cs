namespace eStore.DataAccess
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderTableId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual OrderTable OrderTable { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
