namespace DoctorsAppointment.Service
{
    public class DoctorService : ControllerBase, IDoctorService
    {
        private readonly DataContext _context;

        public DoctorService(DataContext context)
        {
            _context = context;
        }
        public List<Doctor> GetDoctors()
        {
            return _context.Doctors.ToList();
        }

        public Doctor? GetDoctor(Guid id)
        {
            return  _context.Doctors
                .Include(_ => _.Specializations)
                .Include(_ => _.Polyclinics)
                .ThenInclude(_ => _.City)
                .FirstOrDefault(_ => _.Id == id);
        }

        public void AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public void AddDoctorSpecialization(Doctor doctor, Specialization specialization)
        {
            doctor.Specializations.Add(specialization);
            _context.SaveChanges();
        }

        public void AddDoctorPolyclinic(Doctor doctor, Polyclinic polyclinic)
        {
            doctor.Polyclinics.Add(polyclinic);
            _context.SaveChanges();
        }

        public void UpdateDoctor(Doctor dbDoctor, UpdateDoctorDto request)
        {
            dbDoctor.FullName = request.FullName;
            _context.SaveChanges();
        }
        public void AddDoctorsPhoto(Doctor dbDoctor, string path)
        {
            dbDoctor.Photo = path;
            _context.SaveChanges();
        }

        public void DeleteDoctor(Doctor dbDoctor)
        {
            _context.Doctors.Remove(dbDoctor);
            _context.SaveChanges();
        }

        public void DeleteDoctorSpecialization(Doctor doctor, Specialization specialization)
        {
            doctor.Specializations.Remove(specialization);
            _context.SaveChanges();
        }

        public void DeleteDoctorPolyclinic(Doctor doctor, Polyclinic polyclinic)
        {
            doctor.Polyclinics.Remove(polyclinic);
            _context.SaveChanges();
        }
    }
}
