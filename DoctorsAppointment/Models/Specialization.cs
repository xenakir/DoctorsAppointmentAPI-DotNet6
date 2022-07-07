namespace DoctorsAppointment.Models
{
    public class Specialization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}