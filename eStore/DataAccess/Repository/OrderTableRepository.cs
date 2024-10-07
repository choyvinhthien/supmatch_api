using AutoMapper;
using eStore.DataAccess.Interface;
using eStore.Models;
using Microsoft.EntityFrameworkCore;

namespace eStore.DataAccess.Repository
{
    public class OrderTableRepository: IOrderTableRepository
    {
        private readonly eStoreContext _context;
        private readonly IMapper _mapper;
        public OrderTableRepository(eStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<OrderTableModel>> GetAllOrderTables()
        {
            var orderTables = await _context.OrderTables!.AsNoTracking().ToListAsync();
            return _mapper.Map<List<OrderTableModel>>(orderTables);
        }
        public async Task CreateOrderTable(OrderTableModel orderTable)
        {
            var newOrderTable = _mapper.Map<OrderTable>(orderTable);
            _context.OrderTables!.Add(newOrderTable);
            await _context.SaveChangesAsync();
        }
    }
}
