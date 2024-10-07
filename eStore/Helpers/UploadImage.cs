using System.ComponentModel.DataAnnotations;
namespace eStore.Helpers
{
    public class UploadImage
    {
        public IFormFileCollection ImageFile { get; set; }
    }
}
