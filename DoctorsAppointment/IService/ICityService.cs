global using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsAppointment.IService
{
    public interface ICityService
    {
        Task<ActionResult<List<GetCityModel>>> GetCities();
        Task<ActionResult<GetCityModel>> GetCity(Guid id);
        Task<ActionResult<List<GetPolyclinicModel>>> GetCityPolyclinics(Guid id);
        Task<ActionResult<List<GetDoctorModel>>> GetCityDoctors(Guid id);
        Task<ActionResult> AddCity(AddCityDto request);
        Task<ActionResult> UpdateCity(UpdateCityDto request);
        Task<ActionResult> DeleteCity(Guid id);
    }
}
