using Microsoft.AspNetCore.Mvc;

namespace DoctorsAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly DataContext _context;

        public SpecializationController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetSpecializations")]
        public async Task<ActionResult<List<GetSpecializationModel>>> GetSpecializations()
        {
            var specializations = new List<GetSpecializationModel>();
            var dbSpecializations = await _context.Specializations.ToListAsync();
            foreach (var item in dbSpecializations)
            {
                specializations.Add(new GetSpecializationModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return Ok(specializations);
        }

        [HttpGet("{id} GetSpecialization")]
        public async Task<ActionResult<Specialization>> GetSpecialization(Guid id)
        {
            var dbSpecializations = await _context.Specializations.ToListAsync();
            var specialization = new GetSpecializationModel();
            foreach (var item in dbSpecializations)
            {
                if (item.Id == id)
                    specialization = new GetSpecializationModel
                    {
                        Id = item.Id,
                        Name = item.Name
                    };
            }
            if (specialization == null)
                return BadRequest("Специализация не найдена.");

            return Ok(specialization);
        }

        [HttpPost("{name}")]
        public async Task<ActionResult<List<Specialization>>> AddSpecialization(string name)
        {
            var newSpecialization = new Specialization
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            await _context.Specializations.AddAsync(newSpecialization);
            await _context.SaveChangesAsync();

            return Ok(await _context.Specializations.ToListAsync());
            //return Ok();
        }

        [HttpPut("UpdateSpecialization")]
        public async Task<ActionResult<List<Specialization>>> UpdateSpecialization(UpdateSpecializationDto request)
        {
            var dbSpecialization = await _context.Specializations.FindAsync(request.Id);
            if (dbSpecialization == null)
                return BadRequest("Специализация не найдена.");

            dbSpecialization.Name = request.Name;

            await _context.SaveChangesAsync();

            return Ok(await _context.Specializations.ToListAsync());
            //return Ok();
        }

        [HttpDelete("{id} DeleteSpecialization")]
        public async Task<ActionResult<List<Specialization>>> DeleteSpecialization(Guid id)
        {
            var dbSpecialization = await _context.Specializations.FindAsync(id);
            if (dbSpecialization == null)
                return BadRequest("Специализация не найдена.");

            _context.Specializations.Remove(dbSpecialization);
            await _context.SaveChangesAsync();

            return Ok(await _context.Specializations.ToListAsync());
            //return Ok();
        }
    }
}
