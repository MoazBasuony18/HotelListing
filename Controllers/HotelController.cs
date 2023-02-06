using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                var hotels = await unitOfWork.Hotels.GetAllAsync();
                var results = mapper.Map<IList<HotelDTO>>(hotels);
                return Ok(results);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something Went Wrong in the {nameof(GetHotels)}");
                return StatusCode(500, "Internal Server Erorr Please Try Again Later. ");
            }

        }
        
        [HttpGet("{id:int}", Name="GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await unitOfWork.Hotels.GetAsync(q => q.Id == id, new List<string> { "Country" });
                var result = mapper.Map<HotelDTO>(hotel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something Went Wrong in the {nameof(GetHotel)}");
                return StatusCode(500, "Internal Server Erorr Please Try Again Later. ");
            }
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
            try
            {
                var hotel = mapper.Map<Hotel>(hotelDTO);
                await unitOfWork.Hotels.AddAsync(hotel);
                await unitOfWork.Save();
                return CreatedAtRoute("GetHotel",new {id=hotel.Id},hotel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something Went Wrong in the {nameof(CreateHotel)}");
                return StatusCode(500, "Internal Server Erorr Please Try Again Later. ");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>UpdateHotel(int id, [FromBody] UpdateHotelDTO hotelDTO)
        {
            if(!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var hotel = await unitOfWork.Hotels.GetAsync(q => q.Id == id);
                if(hotel == null)
                {
                    logger.LogError($"Invaild UPDATE attempt for {nameof(UpdateHotel)}");
                    return BadRequest("Submitted data is invaild");
                }
                mapper.Map(hotelDTO,hotel);
                unitOfWork.Hotels.UpdateAsync(hotel);
                await unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something Went Wrong in the {nameof(UpdateHotel)}");
                return StatusCode(500, "Internal Server Erorr Please Try Again Later. ");
            }
        }
    }

}
