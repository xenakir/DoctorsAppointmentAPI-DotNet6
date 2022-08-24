namespace DoctorsAppointment.IService
{
    public interface ISpecializationService
    {
        List<Specialization> GetSpecializations();
        Specialization? GetSpecialization(Guid id);
        void AddSpecialization(Specialization specialization);
        void UpdateSpecialization(Specialization dbSpecialization, UpdateSpecializationDto request);
        void DeleteSpecialization(Specialization dbSpecialization);
    }
}
