using SimpleBookManagementSystem.Data.Entities;
using SimpleBookManagementSystem.DTOs.RequestDTOs;
using SimpleBookManagementSystem.DTOs.ResponseDTOs;
using SimpleBookManagementSystem.Repository;
using System.Data;

namespace SimpleBookManagementSystem.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepo;
        private readonly IRepository<Category> _categoryRepo;
        public BookService(IRepository<Book> bookRepo, IRepository<Category> categoryRepo)
        {
            _bookRepo = bookRepo;
            _categoryRepo = categoryRepo;
        }
        public async Task<ICollection<BookResponseDto>> GetAllBooksAsync()
        {
            return (await _bookRepo.GetAllAsync())
                .Select(x => new BookResponseDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Price = x.Price,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt,
                    Categories = x
                            .Categories
                            .Select(category => new CategoryResponseDto()
                            {
                                Id = category.Id,
                                Title = category.Title,
                                CreatedAt = category.CreatedAt,
                                ModifiedAt = category.ModifiedAt
                            }
                            ).ToList()
                })
                .ToList();
        }

        //public async Task<ICollection<BookResponseDto>> GetAllBooksByFilterAsync(BookRequestDto bookRequestDto)
        //{
        //    Func<Book, bool> filter = book =>
        //    (string.IsNullOrEmpty(bookRequestDto.Title) ||
        //        book.Title.Contains(bookRequestDto.Title, StringComparison.OrdinalIgnoreCase))
        //    &&
        //    (string.IsNullOrEmpty(bookRequestDto.Author) ||
        //        book.Author.Contains(bookRequestDto.Author, StringComparison.OrdinalIgnoreCase))
        //    &&
        //    (bookRequestDto.Price == 0f ||
        //        book.Price == bookRequestDto.Price);

        //    return (_bookRepo.GetByFilter(filter))
        //        .Select(x => new BookResponseDto()
        //        {
        //            Id = x.Id,
        //            Title = x.Title,
        //            Author = x.Author,
        //            Price = x.Price,
        //            CreatedAt = x.CreatedAt,
        //            ModifiedAt = x.ModifiedAt,
        //            Categories = x
        //                    .Categories
        //                    .Select(category => new CategoryResponseDto()
        //                    {
        //                        Id = category.Id,
        //                        Title = category.Title,
        //                        CreatedAt = category.CreatedAt,
        //                        ModifiedAt = category.ModifiedAt
        //                    }
        //                    ).ToList()
        //        }
        //        )
        //        .ToList();
        //}

        public async Task<BookResponseDto> GetBookByIdAsync(int bookId)
        {
            var item = await _bookRepo.GetByIdAsync(bookId);

            if (item == null)
                throw new KeyNotFoundException();

            return new BookResponseDto()
            {
                Id = item.Id,
                Title = item.Title,
                Author = item.Author,
                Price = item.Price,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt,
                Categories = item
                            .Categories
                            .Select(category => new CategoryResponseDto()
                            {
                                Id = category.Id,
                                Title = category.Title,
                                CreatedAt = category.CreatedAt,
                                ModifiedAt = category.ModifiedAt
                            }
                            ).ToList()
            };
        }
        public async Task<BookResponseDto> CreateBookAsync(BookRequestDto bookRequestDto)
        {
            var book = await _bookRepo.CreateAsync(new Book()
            {
                Title = bookRequestDto.Title,
                Author = bookRequestDto.Author,
                Price = bookRequestDto.Price,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Categories = bookRequestDto.CategoryIds.Select(categoryId => _categoryRepo.GetByIdAsync(categoryId).Result).ToList()
            });

            return new BookResponseDto()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Categories = book
                            .Categories
                            .Select(category => new CategoryResponseDto()
                            {
                                Id = category.Id,
                                Title = category.Title,
                                CreatedAt = category.CreatedAt,
                                ModifiedAt = category.ModifiedAt
                            }
                            ).ToList()
            };
        }
        public async Task<BookResponseDto> UpdateBookAsync(int bookId, BookRequestDto bookRequestDto)
        {
            var item = await _bookRepo.GetByIdAsync(bookId);
            item.Title = bookRequestDto.Title;
            item.Author = bookRequestDto.Author;
            item.Price = bookRequestDto.Price;
            item.ModifiedAt = DateTime.Now;
            item.Categories.Clear();
            var newCategories = await Task.WhenAll(bookRequestDto.CategoryIds.Select(categoryId => _categoryRepo.GetByIdAsync(categoryId)));

            foreach (var cat in newCategories)
            {
                item.Categories.Add(cat);
            }

            await _bookRepo.UpdateAsync(item);

            return new BookResponseDto()
            {
                Id = item.Id,
                Title = item.Title,
                Author = item.Author,
                Price = item.Price,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt,
                Categories = item
                            .Categories
                            .Select(category => new CategoryResponseDto()
                            {
                                Id = category.Id,
                                Title = category.Title,
                                CreatedAt = category.CreatedAt,
                                ModifiedAt = category.ModifiedAt
                            }
                            ).ToList()
            };
        }
        public async Task<BookResponseDto> DeleteBookAsync(int bookId)
        {
            var item = await _bookRepo.GetByIdAsync(bookId);

            await _bookRepo.DeleteAsync(bookId);

            return new BookResponseDto()
            {
                Id = item.Id,
                Title = item.Title,
                Author = item.Author,
                Price = item.Price,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt,
                Categories = item
                            .Categories
                            .Select(category => new CategoryResponseDto()
                            {
                                Id = category.Id,
                                Title = category.Title,
                                CreatedAt = category.CreatedAt,
                                ModifiedAt = category.ModifiedAt
                            }
                            ).ToList()
            };
        }

        public async Task<ICollection<BookResponseDto>> GetBooksByAuthor(string name)
        {
            var items = (await _bookRepo.GetAllAsync()).Where(x => x.Author.Contains(name, StringComparison.OrdinalIgnoreCase));

            return items.Select(x => new BookResponseDto()
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Price = x.Price,
                CreatedAt = x.CreatedAt,
                ModifiedAt = x.ModifiedAt,
                Categories = x
                .Categories
                .Select(c => new CategoryResponseDto()
                {
                    Id = c.Id,
                    Title = c.Title,
                    CreatedAt = c.CreatedAt,
                    ModifiedAt = c.ModifiedAt
                })
                .ToList()
            }).ToList();

        }
    }
}
