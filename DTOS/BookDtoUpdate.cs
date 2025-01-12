﻿namespace BookHub.DTOS;

public class BookDtoUpdate
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateTime PublishedYear { get; set; } 
    public int AuthorId { get; set; }
}