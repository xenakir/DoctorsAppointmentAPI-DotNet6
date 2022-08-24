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
        public ActionResult GetDoctors()
        //public IActionResult GetDoctors()
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

        [HttpGet("GetDoctor/{id:Guid}")]
        public ActionResult GetDoctor(Guid id)
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

        [HttpGet("GetDoctorPolyclinics/{id:Guid}")]
        public ActionResult GetDoctorPolyclinics(Guid id)
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

        [HttpGet("GetDoctorSpecializations/{id:Guid}")]
        public ActionResult GetDoctorSpecializations(Guid id)
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
        public ActionResult AddDoctor(UpdateDoctorDto request)
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
                FullName = request.FullName,
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

        [HttpPut("AddDoctorSpecialization")]
        public ActionResult AddDoctorSpecialization(DoctorSpecialization request)
        {
            var dbDoctor = _doctorService.GetDoctor(request.IdD);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            var dbSpecialization = _specializationService.GetSpecialization(request.IdS);
            if (dbSpecialization == null)
                return BadRequest("Специализация не найдена.");

            _doctorService.AddDoctorSpecialization(dbDoctor, dbSpecialization);
            return Ok();
        }

        [HttpPut("AddDoctorPolyclinic")]
        public ActionResult AddDoctorPolyclinic(DoctorPolyclinic request)
        {
            var dbDoctor = _doctorService.GetDoctor(request.IdD);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            var dbPolyclinic = _polyclinicService.GetPolyclinic(request.IdP);
            if (dbPolyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            _doctorService.AddDoctorPolyclinic(dbDoctor, dbPolyclinic);
            return Ok();
        }

        [HttpPut("UpdateDoctor/{id:Guid}")]
        public ActionResult UpdateDoctor(Guid id, UpdateDoctorDto request)
        {
            var dbDoctor = _doctorService.GetDoctor(id);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            _doctorService.UpdateDoctor(dbDoctor, request);
            return Ok();
        }

        [HttpDelete("DeleteDoctor/{id:Guid}")]
        public ActionResult DeleteDoctor(Guid id)
        {
            var dbDoctor = _doctorService.GetDoctor(id);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            _doctorService.DeleteDoctor(dbDoctor);
            return Ok();
        }

        [HttpPut("DeleteDoctorSpecialization")]
        public ActionResult DeleteDoctorSpecialization(DoctorSpecialization request)
        {
            var dbDoctor = _doctorService.GetDoctor(request.IdD);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            var dbSpecialization = _specializationService.GetSpecialization(request.IdS);
            if (dbSpecialization == null)
                return BadRequest("Специализация не найдена.");

            _doctorService.DeleteDoctorSpecialization(dbDoctor, dbSpecialization);
            return Ok();
        }

        [HttpPut("DeleteDoctorPolyclinic")]
        public ActionResult DeleteDoctorPolyclinic(DoctorPolyclinic request)
        {
            var dbDoctor = _doctorService.GetDoctor(request.IdD);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            var dbPolyclinic = _polyclinicService.GetPolyclinic(request.IdP);
            if (dbPolyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            _doctorService.DeleteDoctorPolyclinic(dbDoctor, dbPolyclinic);
            return Ok();
        }
    }
}
