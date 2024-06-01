using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;

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
            CreateMap<AuthorCreateDto, Author>().ReverseMap(); // ReverseMap() method allows for 2 way mapping
            CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();
        }
    }
}
