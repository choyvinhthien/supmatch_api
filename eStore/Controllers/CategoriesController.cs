using eStore.DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet("GetAllCategories")]
        /*[Authorize(Roles = AppRole.Customer)]*/
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                return Ok(await _categoryRepository.GetAllCategories());
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
