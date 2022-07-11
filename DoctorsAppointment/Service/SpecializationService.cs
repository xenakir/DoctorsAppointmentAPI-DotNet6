namespace DoctorsAppointment.Service
{
    public class SpecializationService : ControllerBase, ISpecializationService
    {
        private readonly DataContext _context;

        public SpecializationService(DataContext context)
        {
            _context = context;
        }
        public List<Specialization> GetSpecializations()
        {
            return _context.Specializations.ToList();
        }
        //public async Task<ActionResult<List<GetSpecializationModel>>> GetSpecializations()
        //{
        //    var specializations = new List<GetSpecializationModel>();
        //    var dbSpecializations = await _context.Specializations.ToListAsync();
        //    foreach (var item in dbSpecializations)
        //    {
        //        specializations.Add(new GetSpecializationModel
        //        {
        //            Id = item.Id,
        //            Name = item.Name
        //        });
        //    }
        //    return Ok(specializations);
        //}
        public Specialization? GetSpecialization(Guid id)
        {
            return _context.Specializations
                .FirstOrDefault(_ => _.Id == id); ;
        }
        //public async Task<ActionResult<Specialization>> GetSpecialization(Guid id)
        //{
        //    var dbSpecializations = await _context.Specializations.ToListAsync();
        //    var specialization = new GetSpecializationModel();
        //    foreach (var item in dbSpecializations)
        //    {
        //        if (item.Id == id)
        //            specialization = new GetSpecializationModel
        //            {
        //                Id = item.Id,
        //                Name = item.Name
        //            };
        //    }
        //    if (specialization == null)
        //        return BadRequest("Специализация не найдена.");

        //    return Ok(specialization);
        //}
        public void AddSpecialization(Specialization specialization)
        {
            _context.Specializations.Add(specialization);
            _context.SaveChanges();
        }
        //public async Task<ActionResult> AddSpecialization(string name)
        //{
        //    var newSpecialization = new Specialization
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = name
        //    };

        //    await _context.Specializations.AddAsync(newSpecialization);
        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
        public void UpdateSpecialization(Specialization dbSpecialization, UpdateSpecializationDto request)
        {
            dbSpecialization.Name = request.Name;
            _context.SaveChanges();
        }
        //public async Task<ActionResult> UpdateSpecialization(UpdateSpecializationDto request)
        //{
        //    var dbSpecialization = await _context.Specializations.FindAsync(request.Id);
        //    if (dbSpecialization == null)
        //        return BadRequest("Специализация не найдена.");

        //    dbSpecialization.Name = request.Name;

        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
        public void DeleteSpecialization(Specialization dbSpecialization)
        {
            _context.Specializations.Remove(dbSpecialization);
            _context.SaveChanges();
        }
        //public async Task<ActionResult> DeleteSpecialization(Guid id)
        //{
        //    var dbSpecialization = await _context.Specializations.FindAsync(id);
        //    if (dbSpecialization == null)
        //        return BadRequest("Специализация не найдена.");

        //    _context.Specializations.Remove(dbSpecialization);
        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
    }
}
