using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.BL.IRepository;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CountryController(ILogger<CountryController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError($"Invaild post attempt for {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }
            
                var country = mapper.Map<Country>(countryDTO);
                await unitOfWork.Countries.AddAsync(country);
                await unitOfWork.Save();
                return CreatedAtRoute("GetHotel", new { id = country.Id }, country);
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {    
                var countries = await unitOfWork.Countries.GetAllAsync();
                var results = mapper.Map<IList<CountryDTO>>(countries);
                return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetCountry")]
        public async Task<IActionResult> GetCountry(int id)
        {
                var country = await unitOfWork.Countries.GetAsync(q => q.Id == id, new List<string> { "Hotels" });
                var result = mapper.Map<CountryDTO>(country);
                return Ok(result);              
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }
            
                var country = await unitOfWork.Countries.GetAsync(q => q.Id == id);
                if (country == null)
                {
                    logger.LogError($"Invaild UPDATE attempt for {nameof(UpdateCountry)}");
                    return BadRequest("Submitted data is invaild");
                }
                mapper.Map(countryDTO, country);
                unitOfWork.Countries.UpdateAsync(country);
                await unitOfWork.Save();
                return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                return BadRequest(ModelState);
            }
            
                var hotel = await unitOfWork.Countries.GetAsync(q => q.Id == id);
                if (hotel == null)
                {
                    logger.LogError($"Invaild Delete attempt for {nameof(DeleteCountry)}");
                    return BadRequest("Submitted data is invaild");
                }
                await unitOfWork.Countries.DeleteAsync(id);
                await unitOfWork.Save();
                return NoContent();               
        }
    }
}