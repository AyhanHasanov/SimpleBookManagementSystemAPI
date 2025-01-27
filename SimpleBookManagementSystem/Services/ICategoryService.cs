using SimpleBookManagementSystem.DTOs.RequestDTOs;
using SimpleBookManagementSystem.DTOs.ResponseDTOs;

namespace SimpleBookManagementSystem.Services
{
    public interface ICategoryService
    {
        //get by id
        //get all
        //create
        //update
        //delete
        public Task<CategoryResponseDto> GetCategoryByIdAsync(int categoryId);
        public Task<ICollection<CategoryResponseDto>> GetAllCategoriesAsync();
        public Task<CategoryResponseDto> CreateCategoryAsync(CategoryRequestDto categoryRequestDto);
        public Task<CategoryResponseDto> UpdateCategoryAsync(int categoryId, CategoryRequestDto categoryRequestDto);
        public Task<CategoryResponseDto> DeleteCategoryAsync(int categoryId);
    }
}
