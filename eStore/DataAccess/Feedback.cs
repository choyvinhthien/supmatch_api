namespace eStore.DataAccess
{
    public class Feedback
    {
        public Feedback()
        {
            FeedbackId = Guid.NewGuid().ToString();
        }
        public string FeedbackId { get; set; }
        public string Description { get; set; } = null!;
        public float Rating { get; set; }
        public DateTime RatingDate { get; set; }
        public int ProductId {  get; set; }
        public string UserId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
