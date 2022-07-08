using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<GetSpecializationModel>>> GetSpecializations()
        {
            return await _specializationService.GetSpecializations();
        }

        [HttpGet("{id} GetSpecialization")]
        public async Task<ActionResult<Specialization>> GetSpecialization(Guid id)
        {
            return await _specializationService.GetSpecialization(id);
        }

        [HttpPost("{name}")]
        public async Task<ActionResult> AddSpecialization(string name)
        {
            return await _specializationService.AddSpecialization(name);
        }

        [HttpPut("UpdateSpecialization")]
        public async Task<ActionResult> UpdateSpecialization(UpdateSpecializationDto request)
        {
            return await _specializationService.UpdateSpecialization(request);
        }

        [HttpDelete("{id} DeleteSpecialization")]
        public async Task<ActionResult> DeleteSpecialization(Guid id)
        {
            return await _specializationService.DeleteSpecialization(id);
        }
    }
}
