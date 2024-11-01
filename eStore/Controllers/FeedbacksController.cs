using eStore.DataAccess.Interface;
using eStore.DataAccess.Repository;
using eStore.Helpers;
using eStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IProductsRepository _productsRepository;

        public FeedbacksController(IFeedbackRepository feedbackRepository, IProductsRepository productsRepository)
        {
            _feedbackRepository = feedbackRepository;
            _productsRepository = productsRepository;
        }
        [HttpPost("AddFeedback")]
        /*[Authorize(Roles = AppRole.Customer)]*/
        public async Task<IActionResult> AddFeedback([FromBody] frmAddFeedback frmAddFeedback)
        {
            FeedbackModel feedbackModel = new FeedbackModel
            {
                Rating = frmAddFeedback.Rating,
                Description = frmAddFeedback.Description,
                RatingDate = DateTime.UtcNow,
                UserId = frmAddFeedback.UserId,
                ProductId = frmAddFeedback.ProductId
            };
            try
            {
                await _feedbackRepository.CreateFeedback(feedbackModel);
                await _productsRepository.UpdateRatingAverage(frmAddFeedback.ProductId);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
