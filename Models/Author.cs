namespace BookHub.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public IEnumerable<Book>? Books { get; set; }
}