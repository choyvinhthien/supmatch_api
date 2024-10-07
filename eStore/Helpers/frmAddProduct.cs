using System.ComponentModel.DataAnnotations;
namespace eStore.Helpers
{
    public class frmAddProduct
    {
        [Required]
        public string ProductName { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string CategoryId { get; set; } = null!;
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int UnitsInstock { get; set; }
    }
}
