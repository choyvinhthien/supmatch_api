using System.IO.Enumeration;

namespace eStore.DataAccess
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
            CategoryId = Guid.NewGuid().ToString();
        }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
