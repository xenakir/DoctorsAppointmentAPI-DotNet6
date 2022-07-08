using Microsoft.AspNetCore.Mvc;

namespace DoctorsAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolyclinicController : ControllerBase
    {
        private IPolyclinicService _polyclinicService;
        public PolyclinicController(IPolyclinicService polyclinicService)
        {
            _polyclinicService = polyclinicService;
        }

        [HttpGet("GetPolyclinics")]
        public async Task<ActionResult<List<GetPolyclinicModel>>> GetPolyclinics()
        {
            return await _polyclinicService.GetPolyclinics();
        }

        [HttpGet("{id} GetPolyclinic")]
        public async Task<ActionResult<GetPolyclinicModel>> GetPolyclinic(Guid id)
        {
            return await _polyclinicService.GetPolyclinic(id);
        }

        [HttpGet("{id} GetPolyclinicDoctors")]
        public async Task<ActionResult<List<GetDoctorModel>>> GetPolyclinicDoctors(Guid id)
        {
            return await _polyclinicService.GetPolyclinicDoctors(id);
        }

        [HttpPost("AddPolyclinic")]
        public async Task<ActionResult> AddPolyclinic(AddPolyclinicDto request)
        {
            return await _polyclinicService.AddPolyclinic(request);
        }

        [HttpPut("UpdatePolyclinic")]
        public async Task<ActionResult> UpdatePolyclinic(UpdatePolyclinicDto request)
        {
            return await _polyclinicService.UpdatePolyclinic(request);
        }

        [HttpDelete("{id} DeletePolyclinic")]
        public async Task<ActionResult> DeletePolyclinic(Guid id)
        {
            return await _polyclinicService.DeletePolyclinic(id);
        }
    }
}
