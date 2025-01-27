using Microsoft.AspNetCore.Mvc;
using SimpleBookManagementSystem.DTOs.RequestDTOs;
using SimpleBookManagementSystem.DTOs.ResponseDTOs;
using SimpleBookManagementSystem.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleBookManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService service)
        {
            //injecting dependecies
            _categoryService = service;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IEnumerable<CategoryResponseDto>> Get()
        {
            //get all
            return await _categoryService.GetAllCategoriesAsync();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var res = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(res);
            }
            catch (KeyNotFoundException keyNotFoundEx)
            {
                return StatusCode(404, $"Category with id {id} does not exist!\n{keyNotFoundEx.Message}");
            }
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryRequestDto categoryRequestDto)
        {
            try
            {
                var res = await _categoryService.CreateCategoryAsync(categoryRequestDto);
                return Ok(res);
            }
            catch (ArgumentNullException nullEx)
            {
                return StatusCode(400, nullEx.Message);
            }
            catch (ArgumentException argEx)
            {
                return StatusCode(400, argEx.Message);
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryRequestDto categoryRequestDto)
        {
            try
            {
                var res = await _categoryService.UpdateCategoryAsync(id, categoryRequestDto);
                return Ok(res);
            }
            catch (KeyNotFoundException keyNotFoundEx)
            {
                return StatusCode(404, $"Category with id {id} does not exist!\n{keyNotFoundEx.Message}");
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _categoryService.DeleteCategoryAsync(id);
                return Ok(res);
            }
            catch (KeyNotFoundException keyNotFoundEx)
            {
                return StatusCode(404, $"Category with id {id} does not exists!\n{keyNotFoundEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
    }
}
