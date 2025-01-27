namespace SimpleBookManagementSystem.Data.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }
        public string Title { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
