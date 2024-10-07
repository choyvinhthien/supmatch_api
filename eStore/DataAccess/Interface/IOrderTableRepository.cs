using eStore.Models;

namespace eStore.DataAccess.Interface
{
    public interface IOrderTableRepository
    {
        Task<List<OrderTableModel>> GetAllOrderTables();
        Task CreateOrderTable(OrderTableModel orderTable);
    }
}
