namespace DoctorsAppointment.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public List<Polyclinic> Polyclinics { get; set; } = new List<Polyclinic>();
        public List<Specialization> Specializations { get; set; } = new List<Specialization>();
    }
}
