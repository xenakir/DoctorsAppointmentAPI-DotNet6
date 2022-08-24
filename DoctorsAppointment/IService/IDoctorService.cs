namespace DoctorsAppointment.IService
{
    public interface IDoctorService
    {
        List<Doctor> GetDoctors();
        Doctor? GetDoctor(Guid id);
        void AddDoctor(Doctor doctor);
        void AddDoctorSpecialization(Doctor doctor, Specialization specialization);
        void AddDoctorPolyclinic(Doctor doctor, Polyclinic polyclinic);
        void UpdateDoctor(Doctor dbDoctor, UpdateDoctorDto request);
        void AddDoctorsPhoto(Doctor dbDoctor, string path);
        void DeleteDoctor(Doctor dbDoctor);
        void DeleteDoctorSpecialization(Doctor doctor, Specialization specialization);
        void DeleteDoctorPolyclinic(Doctor doctor, Polyclinic polyclinic);
    }
}
