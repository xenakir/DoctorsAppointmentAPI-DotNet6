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

        public Specialization? GetSpecialization(Guid id)
        {
            return _context.Specializations
                .FirstOrDefault(_ => _.Id == id); ;
        }

        public void AddSpecialization(Specialization specialization)
        {
            _context.Specializations.Add(specialization);
            _context.SaveChanges();
        }

        public void UpdateSpecialization(Specialization dbSpecialization, UpdateSpecializationDto request)
        {
            dbSpecialization.Name = request.Name;
            _context.SaveChanges();
        }

        public void DeleteSpecialization(Specialization dbSpecialization)
        {
            _context.Specializations.Remove(dbSpecialization);
            _context.SaveChanges();
        }
    }
}
