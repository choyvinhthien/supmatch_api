using AutoMapper;
using eStore.DataAccess.Interface;
using eStore.Models;
using Microsoft.EntityFrameworkCore;

namespace eStore.DataAccess.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly eStoreContext _context;
        private readonly IMapper _mapper;
        public OrderItemRepository(eStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<OrderItemModel>> GetAllOrderItems()
        {
            var orderItems = await _context.OrderItems!.AsNoTracking().ToListAsync();
            return _mapper.Map<List<OrderItemModel>>(orderItems);
        }
        public async Task CreateOrderItem(OrderItemModel orderItem)
        {
            var newOrderItem = _mapper.Map<OrderItem>(orderItem);
            _context.OrderItems!.Add(newOrderItem);
            await _context.SaveChangesAsync();
        }
    }
}
