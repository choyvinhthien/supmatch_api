using eStore.DataAccess;

namespace eStore.Models
{
    public class ProductImageModel
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string ImageName { get; set; } = null!;
        public byte[] ImageData { get; set; } = null!;
        public double ImageSize { get; set; }
    }
}
