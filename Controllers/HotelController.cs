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
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public HotelController(ILogger<HotelController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError($"Invaild post attempt for {nameof(CreateHotel)}");
                return BadRequest(ModelState);
            }

            var hotel = mapper.Map<Hotel>(hotelDTO);
            await unitOfWork.Hotels.AddAsync(hotel);
            await unitOfWork.Save();
            return CreatedAtRoute("GetHotel", new { id = hotel.Id }, hotel);
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await unitOfWork.Hotels.GetAllAsync();
            var results = mapper.Map<IList<HotelDTO>>(hotels);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        public async Task<IActionResult> GetHotel(int id)
        {
            var hotel = await unitOfWork.Hotels.GetAsync(q => q.Id == id, new List<string> { "Co" });
            var result = mapper.Map<HotelDTO>(hotel);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }

            var hotel = await unitOfWork.Hotels.GetAsync(q => q.Id == id);
            if (hotel == null)
            {
                logger.LogError($"Invaild UPDATE attempt for {nameof(UpdateHotel)}");
                return BadRequest("Submitted data is invaild");
            }
            mapper.Map(hotelDTO, hotel); ;
            unitOfWork.Hotels.UpdateAsync(hotel);
            await unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            var hotel = await unitOfWork.Hotels.GetAsync(q => q.Id == id);
            if (hotel == null)
            {
                logger.LogError($"Invaild Delete attempt for {nameof(DeleteHotel)}");
                return BadRequest("Submitted data is invaild");
            }
            await unitOfWork.Hotels.DeleteAsync(id);
            await unitOfWork.Save();
            return NoContent();
        }
    }
}