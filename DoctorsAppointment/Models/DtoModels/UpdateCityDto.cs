namespace DoctorsAppointment.Models.DtoModels
{
    public class UpdateCityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
