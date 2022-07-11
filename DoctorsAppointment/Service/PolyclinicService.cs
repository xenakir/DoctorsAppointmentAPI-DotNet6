namespace DoctorsAppointment.Service
{
    public class PolyclinicService : ControllerBase, IPolyclinicService
    {
        private readonly DataContext _context;
        public PolyclinicService(DataContext context)
        {
            _context = context;
        }
        public List<Polyclinic> GetPolyclinics()
        {
            return _context.Polyclinics
                .Include(_ => _.City)
                .ToList();
        }
        //public async Task<ActionResult<List<GetPolyclinicModel>>> GetPolyclinics()
        //{
        //    var polyclinics = new List<GetPolyclinicModel>();
        //    var dbPolyclinics = await _context.Polyclinics.Include(p => p.City).ToListAsync();
        //    foreach (var item in dbPolyclinics)
        //    {
        //        polyclinics.Add(new GetPolyclinicModel
        //        {
        //            Id = item.Id,
        //            Address = item.Address,
        //            Photo = item.Photo,
        //            Location = item.Location,
        //            CityId = item.City.Id,
        //            CityName = item.City.Name
        //        });
        //    }
        //    return Ok(polyclinics);
        //}
        public Polyclinic? GetPolyclinic(Guid id)
        {
            return _context.Polyclinics
                .Include(_ => _.City)
                .Include(_ => _.Doctors)
                .FirstOrDefault(_ => _.Id == id);
        }
        //public async Task<ActionResult<GetPolyclinicModel>> GetPolyclinic(Guid id)
        //{
        //    var dbPolyclinics = await _context.Polyclinics.Include(p => p.City).ToListAsync();
        //    var polyclinic = new GetPolyclinicModel();
        //    foreach (var item in dbPolyclinics)
        //    {
        //        if (item.Id == id)
        //            polyclinic = new GetPolyclinicModel
        //            {
        //                Id = item.Id,
        //                Address = item.Address,
        //                Photo = item.Photo,
        //                Location = item.Location,
        //                CityId = item.City.Id,
        //                CityName = item.City.Name
        //            };
        //    }
        //    if (polyclinic == null)
        //        return BadRequest("Поликлиника не найдена.");

        //    return Ok(polyclinic);
        //}

        //public async Task<ActionResult<List<GetDoctorModel>>> GetPolyclinicDoctors(Guid id)
        //{
        //    var polyclinic = await _context.Polyclinics
        //        .Where(p => p.Id == id)
        //        .Include(d => d.Doctors)
        //        .FirstOrDefaultAsync();
        //    if (polyclinic == null)
        //        return BadRequest("Доктор не найден.");

        //    var doctors = new List<GetDoctorModel>();
        //    foreach (var item in polyclinic.Doctors)
        //    {
        //        doctors.Add(new GetDoctorModel
        //        {
        //            Id = item.Id,
        //            FullName = item.FullName,
        //            Photo = item.Photo
        //        });
        //    }
        //    return Ok(doctors);
        //}
        public void AddPolyclinic(Polyclinic polyclinic)
        {
            _context.Polyclinics.Add(polyclinic);
            _context.SaveChanges();
        }
        //public async Task<ActionResult> AddPolyclinic(AddPolyclinicDto request)
        //{
        //    var city = await _context.Cities.FindAsync(request.CityId);
        //    if (city == null)
        //        return NotFound();

        //    var newPolyclinic = new Polyclinic
        //    {
        //        Id = Guid.NewGuid(),
        //        Address = request.Address,
        //        Photo = request.Photo,
        //        Location = request.Location,
        //        City = city
        //    };

        //    _context.Polyclinics.Add(newPolyclinic);
        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
        public void UpdatePolyclinic(Polyclinic dbPolyclinic, UpdatePolyclinicDto request)
        {
            dbPolyclinic.Address = request.Address;
            dbPolyclinic.Location = request.Location;
            _context.SaveChanges();
        }
        public void AddPolyclinicsPhoto(Polyclinic dbPolyclinic, string path)
        {
            dbPolyclinic.Photo = path;
            _context.SaveChanges();
        }
        //public async Task<ActionResult> UpdatePolyclinic(UpdatePolyclinicDto request)
        //{
        //    var dbPolyclinic = await _context.Polyclinics.FindAsync(request.Id);
        //    if (dbPolyclinic == null)
        //        return BadRequest("Поликлиника не найдена.");

        //    dbPolyclinic.Address = request.Address;
        //    dbPolyclinic.Photo = request.Photo;
        //    dbPolyclinic.Location = request.Location;

        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
        public void DeletePolyclinic(Polyclinic dbPolyclinic)
        {
            _context.Polyclinics.Remove(dbPolyclinic);
            _context.SaveChanges();
        }


        //public async Task<ActionResult> DeletePolyclinic(Guid id)
        //{
        //    var dbPolyclinic = await _context.Polyclinics.FindAsync(id);
        //    if (dbPolyclinic == null)
        //        return BadRequest("Поликлиника не найдена.");

        //    _context.Polyclinics.Remove(dbPolyclinic);
        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
    }
}
