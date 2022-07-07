namespace DoctorsAppointment.Models.GetModels
{
    public class GetPolyclinicModel
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Location { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; }
    }
}
