using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Models.Book;

namespace BookStoreApp.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDto, Author>().ReverseMap();
            CreateMap<Author, AuthorReadDto>();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();

            CreateMap<BookCreateDto, Book>().ReverseMap();
            CreateMap<Book, BookReadDto>();
            CreateMap<BookUpdateDto, Book>().ReverseMap();
        }
    }
}
