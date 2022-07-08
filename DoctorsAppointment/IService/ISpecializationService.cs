using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsAppointment.IService
{
    public interface ISpecializationService
    {
        Task<ActionResult<List<GetSpecializationModel>>> GetSpecializations();
        Task<ActionResult<Specialization>> GetSpecialization(Guid id);
        Task<ActionResult> AddSpecialization(string name);
        Task<ActionResult> UpdateSpecialization(UpdateSpecializationDto request);
        Task<ActionResult> DeleteSpecialization(Guid id);
    }
}
