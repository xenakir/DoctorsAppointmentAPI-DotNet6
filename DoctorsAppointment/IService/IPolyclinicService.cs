namespace DoctorsAppointment.IService
{
    public interface IPolyclinicService
    {
        List<Polyclinic> GetPolyclinics();
        Polyclinic? GetPolyclinic(Guid id);
        void AddPolyclinic(Polyclinic polyclinic);
        void UpdatePolyclinic(Polyclinic dbPolyclinic, UpdatePolyclinicDto request);
        void AddPolyclinicsPhoto(Polyclinic dbPolyclinic, string path);
        void DeletePolyclinic(Polyclinic dbPolyclinic);
    }
}
