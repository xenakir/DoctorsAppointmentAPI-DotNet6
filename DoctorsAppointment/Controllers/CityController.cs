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
        public ActionResult<List<GetCityModel>> GetCities()
        {
            var cities = new List<GetCityModel>();
            var dbCities = _cityService.GetCities();
            foreach (var item in dbCities)
            {
                cities.Add(new GetCityModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Type = item.Type
                });
            }
            return Ok(cities);
        }

        [HttpGet("{id} GetCity")]
        public ActionResult<GetCityModel> GetCity(Guid id)
        {
            var dbCity = _cityService.GetCity(id);
            if (dbCity == null)
                return BadRequest("Город не найден.");

            var city = new GetCityModel
            {
                Id = dbCity.Id,
                Name = dbCity.Name,
                Type = dbCity.Type
            };
            return Ok(city);
        }

        [HttpGet("{id} GetCityPolyclinics")]
        public ActionResult<List<GetPolyclinicModel>> GetCityPolyclinics(Guid id)
        {
            var dbCity = _cityService.GetCity(id);
            if (dbCity == null)
                return BadRequest("Город не найден.");

            var polyclinics = new List<GetPolyclinicModel>();
            foreach (var polyclinic in dbCity.Polyclinics)
                polyclinics.Add(new GetPolyclinicModel
                {
                    Id = polyclinic.Id,
                    Address = polyclinic.Address,
                    Photo = polyclinic.Photo,
                    Location = polyclinic.Location,
                    CityId = dbCity.Id,
                    CityName = dbCity.Name
                });

            return Ok(polyclinics);
        }

        [HttpGet("{id} GetCityDoctors")]
        public ActionResult<List<GetDoctorModel>> GetCityDoctors(Guid id)
        {
            var dbCity = _cityService.GetCity(id);
            if (dbCity == null)
                return BadRequest("Город не найден.");

            var doctors = new List<GetDoctorModel>();
            foreach (var polyclinic in dbCity.Polyclinics)
                foreach (var doctor in polyclinic.Doctors)
                    if (doctors.Find(_ => _.Id == doctor.Id) == null)
                        doctors.Add(new GetDoctorModel
                        {
                            Id = doctor.Id,
                            FullName = doctor.FullName,
                            Photo = doctor.Photo
                        });


            return Ok(doctors);
        }
        [HttpPost("AddCity")]
        public ActionResult AddCity(AddCityDto request)
        {
            var newCity = new City
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Type = request.Type
            };

            _cityService.AddCity(newCity);
            return Ok();
        }

        [HttpPut("UpdateCity")]
        public ActionResult UpdateCity(UpdateCityDto request)
        {
            var dbCity = _cityService.GetCity(request.Id);
            if (dbCity == null)
                return BadRequest("Город не найден.");

            _cityService.UpdateCity(dbCity, request);
            return Ok();
        }

        [HttpDelete("{id} DeleteCity")]
        public ActionResult DeleteCity(Guid id)
        {
            var dbCity = _cityService.GetCity(id);
            if (dbCity == null)
                return BadRequest("Город не найден.");

            _cityService.DeleteCity(dbCity);
            return Ok();
        }
    }
}
