using AutoMapper;
using BookHub.Context;
using BookHub.DTOS;
using BookHub.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookHub.EndpointHandlers;

public static class AuthorHandlers
{
    public static async Task<Ok<IEnumerable<AuthorDto>>> GetAllAuthors(ApplicationDbContext context, IMapper mapper)
    {
        var autors = await context.Authors.ToListAsync();
        return TypedResults.Ok(mapper.Map<IEnumerable<AuthorDto>>(autors));
    }
    
    public static Task<Ok<IEnumerable<AuthorDto>>> GetAllAuthorsWithBooks(ApplicationDbContext context, IMapper mapper)
    {
        var autors = context.Authors
            .Include(a => a.Books)
            .Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                BirthDate = a.BirthDate,
                Books = a.Books.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Genre = b.Genre,
                    PublishedYear = b.PublishedYear,
                    AuthorId = b.AuthorId
                }).ToList()
            })
            .ToList();
        
        return Task.FromResult(TypedResults.Ok(mapper.Map<IEnumerable<AuthorDto>>(autors)));
    }

    public static async Task<Results<Ok<AuthorDto>, NotFound>> GetAuthorById(int id, ApplicationDbContext context, IMapper mapper)
    {
        var author = await context.Authors.FindAsync(id);
        if (author is null)
            return TypedResults.NotFound();
        
        return TypedResults.Ok(mapper.Map<AuthorDto>(await context.Authors.FirstOrDefaultAsync(a => a.Id == id)));
    }

    public static async Task<CreatedAtRoute<AuthorDto>> AddAuthor(ApplicationDbContext context, IMapper mapper, [FromBody] AuthorDtoCreate authorDtoCreate)
    {
        var author = mapper.Map<Author>(authorDtoCreate);
        await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();
        var authorDto = mapper.Map<AuthorDto>(author);
        
        return TypedResults.CreatedAtRoute(authorDto, "GetAuthorById", new { id = author.Id });
    }

    public static async Task<Results<NoContent, NotFound>> UpdateAuthor(int id, ApplicationDbContext context, IMapper mapper, [FromBody] AuthorDtoUpdate authorDtoUpdate)
    {
        var author = await context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author is null)
            return TypedResults.NotFound();
        
        mapper.Map(authorDtoUpdate, author);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound>> DeleteAuthor(ApplicationDbContext context, int id)
    {
        var author = await context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author is null)
            return TypedResults.NotFound();
        
        context.Authors.Remove(author);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}