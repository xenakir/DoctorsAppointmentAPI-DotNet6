namespace DoctorsAppointment.Models
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } 
        public List<Polyclinic> Polyclinics { get; set; } = new List<Polyclinic>();
    }
}
