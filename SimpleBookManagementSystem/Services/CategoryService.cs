using SimpleBookManagementSystem.Data.Entities;
using SimpleBookManagementSystem.DTOs.RequestDTOs;
using SimpleBookManagementSystem.DTOs.ResponseDTOs;
using SimpleBookManagementSystem.Repository;

namespace SimpleBookManagementSystem.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repo;
        public CategoryService(IRepository<Category> repo)
        {
            _repo = repo;
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryRequestDto categoryRequestDto)
        {
            Category category = new Category()
            {
                Title = categoryRequestDto.Title,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            await _repo.CreateAsync(category);

            //mapping
            CategoryResponseDto res = new CategoryResponseDto()
            {
                Title = category.Title,
                CreatedAt = category.CreatedAt,
                ModifiedAt = category.ModifiedAt
            };
            return res;
        }

        public Task<CategoryResponseDto> DeleteCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryResponseDto> GetCategoryByIdAsync(int categoryId)
        {
            var item = await _repo.GetByIdAsync(categoryId);

            //mapping
            CategoryResponseDto res = new CategoryResponseDto()
            {
                Title = item.Title,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt
            };

        }

        public Task<CategoryResponseDto> UpdateCategoryAsync(int categoryId, CategoryRequestDto categoryRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
