namespace DoctorsAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolyclinicController : ControllerBase
    {
        private IPolyclinicService _polyclinicService;
        private ICityService _cityService;
        private static IWebHostEnvironment _webHostEnvironment;
        public PolyclinicController(IPolyclinicService polyclinicService, 
            ICityService cityService, IWebHostEnvironment webHostEnvironment)
        {
            _polyclinicService = polyclinicService;
            _cityService = cityService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetPolyclinics")]
        public ActionResult<List<GetPolyclinicModel>> GetPolyclinics()
        {
            var polyclinics = new List<GetPolyclinicModel>();
            var dbPolyclinics = _polyclinicService.GetPolyclinics();
            foreach (var item in dbPolyclinics)
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

        [HttpGet("{id} GetPolyclinic")]
        public ActionResult<GetPolyclinicModel> GetPolyclinic(Guid id)
        {
            var dbPolyclinic = _polyclinicService.GetPolyclinic(id);
            if (dbPolyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            var polyclinic = new GetPolyclinicModel
            {
                Id = dbPolyclinic.Id,
                Address = dbPolyclinic.Address,
                Photo = dbPolyclinic.Photo,
                Location = dbPolyclinic.Location,
                CityId = dbPolyclinic.City.Id,
                CityName = dbPolyclinic.City.Name
            };

            return Ok(polyclinic);
        }

        [HttpGet("{id} GetPolyclinicDoctors")]
        public ActionResult<List<GetDoctorModel>> GetPolyclinicDoctors(Guid id)
        {
            var polyclinic = _polyclinicService.GetPolyclinic(id);
            if (polyclinic == null)
                return BadRequest("Доктор не найден.");

            var doctors = new List<GetDoctorModel>();
            foreach (var item in polyclinic.Doctors)
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

        [HttpPost("AddPolyclinic")]
        public ActionResult AddPolyclinic(AddPolyclinicDto request)
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

            var dbCity = _cityService.GetCity(request.CityId);
            if (dbCity == null)
                return BadRequest("Город не найден.");

            var newPolyclinic = new Polyclinic
            {
                Id = Guid.NewGuid(),
                Address = request.Address,
                Photo = "",
                Location = request.Location,
                City = dbCity
            };

            _polyclinicService.AddPolyclinic(newPolyclinic);
            return Ok();
        }

        [HttpPost("AddPolyclinicsPhoto")]
        public ActionResult AddPolyclinicsPhoto([FromForm] FileUpload objectFile, Guid id)
        {
            var dbPolyclinic = _polyclinicService.GetPolyclinic(id);
            if (dbPolyclinic == null)
                return BadRequest("Поликлиника не найдена.");

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

            _polyclinicService.AddPolyclinicsPhoto(dbPolyclinic, path2);
            return Ok();
        }

        [HttpPut("UpdatePolyclinic")]
        public ActionResult UpdatePolyclinic(UpdatePolyclinicDto request)
        {
            var dbPolyclinic = _polyclinicService.GetPolyclinic(request.Id);
            if (dbPolyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            _polyclinicService.UpdatePolyclinic(dbPolyclinic, request);
            return Ok();
        }

        [HttpDelete("{id} DeletePolyclinic")]
        public ActionResult DeletePolyclinic(Guid id)
        {
            var dbPolyclinic = _polyclinicService.GetPolyclinic(id);
            if (dbPolyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            _polyclinicService.DeletePolyclinic(dbPolyclinic);
            return Ok();
        }
    }
}
