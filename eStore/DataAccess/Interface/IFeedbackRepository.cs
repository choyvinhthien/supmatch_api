using eStore.Models;

namespace eStore.DataAccess.Interface
{
    public interface IFeedbackRepository
    {
        Task CreateFeedback(FeedbackModel feedbackModel);
    }
}
