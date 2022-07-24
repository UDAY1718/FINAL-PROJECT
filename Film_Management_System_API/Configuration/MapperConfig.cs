using AutoMapper;
using Film_Management_System_API.DataModels;
using Film_Management_System_API.DataModels.Film;
using Film_Management_System_API.Models;

namespace Film_Management_System_API.Configuration
{
    public class MapperConfig: Profile
    {

        public MapperConfig()
        {
            CreateMap<FilmDTO, Film>().ReverseMap();
            CreateMap<ActorDTO, Actor>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<LanguageDTO, Language>().ReverseMap();    
            CreateMap<AdminDTO, Admin>().ReverseMap();
        }
    }
}
