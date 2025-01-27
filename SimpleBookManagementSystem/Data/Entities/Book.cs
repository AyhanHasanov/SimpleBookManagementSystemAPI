namespace SimpleBookManagementSystem.Data.Entities
{
    public class Book : BaseEntity
    {
        public Book()
        {
            Categories = new HashSet<Category>();
        }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
