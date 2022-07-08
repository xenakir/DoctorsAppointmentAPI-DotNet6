using Microsoft.AspNetCore.Mvc;

namespace DoctorsAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("GetDoctors")]
        public async Task<ActionResult<List<GetDoctorModel>>> GetDoctors()
        {
            return await _doctorService.GetDoctors();
        }

        [HttpGet("{id} GetDoctor")]
        public async Task<ActionResult<GetDoctorModel>> GetDoctor(Guid id)
        {
            return await _doctorService.GetDoctor(id);
        }

        [HttpGet("{id} GetDoctorPolyclinics")]
        public async Task<ActionResult<List<GetPolyclinicModel>>> GetDoctorPolyclinics(Guid id)
        {
            return await _doctorService.GetDoctorPolyclinics(id);
        }

        [HttpGet("{id} GetDoctorSpecializations")]
        public async Task<ActionResult<List<GetSpecializationModel>>> GetDoctorSpecializations(Guid id)
        {
            return await _doctorService.GetDoctorSpecializations(id);
        }

        [HttpPost("AddDoctor")]
        public async Task<ActionResult> AddDoctor(AddDoctorDto request)
        {
            return await _doctorService.AddDoctor(request);
        }

        [HttpPost("AddDoctorSpecialization")]
        public async Task<ActionResult> AddDoctorSpecialization(AddDoctorSpecializationDto request)
        {
            return await _doctorService.AddDoctorSpecialization(request);
        }

        [HttpPost("AddDoctorPolyclinic")]
        public async Task<ActionResult> AddDoctorPolyclinic(AddDoctorPolyclinicDto request)
        {
            return await _doctorService.AddDoctorPolyclinic(request);
        }

        [HttpPut("UpdateDoctor")]
        public async Task<ActionResult> UpdateDoctor(UpdateDoctorDto request)
        {
            return await _doctorService.UpdateDoctor(request);
        }

        [HttpDelete("{id} DeleteDoctor")]
        public async Task<ActionResult> DeleteDoctor(Guid id)
        {
            return await _doctorService.DeleteDoctor(id);
        }

        [HttpDelete("DeleteDoctorSpecialization")]
        public async Task<ActionResult> DeleteDoctorSpecialization(AddDoctorSpecializationDto request)
        {
            return await _doctorService.DeleteDoctorSpecialization(request);
        }

        [HttpDelete("DeleteDoctorPolyclinic")]
        public async Task<ActionResult> DeleteDoctorPolyclinic(AddDoctorPolyclinicDto request)
        {
            return await _doctorService.DeleteDoctorPolyclinic(request);
        }
    }
}
