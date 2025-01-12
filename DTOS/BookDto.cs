namespace BookHub.DTOS;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateTime PublishedYear { get; set; } 
    public int AuthorId { get; set; }
}