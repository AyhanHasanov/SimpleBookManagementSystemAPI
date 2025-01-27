using Microsoft.AspNetCore.Mvc;
using SimpleBookManagementSystem.DTOs.RequestDTOs;
using SimpleBookManagementSystem.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleBookManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService service)
        {
            _bookService = service;
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var res = await _bookService.GetAllBooksAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        // GET api/<BookController>/5
        [HttpGet("id/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _bookService.GetBookByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        [HttpGet("{authorName}")]
        public async Task<IActionResult> GetBooksByAuthorName(string authorName)
        {
            try
            {
                var result = await _bookService.GetBooksByAuthor(authorName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookRequestDto bookRequestDto)
        {
            try
            {
                var result = await _bookService.CreateBookAsync(bookRequestDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BookRequestDto bookRequestDto)
        {
            try
            {
                var result = await _bookService.UpdateBookAsync(id, bookRequestDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _bookService.DeleteBookAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
