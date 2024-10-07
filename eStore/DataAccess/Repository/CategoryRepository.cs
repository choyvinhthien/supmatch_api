using AutoMapper;
using eStore.DataAccess.Interface;
using eStore.Models;
using Microsoft.EntityFrameworkCore;

namespace eStore.DataAccess.Repository
{
    public class CategoryRepository:ICategoryRepository
    {
        public readonly eStoreContext _context;
        public readonly IMapper _mapper;
        public CategoryRepository(eStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CategoryModel>> GetAllCategories()
        {
            var categories = await _context.Categories!.OrderBy(c => c.CategoryName).ToListAsync();
            return _mapper.Map<List<CategoryModel>>(categories);
        }
    }
}
