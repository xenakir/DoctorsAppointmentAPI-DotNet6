namespace DoctorsAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private ISpecializationService _specializationService;
        private IPolyclinicService _polyclinicService;
        private ICityService _cityService;
        public SpecializationController(ISpecializationService specializationService,
            IPolyclinicService polyclinicService,
            ICityService cityService)
        {
            _specializationService = specializationService;
            _polyclinicService = polyclinicService;
            _cityService = cityService;
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

        [HttpGet("{id} GetSpecializationDoctors")]
        public ActionResult<List<GetDoctorModel>> GetSpecializationDoctors(Guid id)
        {
            var dbSpecialization = _specializationService.GetSpecialization(id);
            if (dbSpecialization == null)
                return BadRequest("Специализация не найдена.");

            var doctors = new List<GetDoctorModel>();
            foreach (var doctor in dbSpecialization.Doctors)
                doctors.Add(new GetDoctorModel
                {
                    Id = doctor.Id,
                    FullName = doctor.FullName,
                    Photo = doctor.Photo
                });

            return Ok(doctors);
        }

        [HttpGet("GetSpecializationPolyclinicDoctors")]
        public ActionResult<List<GetPolyclinicModel>> GetSpecializationPolyclinicDoctors(
            Guid SpecializationId, Guid PolyclinicId)
        {
            var dbSpecialization = _specializationService.GetSpecialization(SpecializationId);
            if (dbSpecialization == null)
                return BadRequest("Специализация не найдена.");

            var dbPolyclinic = _polyclinicService.GetPolyclinic(PolyclinicId);
            if (dbPolyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            var doctors = new List<GetDoctorModel>();
            foreach (var doctor in dbPolyclinic.Doctors)
                if (dbSpecialization.Doctors.Find(_ => _.Id == doctor.Id) != null
                    && doctors.Find(_ => _.Id == doctor.Id) == null)
                    doctors.Add(new GetDoctorModel
                    {
                        Id = doctor.Id,
                        FullName = doctor.FullName,
                        Photo = doctor.Photo
                    });

            return Ok(doctors);
        }

        [HttpGet("GetSpecializationCityDoctors")]
        public ActionResult<List<GetPolyclinicModel>> GetSpecializationCityDoctors(
            Guid SpecializationId, Guid CityId)
        {
            var dbSpecialization = _specializationService.GetSpecialization(SpecializationId);
            if (dbSpecialization == null)
                return BadRequest("Специализация не найдена.");

            var dbCity = _cityService.GetCity(CityId);
            if (dbCity == null)
                return BadRequest("Город не найден.");

            var dbPolyclinics = _polyclinicService.GetPolyclinics()
                .Where(_ => _.City.Id == CityId);

            var doctors = new List<GetDoctorModel>();
            foreach(var polyclinic in dbPolyclinics)
            foreach (var doctor in polyclinic.Doctors)
                if (dbSpecialization.Doctors.Find(_ => _.Id == doctor.Id) != null
                        && doctors.Find(_ => _.Id == doctor.Id) == null)
                    doctors.Add(new GetDoctorModel
                    {
                        Id = doctor.Id,
                        FullName = doctor.FullName,
                        Photo = doctor.Photo
                    });

            return Ok(doctors);
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
