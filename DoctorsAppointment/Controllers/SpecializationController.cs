namespace DoctorsAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private ISpecializationService _specializationService;
        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpGet("GetSpecializations")]
        public ActionResult<List<GetSpecializationModel>> GetSpecializations()
        {
            var specializations = new List<GetSpecializationModel>();
            var dbSpecializations = _specializationService.GetSpecializations();
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
        public ActionResult<Specialization> GetSpecialization(Guid id)
        {
            var dbSpecialization = _specializationService.GetSpecialization(id);
            if (dbSpecialization == null)
                return BadRequest("Специализация не найдена.");

            var specialization = new GetSpecializationModel
            {
                Id = dbSpecialization.Id,
                Name = dbSpecialization.Name
            };

            return Ok(specialization);
        }

        [HttpPost("{name}")]
        public ActionResult AddSpecialization(string name)
        {
            var newSpecialization = new Specialization
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            _specializationService.AddSpecialization(newSpecialization);
            return Ok();
        }

        [HttpPut("UpdateSpecialization")]
        public ActionResult UpdateSpecialization(UpdateSpecializationDto request)
        {
            var dbSpecialization = _specializationService.GetSpecialization(request.Id);
            if (dbSpecialization == null)
                return BadRequest("Специализация не найдена.");

            _specializationService.UpdateSpecialization(dbSpecialization, request);
            return Ok();
        }

        [HttpDelete("{id} DeleteSpecialization")]
        public ActionResult DeleteSpecialization(Guid id)
        {
            var dbSpecialization = _specializationService.GetSpecialization(id);
            if (dbSpecialization == null)
                return BadRequest("Специализация не найдена.");

            _specializationService.DeleteSpecialization(dbSpecialization);
            return Ok();
        }
    }
}
