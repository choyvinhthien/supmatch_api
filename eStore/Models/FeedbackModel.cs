using eStore.DataAccess;

namespace eStore.Models
{
    public class FeedbackModel
    {
        public FeedbackModel()
        {
            FeedbackId = Guid.NewGuid().ToString();
        }
        public string FeedbackId { get; set; }
        public string Description { get; set; } = null!;
        public float Rating { get; set; }
        public DateTime RatingDate { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public virtual ProductModel? Product { get; set; } = null!;
        public virtual ApplicationUserModel User { get; set; } = null!;

    }
}
