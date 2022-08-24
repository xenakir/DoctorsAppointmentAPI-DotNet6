namespace DoctorsAppointment.Models
{
    public class Polyclinic
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Location { get; set; }
        public City City { get; set; }
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
