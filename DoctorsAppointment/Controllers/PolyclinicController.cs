using Microsoft.AspNetCore.Mvc;

namespace DoctorsAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolyclinicController : ControllerBase
    {
        private readonly DataContext _context;
        public PolyclinicController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetPolyclinics")]
        public async Task<ActionResult<List<GetPolyclinicModel>>> GetPolyclinics()
        {
            var polyclinics = new List<GetPolyclinicModel>();
            var dbPolyclinics = await _context.Polyclinics.Include(p => p.City).ToListAsync();
            foreach (var item in dbPolyclinics)
            {
                polyclinics.Add(new GetPolyclinicModel
                {
                    Id = item.Id,
                    Address = item.Address,
                    Photo = item.Photo,
                    Location = item.Location,
                    CityId = item.City.Id,
                    CityName = item.City.Name
                });
            }
            return Ok(polyclinics);
        }

        [HttpGet("{id} GetPolyclinic")]
        public async Task<ActionResult<GetPolyclinicModel>> GetPolyclinic(Guid id)
        {
            var dbPolyclinics = await _context.Polyclinics.Include(p => p.City).ToListAsync();
            var polyclinic = new GetPolyclinicModel();
            foreach (var item in dbPolyclinics)
            {
                if (item.Id == id)
                    polyclinic = new GetPolyclinicModel
                    {
                        Id = item.Id,
                        Address = item.Address,
                        Photo = item.Photo,
                        Location = item.Location,
                        CityId = item.City.Id,
                        CityName = item.City.Name
                    };
            }
            if (polyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            return Ok(polyclinic);
        }

        [HttpGet("{id} GetPolyclinicDoctors")]
        public async Task<ActionResult<List<GetDoctorModel>>> GetPolyclinicDoctors(Guid id)
        {
            var polyclinic = await _context.Polyclinics
                .Where(p => p.Id == id)
                .Include(d => d.Doctors)
                .FirstOrDefaultAsync();
            if (polyclinic == null)
                return BadRequest("Доктор не найден.");

            var doctors = new List<GetDoctorModel>();
            foreach (var item in polyclinic.Doctors)
            {
                doctors.Add(new GetDoctorModel
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    Photo = item.Photo
                });
            }
            return Ok(doctors);
        }

        [HttpPost("AddPolyclinic")]
        public async Task<ActionResult> AddPolyclinic(AddPolyclinicDto request)
        {
            var city = await _context.Cities.FindAsync(request.CityId);
            if (city == null)
                return NotFound();

            var newPolyclinic = new Polyclinic
            {
                Id = Guid.NewGuid(),
                Address = request.Address,
                Photo = request.Photo,
                Location = request.Location,
                City = city
            };

            _context.Polyclinics.Add(newPolyclinic);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("UpdatePolyclinic")]
        public async Task<ActionResult> UpdatePolyclinic(UpdatePolyclinicDto request)
        {
            var dbPolyclinic = await _context.Polyclinics.FindAsync(request.Id);
            if (dbPolyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            dbPolyclinic.Address = request.Address;
            dbPolyclinic.Photo = request.Photo;
            dbPolyclinic.Location = request.Location;

            await _context.SaveChangesAsync();
            //return Ok();
            //return Ok(await _context.Polyclinics.ToListAsync());
            return Ok();
        }

        [HttpDelete("{id} DeletePolyclinic")]
        public async Task<ActionResult> DeletePolyclinic(Guid id)
        {
            var dbPolyclinic = await _context.Polyclinics.FindAsync(id);
            if (dbPolyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            _context.Polyclinics.Remove(dbPolyclinic);
            await _context.SaveChangesAsync();

            //return Ok(await _context.Polyclinics.ToListAsync());
            return Ok();
        }
    }
}
