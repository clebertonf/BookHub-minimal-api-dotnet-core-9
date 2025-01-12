using AutoMapper;
using BookHub.Context;
using BookHub.DTOS;
using BookHub.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookHub.EndpointHandlers;

public static class BookHandlers
{
    public static async Task<Ok<IEnumerable<BookDto>>> GetAllBooks(ApplicationDbContext context, IMapper mapper)
    {
        var books = await context.Books.ToListAsync();
        return TypedResults.Ok(mapper.Map<IEnumerable<BookDto>>(books));
    }

    public static async Task<Results<Ok<BookDto>, NotFound>> GetBookById(int id, ApplicationDbContext context, IMapper mapper)
    {
        var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(mapper.Map<BookDto>(book));
    }

    public static async Task<CreatedAtRoute<BookDto>> AddBook(ApplicationDbContext context, IMapper mapper, [FromBody] BookDtoCreate bookDtoCreate)
    {
        var book = mapper.Map<Book>(bookDtoCreate);
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
        var bookDto = mapper.Map<BookDto>(book);
        
        return TypedResults.CreatedAtRoute(bookDto, "GetBookById", new { id = book.Id });
    }

    public static async Task<Results<NoContent, NotFound>> UpdateBook(ApplicationDbContext context, IMapper mapper, [FromBody] BookDtoUpdate bookDtoUpdate, int id)
    {
        var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if(book is null)
            return TypedResults.NotFound();
        
        mapper.Map(bookDtoUpdate, book);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound>> DeleteBook(ApplicationDbContext context, int id)
    {
        var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if(book is null)
            return TypedResults.NotFound();
        
        context.Books.Remove(book);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}