using SimpleBookManagementSystem.DTOs.RequestDTOs;
using SimpleBookManagementSystem.DTOs.ResponseDTOs;

namespace SimpleBookManagementSystem.Services
{
    public interface IBookService
    {
        //get by id
        //get all
        //create
        //update
        //delete
        public Task<BookResponseDto> GetBookByIdAsync(int bookId);
        public Task<ICollection<BookResponseDto>> GetAllBooksAsync();
        //public Task<ICollection<BookResponseDto>> GetAllBooksByFilterAsync(BookRequestDto bookRequestDto);
        public Task<BookResponseDto> CreateBookAsync(BookRequestDto bookRequestDto);
        public Task<BookResponseDto> UpdateBookAsync(int bookId, BookRequestDto bookRequestDto);
        public Task<BookResponseDto> DeleteBookAsync(int bookId);

        public Task<ICollection<BookResponseDto>> GetBooksByAuthor(string name);
    }
}
