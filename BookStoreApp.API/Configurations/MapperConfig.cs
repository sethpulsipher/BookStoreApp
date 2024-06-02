using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Models.Book;

namespace BookStoreApp.API.Configurations
{
    /*
     * This class is to 'map' the DTO and Model
     * Converts raw data from database to a usable object of the corresponding model
     * 
     * MUST CREATE A MAP FOR EACH DTO
     */
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Author
            CreateMap<AuthorCreateDto, Author>().ReverseMap(); // ReverseMap() method allows for 2 way mapping
            CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();
            
            // Book
            CreateMap<Book, BookReadOnlyDto>().ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}")).ReverseMap();
            CreateMap<Book, BookDetailsDto>().ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}")).ReverseMap();

            CreateMap<BookCreateDto, Book>().ReverseMap();
            CreateMap<BookUpdateDto, Book>().ReverseMap();
        }
    }
}
