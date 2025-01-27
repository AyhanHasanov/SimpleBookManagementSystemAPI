##1. Repository Layer
**Responsibility:** The repository layer is responsible for data access and interacting with the database (or any other data source).

**Key Features:**
Abstracts away database logic from the rest of the application.
Ensures consistent data access and management logic.
Uses Entity Framework Core or other ORMs to query and manage data.
Repository interface (the methods the need to be implemented by the repository class)
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
##2. Service Layer
Responsibility: The service layer is responsible for business logic. It coordinates between the repository layer and the controller layer.
Key Features:
Contains application-specific logic and validation.
Acts as a middle layer between repositories and controllers.
Decides what to fetch, how to transform data, and what to return to the controller.

##3. Controller Layer
Responsibility: The controller layer is responsible for handling HTTP requests and returning HTTP responses to the client.
Key Features:
Acts as an interface between the client and the service layer.
Contains no business logic or database access logic (delegates this to the service layer).
Used for error hadnling (usually by returning the appropriate status code)
Returns appropriate HTTP status codes.


