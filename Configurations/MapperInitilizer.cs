using AutoMapper;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class MapperInitilizer:Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Country,CountryDTO>().ReverseMap();
            CreateMap<Country,CreateCountryDTO>().ReverseMap();
            CreateMap<Hotel,HotelDTO>().ReverseMap();
            CreateMap<Hotel,CreateHotelDTO>().ReverseMap();
        }
    }
}
