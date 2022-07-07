using Microsoft.AspNetCore.Mvc;

namespace DoctorsAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DataContext _context;

        public CityController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetCities")]
        public async Task<ActionResult<List<GetCityModel>>> GetCities()
        {
            var cities = new List<GetCityModel>();
            var dbCities = await _context.Cities.ToListAsync();
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
        public async Task<ActionResult<GetCityModel>> GetCity(Guid id)
        {
            var dbCities = await _context.Cities.ToListAsync();
            var city = new GetCityModel();
            foreach (var item in dbCities)
            {
                if (item.Id == id)
                    city = new GetCityModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Type =item.Type
                    };
            }
            if (city == null)
                return BadRequest("Город не найден.");

            return Ok(city);
        }

        [HttpGet("{id} GetCityPolyclinics")]
        public async Task<ActionResult<List<GetPolyclinicModel>>> GetCityPolyclinics(Guid id)
        {
            var city = await _context.Cities
                .Where(p => p.Id == id)
                .Include(p => p.Polyclinics)
                .FirstOrDefaultAsync();
            if (city == null)
                return BadRequest("Город не найден.");

            var polyclinics = new List<GetPolyclinicModel>();
            foreach (var polyclinic in city.Polyclinics)
                polyclinics.Add(new GetPolyclinicModel
                {
                    Id = polyclinic.Id,
                    Address = polyclinic.Address,
                    Photo = polyclinic.Photo,
                    Location = polyclinic.Location,
                    CityId = city.Id,
                    CityName = city.Name
                });
            return Ok(polyclinics);
        }

        [HttpGet("{id} GetCityDoctors")]
        public async Task<ActionResult<List<GetDoctorModel>>> GetCityDoctors(Guid id)
        {
            var city = await _context.Cities
                .Where(p => p.Id == id)
                .Include(p => p.Polyclinics)
                .ThenInclude(d => d.Doctors)
                .FirstOrDefaultAsync();
            if (city == null)
                return BadRequest("Город не найден.");

            var doctors = new List<GetDoctorModel>();
            foreach (var polyclinic in city.Polyclinics)
                foreach(var doctor in polyclinic.Doctors)
                    doctors.Add(new GetDoctorModel
                    {
                        Id = doctor.Id,
                        FullName = doctor.FullName,
                        Photo = doctor.Photo
                    });
            return Ok(doctors);
        }
        [HttpPost("AddCity")]
        public async Task<ActionResult<List<City>>> AddCity(AddCityDto request)
        {
            var city = new City 
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Type = request.Type
            };

            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();

            return Ok(await _context.Cities.ToListAsync());
            //return Ok();
        }

        [HttpPut("UpdateCity")]
        public async Task<ActionResult<List<City>>> UpdateCity(UpdateCityDto request)
        {
            var dbCity = await _context.Cities.FindAsync(request.Id);
            if (dbCity == null)
                return BadRequest("Город не найден.");

            dbCity.Name = request.Name;
            dbCity.Type = request.Type;

            await _context.SaveChangesAsync();

            return Ok(await _context.Cities.ToListAsync());
            //return Ok();
        }

        [HttpDelete("{id} DeleteCity")]
        public async Task<ActionResult<List<City>>> DeleteCity(Guid id)
        {
            var dbCity = await _context.Cities.FindAsync(id);
            if (dbCity == null)
                return BadRequest("Город не найден.");

            _context.Cities.Remove(dbCity);
            await _context.SaveChangesAsync();

            return Ok(await _context.Cities.ToListAsync());
            //return Ok();
        }
    }
}
