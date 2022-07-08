using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsAppointment.IService
{
    public interface IPolyclinicService
    {
        Task<ActionResult<List<GetPolyclinicModel>>> GetPolyclinics();
        Task<ActionResult<GetPolyclinicModel>> GetPolyclinic(Guid id);
        Task<ActionResult<List<GetDoctorModel>>> GetPolyclinicDoctors(Guid id);
        Task<ActionResult> AddPolyclinic(AddPolyclinicDto request);
        Task<ActionResult> UpdatePolyclinic(UpdatePolyclinicDto request);
        Task<ActionResult> DeletePolyclinic(Guid id);
    }
}
