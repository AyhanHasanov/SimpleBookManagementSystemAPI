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

        public async Task<CategoryResponseDto> GetCategoryByIdAsync(int categoryId)
        {
            var item = await _repo.GetByIdAsync(categoryId);

            if (item == null)
                throw new KeyNotFoundException();

            //mapping
            CategoryResponseDto res = new CategoryResponseDto()
            {
                Title = item.Title,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt
            };

            return res;
        }

        public async Task<ICollection<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            ICollection<CategoryResponseDto> result = new List<CategoryResponseDto>();

            return (await _repo.GetAllAsync()).
                Select(x => new CategoryResponseDto()
                {
                    Title = x.Title,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt
                })
                .ToList();
        }
        public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryRequestDto categoryRequestDto)
        {

            if (string.IsNullOrEmpty(categoryRequestDto.Title))
                throw new ArgumentNullException("Category name must not be empty!");

            if(categoryRequestDto.Title.Length < 3)
                throw new ArgumentException("Category title must be at least 3 characters long!");

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


        public async Task<CategoryResponseDto> UpdateCategoryAsync(int categoryId, CategoryRequestDto categoryRequestDto)
        {
            var item = await _repo.GetByIdAsync(categoryId);

            if (item == null)
                throw new KeyNotFoundException();

            item.Title = categoryRequestDto.Title;
            item.ModifiedAt = DateTime.Now;

            await _repo.UpdateAsync(item);

            CategoryResponseDto res = new CategoryResponseDto()
            {
                Title = item.Title,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt
            };

            return res;
        }
        public async Task<CategoryResponseDto> DeleteCategoryAsync(int categoryId)
        {
            var item = await _repo.GetByIdAsync(categoryId);

            if (item == null)
                throw new KeyNotFoundException();

            await _repo.DeleteAsync(categoryId);
            CategoryResponseDto res = new CategoryResponseDto()
            {
                Title = item.Title,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt
            };
            return res;
        }
    }
}
