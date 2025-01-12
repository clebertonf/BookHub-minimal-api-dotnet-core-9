using AutoMapper;
using BookHub.DTOS;
using BookHub.Models;

namespace BookHub.Profiles;

public class BookHubProfile : Profile
{
    public BookHubProfile()
    {
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Author, AuthorDtoCreate>().ReverseMap();
        CreateMap<Author, AuthorDtoUpdate>().ReverseMap();
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Book, BookDtoCreate>().ReverseMap();
        CreateMap<Book, BookDtoUpdate>().ReverseMap();
    }
}