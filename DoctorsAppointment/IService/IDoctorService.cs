using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsAppointment.IService
{
    public interface IDoctorService
    {
        Task<ActionResult<List<GetDoctorModel>>> GetDoctors();
        Task<ActionResult<GetDoctorModel>> GetDoctor(Guid id);
        Task<ActionResult<List<GetPolyclinicModel>>> GetDoctorPolyclinics(Guid id);
        Task<ActionResult<List<GetSpecializationModel>>> GetDoctorSpecializations(Guid id);
        Task<ActionResult> AddDoctor(AddDoctorDto request);
        Task<ActionResult> AddDoctorSpecialization(AddDoctorSpecializationDto request);
        Task<ActionResult> AddDoctorPolyclinic(AddDoctorPolyclinicDto request);
        Task<ActionResult> UpdateDoctor(UpdateDoctorDto request);
        Task<ActionResult> DeleteDoctor(Guid id);
        Task<ActionResult> DeleteDoctorSpecialization(AddDoctorSpecializationDto request);
        Task<ActionResult> DeleteDoctorPolyclinic(AddDoctorPolyclinicDto request);
    }
}
