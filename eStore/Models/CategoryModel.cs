

namespace eStore.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            Products = new HashSet<ProductModel>();
            CategoryId = Guid.NewGuid().ToString();
        }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
