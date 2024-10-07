using AutoMapper;
using eStore.DataAccess.Interface;
using eStore.Helpers;
using eStore.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace eStore.DataAccess.Repository
{
    public class CartItemRepository: ICartItemRepository
    {
        private readonly eStoreContext _context;
        private readonly IMapper _mapper;
        public CartItemRepository(eStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CartItemModel>> GetCartItemsByShoppingCartId(int shoppingCartId)
        {
            var cartItems = await _context.CartItems!.AsNoTracking().Where(cartItem => cartItem.ShoppingCartId == shoppingCartId).ToListAsync();
            return _mapper.Map<List<CartItemModel>>(cartItems);
        }
        public async Task AddToCart(CartItemModel cartItemModel)
        {
            var cartItem = _mapper.Map<CartItem>(cartItemModel);
            _context.CartItems!.Add(cartItem);
            await _context.SaveChangesAsync();
        }
        public async Task<List<CartItemModel>> GetAllCartItems()
        {
            var cartItems = await _context.CartItems!.AsNoTracking().ToListAsync();
            return _mapper.Map<List<CartItemModel>>(cartItems);
        }
        public async Task<CartItemModel> GetCartItemByCartItemId(int cartItemId)
        {
            var cartItem = await _context.CartItems!.AsNoTracking().SingleOrDefaultAsync(cartItem => cartItem.CartItemId == cartItemId);
            return _mapper.Map<CartItemModel>(cartItem);
        }
        public async Task IncreaseQuantity(CartItemModel cartItemModel)
        {
            var updateCartItem = _mapper.Map<CartItem>(cartItemModel);
            updateCartItem.Quantity++;
            _context.CartItems!.Update(updateCartItem);
            await _context.SaveChangesAsync();
        }
        public async Task DecreaseQuantity(CartItemModel cartItemModel)
        {
            var updateCartItem = _mapper.Map<CartItem>(cartItemModel);
            updateCartItem.Quantity--;
            _context.CartItems!.Update(updateCartItem);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveCartItem(int cartItemId)
        {
            var removeCartItem = await _context.CartItems.SingleOrDefaultAsync(cartItem => cartItem.CartItemId == cartItemId);
            if (removeCartItem != null)
            {
                _context.CartItems!.Remove(removeCartItem);
                await _context.SaveChangesAsync();
            }
        }
        public async Task RemoveListCartItemsByShoppingCartId(int shoppingCartId)
        {
            var removedCartItems = await _context.CartItems!.AsNoTracking().Where(cartItem => cartItem.ShoppingCartId == shoppingCartId).ToListAsync();
            _context.CartItems!.RemoveRange(removedCartItems);
            await _context.SaveChangesAsync();
        }
        public async Task<CartItemModel> CheckAvailableCart(string userId, int productId)
        {
            var cartItems = _context.CartItems!.Where(cartItems => cartItems.ShoppingCart.UserId.Equals(userId) && cartItems.ProductId == productId).Include(cartItems=>cartItems.Product).Include(cartItems=>cartItems.ShoppingCart);
            if (cartItems.Any())
            {
                return _mapper.Map<CartItemModel>(cartItems.First());

            }
            return null;
        }
        public async Task AddQuantity(CartItemModel cartItem, int additionalQuantity, int maxQuantity)
        {

            var checkQuantity = cartItem.Quantity + additionalQuantity;
            var existingCartItem = await _context.CartItems.FindAsync(cartItem.CartItemId);
            if (existingCartItem != null) {
                existingCartItem.Quantity = Math.Clamp(checkQuantity, 1, maxQuantity);
                _context.CartItems.Update(existingCartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
