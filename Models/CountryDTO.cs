using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CreateCountryDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortName { get; set; }
    }
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public virtual IList<HotelDTO> HotelDTOs { get; set; }
    }
}
