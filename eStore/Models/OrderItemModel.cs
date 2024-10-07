using eStore.DataAccess;

namespace eStore.Models
{
    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public int OrderTableId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual OrderTableModel OrderTable { get; set; } = null!;
        public virtual ProductModel Product { get; set; } = null!;
    }
}
