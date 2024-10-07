using System.ComponentModel.DataAnnotations;
namespace eStore.DataAccess
{
    public partial class ProductImage
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string ImageName { get; set; } = null!;
        public byte[] ImageData { get; set; } = null!;
        public double ImageSize { get; set; }

        public virtual Product Product { get; set; } = null!;

    }
}
