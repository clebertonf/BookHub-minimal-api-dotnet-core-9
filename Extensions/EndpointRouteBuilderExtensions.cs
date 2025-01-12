using BookHub.EndpointHandlers;

namespace BookHub.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterAuthorEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var baseEndpoint = endpoints.MapGroup("author").WithTags("Author");
        baseEndpoint.MapGet("", AuthorHandlers.GetAllAuthors);
        baseEndpoint.MapGet("/books", AuthorHandlers.GetAllAuthorsWithBooks);
        baseEndpoint.MapGet("/{id:int}", AuthorHandlers.GetAuthorById).WithName("GetAuthorById");
        baseEndpoint.MapPost("", AuthorHandlers.AddAuthor);
        baseEndpoint.MapPut("", AuthorHandlers.UpdateAuthor);
        baseEndpoint.MapDelete("", AuthorHandlers.DeleteAuthor);
    }

    public static void RegisterBookEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var baseEndpoint = endpoints.MapGroup("book").WithTags("Book");
        baseEndpoint.MapGet("", BookHandlers.GetAllBooks);
        baseEndpoint.MapGet("/{id:int}", BookHandlers.GetBookById).WithName("GetBookById");;
        baseEndpoint.MapPost("", BookHandlers.AddBook);
        baseEndpoint.MapPut("", BookHandlers.UpdateBook);
        baseEndpoint.MapDelete("", BookHandlers.DeleteBook);
    }
}