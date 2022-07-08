using Microsoft.AspNetCore.Mvc;

namespace DoctorsAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("GetCities")]
        public async Task<ActionResult<List<GetCityModel>>> GetCities()
        {
            return await _cityService.GetCities();
        }

        [HttpGet("{id} GetCity")]
        public async Task<ActionResult<GetCityModel>> GetCity(Guid id)
        {
            return await _cityService.GetCity(id);
        }

        [HttpGet("{id} GetCityPolyclinics")]
        public async Task<ActionResult<List<GetPolyclinicModel>>> GetCityPolyclinics(Guid id)
        {
            return await _cityService.GetCityPolyclinics(id);
        }

        [HttpGet("{id} GetCityDoctors")]
        public async Task<ActionResult<List<GetDoctorModel>>> GetCityDoctors(Guid id)
        {
            return await _cityService.GetCityDoctors(id);
        }
        [HttpPost("AddCity")]
        public async Task<ActionResult> AddCity(AddCityDto request)
        {
            return await _cityService.AddCity(request);
        }

        [HttpPut("UpdateCity")]
        public async Task<ActionResult> UpdateCity(UpdateCityDto request)
        {
            return await _cityService.UpdateCity(request);
        }

        [HttpDelete("{id} DeleteCity")]
        public async Task<ActionResult> DeleteCity(Guid id)
        {
            return await _cityService.DeleteCity(id);
        }
    }
}
