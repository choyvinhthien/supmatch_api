using AutoMapper;
using eStore.DataAccess.Interface;
using eStore.Helpers;
using eStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eStore.DataAccess.Repository
{
    public class ShoppingCartRepository: IShoppingCartRepository
    {
        private readonly eStoreContext _context;
        private readonly IMapper _mapper;
        public ShoppingCartRepository(eStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateShoppingCart(ShoppingCartModel shoppingCart)
        {
            var newShoppingCart = _mapper.Map<ShoppingCart>(shoppingCart);
            _context.ShoppingCarts!.Add(newShoppingCart);
            await _context.SaveChangesAsync();
        }
        public async Task<List<ShoppingCartModel>> GetAllShoppingCarts()
        {
            var shoppingCarts = await _context.ShoppingCarts!.AsNoTracking().ToListAsync();
            return _mapper.Map<List<ShoppingCartModel>>(shoppingCarts);
        }
        public async Task<ShoppingCartModel> GetShoppingCartByUserId(string userId)
        {
            var shoppingCart = await _context.ShoppingCarts!.AsNoTracking()
                                                            .Include(shoppingCart => shoppingCart.CartItems).ThenInclude(cartItem => cartItem.Product).ThenInclude(product => product.ProductImages)
                                                            .Include(shoppingCart => shoppingCart.CartItems)
                                                            .Include(shoppingCart => shoppingCart.CartItems).ThenInclude(cartItem => cartItem.Product).ThenInclude(product => product.Category)
                                                            .Include(shoppingCart => shoppingCart.User)
                                                            .SingleOrDefaultAsync(shoppingCart => shoppingCart.UserId.Equals(userId));
            return _mapper.Map<ShoppingCartModel>(shoppingCart);
        }
    }
}
