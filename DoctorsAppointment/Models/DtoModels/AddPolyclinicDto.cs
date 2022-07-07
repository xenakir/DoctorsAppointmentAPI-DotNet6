namespace DoctorsAppointment.Models.DtoModels
{
    public class AddPolyclinicDto
    {
        public string Address { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public Guid CityId { get; set; }
    }
}
