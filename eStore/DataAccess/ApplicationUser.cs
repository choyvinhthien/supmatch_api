using Microsoft.AspNetCore.Identity;

namespace eStore.DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            OrderTables = new HashSet<OrderTable>();
            Feedbacks = new HashSet<Feedback>();
        }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender {  get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Status { get; set; }
        public virtual ICollection<OrderTable> OrderTables { get; set; }
        public virtual ShoppingCart ShoppingCarts { get; set; } = null!;
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
