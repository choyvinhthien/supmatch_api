using eStore.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            CartItems = new HashSet<CartItemModel>();
            OrderItems = new HashSet<OrderItemModel>();
            ProductImages = new HashSet<ProductImageModel>();
            Feedbacks = new HashSet<FeedbackModel>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int UnitsInstock { get; set; }
        public DateTime ReleasedDate { get; set; }
        public int ratingAverage { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<CartItemModel> CartItems { get; set; }
        public virtual ICollection<OrderItemModel> OrderItems { get; set; }
        public virtual ICollection<ProductImageModel> ProductImages { get; set; }
        public virtual CategoryModel Category { get; set; } = null!;
        public virtual ICollection<FeedbackModel> Feedbacks { get; set; }
    }
}
