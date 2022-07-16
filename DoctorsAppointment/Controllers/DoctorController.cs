namespace DoctorsAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private IDoctorService _doctorService;
        private ISpecializationService _specializationService;
        private IPolyclinicService _polyclinicService;
        private static IWebHostEnvironment _webHostEnvironment;
        public DoctorController(IDoctorService doctorService, 
            ISpecializationService specializationService,
            IPolyclinicService polyclinicService,
            IWebHostEnvironment webHostEnvironment)
        {
            _doctorService = doctorService;
            _specializationService = specializationService;
            _polyclinicService = polyclinicService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetDoctors")]
        public ActionResult<List<GetDoctorModel>> GetDoctors()
        {
            var doctors = new List<GetDoctorModel>();
            var dbDoctors = _doctorService.GetDoctors();
            foreach (var item in dbDoctors)
            {
                doctors.Add(new GetDoctorModel
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    Photo = item.Photo
                });
            }
            return Ok(doctors);
        }

        [HttpGet("{id} GetDoctor")]
        public ActionResult<GetDoctorModel> GetDoctor(Guid id)
        {
            var dbDoctor = _doctorService.GetDoctor(id);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            var doctor = new GetDoctorModel
            {
                Id = dbDoctor.Id,
                FullName = dbDoctor.FullName,
                Photo = dbDoctor.Photo
            };

            return Ok(doctor);
        }

        [HttpGet("{id} GetDoctorPolyclinics")]
        public ActionResult<List<GetPolyclinicModel>> GetDoctorPolyclinics(Guid id)
        {
            var dbDoctor = _doctorService.GetDoctor(id);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            var polyclinics = new List<GetPolyclinicModel>();
            foreach (var item in dbDoctor.Polyclinics)
            {
                polyclinics.Add(new GetPolyclinicModel
                {
                    Id = item.Id,
                    Address = item.Address,
                    Photo = item.Photo,
                    Location = item.Location,
                    CityId = item.City.Id,
                    CityName = item.City.Name
                });
            }
            return Ok(polyclinics);
        }

        [HttpGet("{id} GetDoctorSpecializations")]
        public ActionResult<List<GetSpecializationModel>> GetDoctorSpecializations(Guid id)
        {
            var dbDoctor = _doctorService.GetDoctor(id);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            var specializations = new List<GetSpecializationModel>();
            foreach (var item in dbDoctor.Specializations)
            {
                specializations.Add(new GetSpecializationModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return Ok(specializations);
        }

        [HttpPost("AddDoctor")]
        public ActionResult AddDoctor(string fullName)
        {
            //string path1, path2;
            //if (objectFile.file.Length > 0)
            //{
            //    path1 = _webHostEnvironment.WebRootPath;
            //    path2 = "/Content/" + Guid.NewGuid().ToString() + "_" + objectFile.file.FileName;

            //    using (FileStream fileStream = System.IO.File.Create(path1 + path2))
            //    {
            //        objectFile.file.CopyTo(fileStream);
            //        fileStream.Flush();
            //    }
            //}
            //else
            //    return BadRequest("Ошибка загрузки фотографии.");

            var newDoctor = new Doctor
            {
                Id = Guid.NewGuid(),
                FullName = fullName,
                Photo = ""
            };

            _doctorService.AddDoctor(newDoctor);
            return Ok();
        }

        [HttpPost("AddDoctorsPhoto")]
        public ActionResult AddDoctorsPhoto([FromForm] FileUpload objectFile, Guid id)
        {
            var dbDoctor = _doctorService.GetDoctor(id);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");


            string path1, path2;
            if (objectFile.file.Length > 0)
            {
                path1 = _webHostEnvironment.WebRootPath;
                path2 = "/Content/" + Guid.NewGuid().ToString() + "_" + objectFile.file.FileName;

                using (FileStream fileStream = System.IO.File.Create(path1 + path2))
                {
                    objectFile.file.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            else
                return BadRequest("Ошибка загрузки фотографии.");

            _doctorService.AddDoctorsPhoto(dbDoctor, path2);
            return Ok();
        }


        [HttpPost("AddDoctorSpecialization")]
        public ActionResult AddDoctorSpecialization(
            Guid DoctorId, Guid SpecializationId)
        {
            var dbDoctor = _doctorService.GetDoctor(DoctorId);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            var dbSpecialization = _specializationService.GetSpecialization(SpecializationId);
            if (dbSpecialization == null)
                return BadRequest("Специализация не найдена.");

            _doctorService.AddDoctorSpecialization(dbDoctor, dbSpecialization);
            return Ok();
        }

        [HttpPost("AddDoctorPolyclinic")]
        public async Task<ActionResult> AddDoctorPolyclinic(
            Guid DoctorId, Guid PolyclinicId)
        {
            var dbDoctor = _doctorService.GetDoctor(DoctorId);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            var dbPolyclinic = _polyclinicService.GetPolyclinic(PolyclinicId);
            if (dbPolyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            _doctorService.AddDoctorPolyclinic(dbDoctor, dbPolyclinic);
            return Ok();
        }

        [HttpPut("UpdateDoctor")]
        public ActionResult UpdateDoctor(UpdateDoctorDto request)
        {
            var dbDoctor = _doctorService.GetDoctor(request.Id);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            _doctorService.UpdateDoctor(dbDoctor, request);
            return Ok();
        }

        [HttpDelete("{id} DeleteDoctor")]
        public ActionResult DeleteDoctor(Guid id)
        {
            var dbDoctor = _doctorService.GetDoctor(id);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            _doctorService.DeleteDoctor(dbDoctor);
            return Ok();
        }

        [HttpDelete("DeleteDoctorSpecialization")]
        public ActionResult DeleteDoctorSpecialization(
            Guid DoctorId, Guid SpecializationId)
        {
            var dbDoctor = _doctorService.GetDoctor(DoctorId);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            var dbSpecialization = _specializationService.GetSpecialization(SpecializationId);
            if (dbSpecialization == null)
                return BadRequest("Специализация не найдена.");

            _doctorService.DeleteDoctorSpecialization(dbDoctor, dbSpecialization);
            return Ok();
        }

        [HttpDelete("DeleteDoctorPolyclinic")]
        public ActionResult DeleteDoctorPolyclinic(
            Guid DoctorId, Guid PolyclinicId)
        {
            var dbDoctor = _doctorService.GetDoctor(DoctorId);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            var dbPolyclinic = _polyclinicService.GetPolyclinic(PolyclinicId);
            if (dbPolyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            _doctorService.DeleteDoctorPolyclinic(dbDoctor, dbPolyclinic);
            return Ok();
        }
    }
}
