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

        public Polyclinic? GetPolyclinic(Guid id)
        {
            return _context.Polyclinics
                .Include(_ => _.City)
                .Include(_ => _.Doctors)
                .FirstOrDefault(_ => _.Id == id);
        }

        public void AddPolyclinic(Polyclinic polyclinic)
        {
            _context.Polyclinics.Add(polyclinic);
            _context.SaveChanges();
        }

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

        public void DeletePolyclinic(Polyclinic dbPolyclinic)
        {
            _context.Polyclinics.Remove(dbPolyclinic);
            _context.SaveChanges();
        }
    }
}
