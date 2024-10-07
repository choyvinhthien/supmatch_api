using AutoMapper;
using eStore.DataAccess;
using eStore.Models;

namespace eStore.Helpers
{
    public class ApplicationMapper: Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Product,  ProductModel>().ReverseMap();
            CreateMap<ProductImage, ProductImageModel>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartModel>().ReverseMap();
            CreateMap<CartItem, CartItemModel>().ReverseMap();
            CreateMap<OrderTable, OrderTableModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Feedback, FeedbackModel>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserModel>().ReverseMap();
        }
    }
}
