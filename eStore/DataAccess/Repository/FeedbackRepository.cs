using AutoMapper;
using eStore.DataAccess.Interface;
using eStore.Models;

namespace eStore.DataAccess.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly eStoreContext _context;
        private readonly IMapper _mapper;
        public FeedbackRepository(eStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateFeedback(FeedbackModel feedbackModel)
        {
            var feedback = _mapper.Map<Feedback>(feedbackModel);
            _context.Feedbacks!.Add(feedback);
            await _context.SaveChangesAsync();
        }
    }
}
