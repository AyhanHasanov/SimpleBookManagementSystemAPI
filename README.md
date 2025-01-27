## 1. Repository Layer

**Responsibility:** The repository layer is responsible for data access and interacting with the database (or any other data source).

**Key Features:**
- Abstracts away database logic from the rest of the application.
- Ensures consistent data access and management logic.
- Uses Entity Framework Core or other ORMs to query and manage data.
- Repository interface (the methods the need to be implemented by the repository class)
```c#
public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<ICollection<T>> GetAllAsync();
    ICollection<T> GetByFilter(Func<T, bool> predicate);
    Task<T> CreateAsync(T item);
    Task<T> UpdateAsync(T item);
    Task<T> DeleteAsync(int id);
}
```
Fragment of the Reposiroty Class:
``` c#
public class Repository<T> : IRepository<T>
    where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual async Task<T> CreateAsync(T item)
    {
        _context.Set<T>().Add(item);
        await _context.SaveChangesAsync();
        return item;
    }
}
```
## 2. Service Layer

**Responsibility:** The service layer is responsible for business logic. It coordinates between the repository layer and the controller layer.

**Key Features:**
- Contains application-specific logic and validation.
- Acts as a middle layer between repositories and controllers.
- Decides what to fetch, how to transform data, and what to return to the controller.

In the following example the repository of type Category is being used in the CategoryService through dependency injection
```c#
public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _repo;
    public CategoryService(IRepository<Category> repo)
    {
        //dependency injection
        _repo = repo;
    }

    public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryRequestDto categoryRequestDto)
    {

        if (string.IsNullOrEmpty(categoryRequestDto.Title))
            throw new ArgumentNullException("Category name must not be empty!");

        if (categoryRequestDto.Title.Length < 3)
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
            Id = category.Id,
            Title = category.Title,
            CreatedAt = category.CreatedAt,
            ModifiedAt = category.ModifiedAt
        };
        return res;
    }
}
```

## 3. Controller Layer

**Responsibility:** The controller layer is responsible for handling HTTP requests and returning HTTP responses to the client.

**Key Features:**
- Acts as an interface between the client and the service layer.
- Contains no business logic or database access logic (delegates this to the service layer).
- Used for error hadnling (usually by returning the appropriate status code)
- Returns appropriate HTTP status codes.

The controller uses the service by calling the relevant methods of the services needed. Note that in the controller no bussines logic is implemented - only error handling and status code returns
```c#
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
}
```


