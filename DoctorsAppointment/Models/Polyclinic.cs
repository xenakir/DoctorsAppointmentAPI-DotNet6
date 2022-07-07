global using System.Text.Json.Serialization;

namespace DoctorsAppointment.Models
{
    public class Polyclinic
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Location { get; set; }
        //[JsonIgnore]
        public City City { get; set; }
        //[JsonIgnore]
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
