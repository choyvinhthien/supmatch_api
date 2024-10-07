using eStore.Models;

namespace eStore.DataAccess.Interface
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItemModel>> GetAllOrderItems();
        Task CreateOrderItem(OrderItemModel orderItem);
    }
}
